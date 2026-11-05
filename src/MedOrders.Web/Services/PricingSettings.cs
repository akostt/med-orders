using MedOrders.Web.Domain;

namespace MedOrders.Web.Services;

/// <summary>Налоговая ставка и стоимость доставки. Значения берутся из appsettings (разделы Tax и Delivery).</summary>
/// <param name="VatRatePercent">Ставка НДС в процентах.</param>
/// <param name="CourierCost">Стоимость доставки курьером, рублей.</param>
/// <param name="TransportCompanyCost">Стоимость доставки транспортной компанией, рублей.</param>
/// <param name="PickupCost">Стоимость самовывоза, рублей.</param>
public sealed record PricingSettings(
    decimal VatRatePercent,
    decimal CourierCost,
    decimal TransportCompanyCost,
    decimal PickupCost)
{
    /// <summary>Возвращает стоимость доставки для выбранного способа.</summary>
    /// <param name="method">Способ доставки.</param>
    /// <returns>Стоимость доставки, рублей.</returns>
    public decimal GetDeliveryCost(DeliveryMethod method) => method switch
    {
        DeliveryMethod.Courier => CourierCost,
        DeliveryMethod.TransportCompany => TransportCompanyCost,
        _ => PickupCost,
    };
}
