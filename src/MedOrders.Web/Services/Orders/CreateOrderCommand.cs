using MedOrders.Web.Domain;

namespace MedOrders.Web.Services;

/// <summary>Данные, которые отправляет форма оформления заказа.</summary>
/// <param name="UserId">Учётная запись, от имени которой оформляется заказ.</param>
/// <param name="ProductId">Заказываемый товар.</param>
/// <param name="Quantity">Количество, штук. Должно быть больше нуля.</param>
/// <param name="DeliveryMethod">Способ доставки.</param>
/// <param name="DeliveryAddress">Адрес доставки.</param>
/// <param name="Notes">Комментарий клиента, необязательный.</param>
public sealed record CreateOrderCommand(
    int UserId,
    int ProductId,
    int Quantity,
    DeliveryMethod DeliveryMethod,
    string DeliveryAddress,
    string? Notes);
