namespace MedOrders.Web.Domain;

/// <summary>Категория товаров каталога (таблица Category).</summary>
public sealed class Category
{
    /// <summary>Идентификатор категории (ID_Category).</summary>
    public int Id { get; set; }

    /// <summary>Название категории, например «Мониторинг пациента».</summary>
    public string Name { get; set; } = string.Empty;
}
