namespace MedOrders.Web.Services;

/// <summary>Общие проверки текстовых полей, которые используют сервисы.</summary>
public static class FieldValidation
{
    /// <summary>Проверяет обязательное текстовое поле: не пустое и не длиннее максимума.</summary>
    /// <param name="errors">Словарь ошибок, в который добавляется текст при нарушении.</param>
    /// <param name="fieldName">Имя поля формы.</param>
    /// <param name="value">Значение поля.</param>
    /// <param name="maxLength">Максимальная длина после удаления пробелов по краям.</param>
    /// <param name="displayName">Название поля для текста ошибки.</param>
    public static void RequireText(
        IDictionary<string, string> errors, string fieldName, string? value, int maxLength, string displayName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            errors[fieldName] = $"Поле «{displayName}» обязательно для заполнения.";
        }
        else if (value.Trim().Length > maxLength)
        {
            errors[fieldName] = $"Поле «{displayName}» не длиннее {maxLength} символов.";
        }
    }

    /// <summary>Проверяет необязательное текстовое поле: если заполнено, не длиннее максимума.</summary>
    /// <param name="errors">Словарь ошибок, в который добавляется текст при нарушении.</param>
    /// <param name="fieldName">Имя поля формы.</param>
    /// <param name="value">Значение поля.</param>
    /// <param name="maxLength">Максимальная длина после удаления пробелов по краям.</param>
    /// <param name="displayName">Название поля для текста ошибки.</param>
    public static void LimitText(
        IDictionary<string, string> errors, string fieldName, string? value, int maxLength, string displayName)
    {
        if (value is not null && value.Trim().Length > maxLength)
        {
            errors[fieldName] = $"Поле «{displayName}» не длиннее {maxLength} символов.";
        }
    }
}
