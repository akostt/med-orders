namespace MedOrders.Web.Domain;

/// <summary>Запись журнала действий пользователей (таблица ActionLog).</summary>
public sealed class ActionLog
{
    /// <summary>Идентификатор записи (ID_ActionLog).</summary>
    public int Id { get; set; }

    /// <summary>Пользователь, совершивший действие. Пусто для системных действий.</summary>
    public int? UserId { get; set; }

    /// <summary>Название действия, см. <see cref="Actions"/>.</summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>Таблица, в которой выполнено действие, см. <see cref="Tables"/>.</summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>Идентификатор изменённой записи.</summary>
    public int? RecordId { get; set; }

    /// <summary>Дата и время действия (UTC).</summary>
    public DateTime RecordedAt { get; set; }

    /// <summary>Создаёт запись журнала.</summary>
    /// <param name="userId">Пользователь или null для системного действия.</param>
    /// <param name="action">Название действия из <see cref="Actions"/>.</param>
    /// <param name="tableName">Таблица из <see cref="Tables"/>.</param>
    /// <param name="recordId">Идентификатор изменённой записи.</param>
    /// <param name="recordedAt">Момент действия (UTC).</param>
    /// <returns>Новая запись, которую нужно добавить в контекст.</returns>
    public static ActionLog Create(int? userId, string action, string tableName, int? recordId, DateTime recordedAt) =>
        new()
        {
            UserId = userId,
            Action = action,
            TableName = tableName,
            RecordId = recordId,
            RecordedAt = recordedAt,
        };

    /// <summary>Названия действий, которые пишутся в журнал.</summary>
    public static class Actions
    {
        /// <summary>Создание записи.</summary>
        public const string Create = "создать";
    }

    /// <summary>Названия таблиц, которые попадают в журнал.</summary>
    public static class Tables
    {
        /// <summary>Таблица товаров.</summary>
        public const string Product = "Product";
    }
}
