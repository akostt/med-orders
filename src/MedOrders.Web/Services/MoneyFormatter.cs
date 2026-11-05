using System.Globalization;

namespace MedOrders.Web.Services;

/// <summary>Форматирование денежных сумм для интерфейса.</summary>
public static class MoneyFormatter
{
    private static readonly CultureInfo RussianCulture = CultureInfo.GetCultureInfo("ru-RU");

    /// <summary>Форматирует сумму по-русски: целые суммы без копеек, например «189 000», дробные — с двумя знаками.</summary>
    /// <param name="amount">Сумма в рублях.</param>
    /// <returns>Строка для показа пользователю.</returns>
    public static string Format(decimal amount) =>
        amount % 1 == 0
            ? amount.ToString("N0", RussianCulture)
            : amount.ToString("N2", RussianCulture);
}
