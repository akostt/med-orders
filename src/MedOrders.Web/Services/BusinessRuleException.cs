namespace MedOrders.Web.Services;

/// <summary>
/// Нарушение бизнес-правила, например недостаток товара на складе.
/// Текст сообщения можно показывать пользователю без изменений.
/// </summary>
public sealed class BusinessRuleException(string message) : Exception(message);
