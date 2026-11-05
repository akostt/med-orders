using MedOrders.Web.Domain;
using MedOrders.Web.Persistence;
using MedOrders.Web.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedOrders.Web.Services;

/// <summary>Регистрация клиентов: учётная запись с ролью «клиент» и профиль клиента создаются вместе.</summary>
public sealed class AccountService(AppDbContext db, UserManager<ApplicationUser> users, TimeProvider clock)
{
    private const int LoginMaxLength = 50;
    private const int FullNameMaxLength = 150;
    private const int EmailMaxLength = 100;
    private const int AddressMaxLength = 300;

    /// <summary>Регистрирует клиента. При ошибке профиль не создаётся, учётная запись удаляется.</summary>
    /// <param name="command">Данные формы регистрации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <exception cref="FormValidationException">Ошибки в полях или логин, почта либо пароль не прошли проверку Identity.</exception>
    public async Task RegisterClientAsync(RegisterClientCommand command, CancellationToken cancellationToken = default)
    {
        var fieldErrors = ValidateFields(command);
        if (fieldErrors.Count > 0)
        {
            throw new FormValidationException(fieldErrors);
        }

        var clientRoleId = await db.Roles.Where(role => role.Name == RoleNames.Client)
            .Select(role => role.Id)
            .SingleAsync(cancellationToken);

        var user = new ApplicationUser
        {
            UserName = command.Login.Trim(),
            Email = command.Email.Trim(),
            FullName = command.FullName.Trim(),
            RegisteredAt = clock.GetUtcNow().UtcDateTime,
            IsActive = true,
            RoleId = clientRoleId,
        };

        var creationResult = await users.CreateAsync(user, command.Password);
        if (!creationResult.Succeeded)
        {
            throw new FormValidationException(MapIdentityErrors(creationResult.Errors));
        }

        try
        {
            db.Clients.Add(new Client
            {
                UserId = user.Id,
                ClientType = ClientType.Individual,
                Address = command.Address.Trim(),
            });
            await db.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await users.DeleteAsync(user);
            throw;
        }
    }

    private static Dictionary<string, string> ValidateFields(RegisterClientCommand command)
    {
        var errors = new Dictionary<string, string>();
        FieldValidation.RequireText(errors, nameof(command.Login), command.Login, LoginMaxLength, "Логин");
        FieldValidation.RequireText(errors, nameof(command.FullName), command.FullName, FullNameMaxLength, "ФИО");
        FieldValidation.RequireText(errors, nameof(command.Email), command.Email, EmailMaxLength, "Email");
        FieldValidation.RequireText(errors, nameof(command.Address), command.Address, AddressMaxLength, "Адрес");

        if (!string.IsNullOrWhiteSpace(command.Email) && !command.Email.Contains('@'))
        {
            errors[nameof(command.Email)] = "Введите корректный адрес электронной почты.";
        }

        if (string.IsNullOrEmpty(command.Password))
        {
            errors[nameof(command.Password)] = "Укажите пароль.";
        }

        return errors;
    }

    /// <summary>Переводит ошибки Identity в ключи полей формы.</summary>
    private static Dictionary<string, string> MapIdentityErrors(IEnumerable<IdentityError> identityErrors)
    {
        var fieldErrors = new Dictionary<string, string>();
        foreach (var identityError in identityErrors)
        {
            var fieldName = identityError.Code switch
            {
                "DuplicateUserName" or "InvalidUserName" => nameof(RegisterClientCommand.Login),
                "DuplicateEmail" or "InvalidEmail" => nameof(RegisterClientCommand.Email),
                _ => nameof(RegisterClientCommand.Password),
            };
            fieldErrors.TryAdd(fieldName, identityError.Description);
        }

        return fieldErrors;
    }
}
