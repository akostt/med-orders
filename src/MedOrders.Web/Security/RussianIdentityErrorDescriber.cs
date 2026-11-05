using Microsoft.AspNetCore.Identity;

namespace MedOrders.Web.Security;

/// <summary>Тексты ошибок Identity на русском языке для форм регистрации.</summary>
public sealed class RussianIdentityErrorDescriber : IdentityErrorDescriber
{
    /// <inheritdoc />
    public override IdentityError DuplicateUserName(string userName) =>
        CreateError(nameof(DuplicateUserName), $"Логин «{userName}» уже занят.");

    /// <inheritdoc />
    public override IdentityError DuplicateEmail(string email) =>
        CreateError(nameof(DuplicateEmail), $"Пользователь с почтой «{email}» уже зарегистрирован.");

    /// <inheritdoc />
    public override IdentityError InvalidUserName(string? userName) =>
        CreateError(nameof(InvalidUserName), $"Логин «{userName}» содержит недопустимые символы.");

    /// <inheritdoc />
    public override IdentityError InvalidEmail(string? email) =>
        CreateError(nameof(InvalidEmail), "Введите корректный адрес электронной почты.");

    /// <inheritdoc />
    public override IdentityError PasswordTooShort(int length) =>
        CreateError(nameof(PasswordTooShort), $"Пароль должен содержать не менее {length} символов.");

    /// <inheritdoc />
    public override IdentityError PasswordRequiresDigit() =>
        CreateError(nameof(PasswordRequiresDigit), "Пароль должен содержать хотя бы одну цифру.");

    /// <inheritdoc />
    public override IdentityError PasswordRequiresLower() =>
        CreateError(nameof(PasswordRequiresLower), "Пароль должен содержать строчную букву.");

    /// <inheritdoc />
    public override IdentityError PasswordRequiresUpper() =>
        CreateError(nameof(PasswordRequiresUpper), "Пароль должен содержать заглавную букву.");

    private static IdentityError CreateError(string code, string description) =>
        new() { Code = code, Description = description };
}
