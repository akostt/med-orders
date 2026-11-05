namespace MedOrders.Web.Services;

/// <summary>
/// Ошибки заполнения полей формы. Ключ словаря — имя поля формы,
/// значение — текст ошибки, который выводится под полем.
/// </summary>
public sealed class FormValidationException(IReadOnlyDictionary<string, string> fieldErrors)
    : Exception("Проверьте заполнение полей.")
{
    /// <summary>Ошибки по именам полей.</summary>
    public IReadOnlyDictionary<string, string> FieldErrors { get; } = fieldErrors;
}
