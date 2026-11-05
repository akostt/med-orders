using MedOrders.Web.Domain;

namespace MedOrders.Web.Persistence;

/// <summary>Демо-каталог из макетов ЛР4. Попадает в миграцию через HasData.</summary>
internal static class CatalogSeedData
{
    public static readonly Category[] Categories =
    [
        new() { Id = 1, Name = "Диагностика" },
        new() { Id = 2, Name = "Мониторинг пациента" },
        new() { Id = 3, Name = "Реабилитация" },
        new() { Id = 4, Name = "Расходные материалы" },
    ];

    public static readonly Manufacturer[] Manufacturers =
    [
        new() { Id = 1, Name = "МедЭлектроника", Country = "Россия" },
        new() { Id = 2, Name = "КардиоТех", Country = "Россия" },
        new() { Id = 3, Name = "ВитаМед", Country = "Германия" },
    ];

    public static readonly Warehouse[] Warehouses =
    [
        new() { Id = 1, Name = "Основной склад", Address = "Адрес склада уточняется" },
    ];

    public static readonly Product[] Products =
    [
        new()
        {
            Id = 1, Article = "MT-1043", Name = "Монитор пациента М-7", CategoryId = 2, ManufacturerId = 1,
            Price = 189000m, RegistrationNumber = "РУ-2024/0173", StorageConditions = "+5…+40 °C, сухо",
            WarrantyMonths = 24, ShelfLifeMonths = 36,
            Specifications = Lines(
                "Тип: Настольный монитор, 6 параметров",
                "Диапазон ЧСС: 30–250 уд/мин",
                "Экран: 12,1\", сенсорный, TFT",
                "Питание: ~220 В / встроенный аккумулятор",
                "Масса: 3,2 кг",
                "Комплектация: монитор, кабель ЭКГ, манжета SpO₂, руководство",
                "Производитель обязан обеспечить сервисное обслуживание в течение гарантии."),
        },
        new()
        {
            Id = 2, Article = "PO-2210", Name = "Пульсоксиметр ОксиЛайт 3", CategoryId = 1, ManufacturerId = 2,
            Price = 1890m, RegistrationNumber = "РУ-2023/1184", StorageConditions = "+5…+40 °C, сухо",
            WarrantyMonths = 12, ShelfLifeMonths = 48,
            Specifications = Lines(
                "Тип: Портативный пульсоксиметр",
                "Диапазон SpO₂: 70–100 %",
                "Питание: 2 батареи AAA"),
        },
        new()
        {
            Id = 3, Article = "IN-0875", Name = "Ингалятор «Бриз-компрессор»", CategoryId = 3, ManufacturerId = 3,
            Price = 3250m, StorageConditions = "+5…+30 °C, сухо",
            WarrantyMonths = 12, ShelfLifeMonths = 60,
            Specifications = Lines(
                "Тип: Компрессорный ингалятор",
                "Производительность: 0,25 мл/мин",
                "Питание: ~220 В"),
        },
        new()
        {
            Id = 4, Article = "TN-0130", Name = "Тонометр «Кардио-Про»", CategoryId = 1, ManufacturerId = 2,
            Price = 4590m, RegistrationNumber = "РУ-2022/0455", StorageConditions = "+5…+40 °C, сухо",
            WarrantyMonths = 24, ShelfLifeMonths = 60,
            Specifications = Lines(
                "Тип: Автоматический тонометр на плечо",
                "Диапазон измерения: 0–299 мм рт. ст."),
        },
        new()
        {
            Id = 5, Article = "EK-3310", Name = "Электрокардиограф ЭКГ-3К", CategoryId = 1, ManufacturerId = 2,
            Price = 412500m, RegistrationNumber = "РУ-2021/0912", StorageConditions = "+10…+35 °C, сухо",
            WarrantyMonths = 24, ShelfLifeMonths = 96,
            Specifications = Lines(
                "Тип: Трёхканальный электрокардиограф",
                "Каналы: 3",
                "Питание: ~220 В"),
        },
        new()
        {
            Id = 6, Article = "KU-0200", Name = "Кушетка медицинская КМ-200", CategoryId = 3, ManufacturerId = 1,
            Price = 27800m, StorageConditions = "Сухое помещение",
            WarrantyMonths = 12, ShelfLifeMonths = 120,
            Specifications = Lines(
                "Тип: Смотровая кушетка",
                "Нагрузка: до 150 кг",
                "Материал: эко-кожа"),
        },
    ];

    public static readonly Certificate[] Certificates =
    [
        new() { Id = 1, ProductId = 1, DocType = CertificateType.RegistrationCertificate, Number = "РУ-2024/0173", IssuedAt = new DateOnly(2024, 1, 15) },
        new() { Id = 2, ProductId = 2, DocType = CertificateType.RegistrationCertificate, Number = "РУ-2023/1184", IssuedAt = new DateOnly(2023, 6, 1) },
        new() { Id = 3, ProductId = 3, DocType = CertificateType.ConformityCertificate, Number = "ТР ТС 020/2011", IssuedAt = new DateOnly(2025, 3, 10) },
        new() { Id = 4, ProductId = 4, DocType = CertificateType.RegistrationCertificate, Number = "РУ-2022/0455", IssuedAt = new DateOnly(2022, 9, 20) },
        new() { Id = 5, ProductId = 5, DocType = CertificateType.RegistrationCertificate, Number = "РУ-2021/0912", IssuedAt = new DateOnly(2021, 11, 5) },
        new() { Id = 6, ProductId = 6, DocType = CertificateType.ConformityCertificate, Number = "ГОСТ Р 52015", IssuedAt = new DateOnly(2025, 5, 30) },
    ];

    private static readonly DateTime SeedDate = new(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc);

    public static readonly Stock[] Stocks =
    [
        new() { Id = 1, ProductId = 1, WarehouseId = 1, Quantity = 12, UpdatedAt = SeedDate },
        new() { Id = 2, ProductId = 2, WarehouseId = 1, Quantity = 40, UpdatedAt = SeedDate },
        new() { Id = 3, ProductId = 3, WarehouseId = 1, Quantity = 2, UpdatedAt = SeedDate },
        new() { Id = 4, ProductId = 4, WarehouseId = 1, Quantity = 3, UpdatedAt = SeedDate },
        new() { Id = 5, ProductId = 5, WarehouseId = 1, Quantity = 0, UpdatedAt = SeedDate },
        new() { Id = 6, ProductId = 6, WarehouseId = 1, Quantity = 5, UpdatedAt = SeedDate },
    ];

    private static string Lines(params string[] lines) => string.Join('\n', lines);
}
