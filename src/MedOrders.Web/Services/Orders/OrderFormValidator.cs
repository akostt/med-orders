namespace MedOrders.Web.Services;

/// <summary>Проверка обязательных полей формы заказа (первый шаг диаграммы «Оформление заказа»).</summary>
public static class OrderFormValidator
{
    private const int MaxAddressLength = 300;

    /// <summary>Проверяет адрес доставки и количество.</summary>
    /// <param name="command">Данные формы.</param>
    /// <returns>Пустой словарь, если всё заполнено верно; иначе ошибки по полям.</returns>
    public static Dictionary<string, string> Validate(CreateOrderCommand command)
    {
        var errors = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(command.DeliveryAddress))
        {
            errors["DeliveryAddress"] = "Поле обязательно: укажите адрес в формате «город, улица, дом».";
        }
        else if (command.DeliveryAddress.Trim().Length > MaxAddressLength)
        {
            errors["DeliveryAddress"] = $"Адрес доставки не длиннее {MaxAddressLength} символов.";
        }

        if (command.Quantity <= 0)
        {
            errors["Quantity"] = "Количество должно быть целым числом больше нуля.";
        }

        return errors;
    }
}
