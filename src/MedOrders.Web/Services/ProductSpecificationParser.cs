namespace MedOrders.Web.Services;

/// <summary>Строка технической характеристики. Имя пустое для текстовых примечаний.</summary>
/// <param name="Name">Название параметра, например «Питание». Может быть пустым.</param>
/// <param name="Value">Значение параметра или текст примечания.</param>
public sealed record SpecificationRow(string? Name, string Value);

/// <summary>Разбор поля Specifications товара: строки «Название: значение» превращаются в таблицу.</summary>
public static class ProductSpecificationParser
{
    /// <summary>Разбирает текст характеристик.</summary>
    /// <param name="text">Текст из поля Specifications, строки разделены переводом строки.</param>
    /// <returns>Строки в исходном порядке.</returns>
    public static IReadOnlyList<SpecificationRow> Parse(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return [];
        }

        var rows = new List<SpecificationRow>();
        foreach (var line in text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var separatorIndex = line.IndexOf(':');
            rows.Add(separatorIndex > 0
                ? new SpecificationRow(line[..separatorIndex].Trim(), line[(separatorIndex + 1)..].Trim())
                : new SpecificationRow(null, line));
        }

        return rows;
    }
}
