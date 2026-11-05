namespace MedOrders.Web.Services;

/// <summary>Данные формы регистрации клиента.</summary>
/// <param name="Login">Логин для входа.</param>
/// <param name="Password">Пароль. В базе хранится только хеш.</param>
/// <param name="FullName">Фамилия, имя и отчество.</param>
/// <param name="Email">Адрес электронной почты.</param>
/// <param name="Address">Адрес клиента.</param>
public sealed record RegisterClientCommand(string Login, string Password, string FullName, string Email, string Address);
