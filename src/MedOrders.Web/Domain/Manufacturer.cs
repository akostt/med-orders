namespace MedOrders.Web.Domain;

/// <summary>Производитель медицинской техники (таблица Manufacturer).</summary>
public sealed class Manufacturer
{
    /// <summary>Идентификатор производителя (ID_Manufacturer).</summary>
    public int Id { get; set; }

    /// <summary>Название производителя.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Страна происхождения.</summary>
    public string Country { get; set; } = string.Empty;
}
