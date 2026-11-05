namespace MedOrders.Web.Services;

/// <summary>Итоговая стоимость заказа. Все суммы в рублях.</summary>
/// <param name="Subtotal">Сумма по позициям без скидки.</param>
/// <param name="Discount">Сумма скидки.</param>
/// <param name="Vat">Сумма НДС.</param>
/// <param name="DeliveryCost">Стоимость доставки.</param>
/// <param name="Total">Итого к оплате.</param>
public sealed record OrderTotals(decimal Subtotal, decimal Discount, decimal Vat, decimal DeliveryCost, decimal Total);
