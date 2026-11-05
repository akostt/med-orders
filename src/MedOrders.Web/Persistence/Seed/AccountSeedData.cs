using MedOrders.Web.Domain;
using MedOrders.Web.Security;

namespace MedOrders.Web.Persistence;

/// <summary>
/// Учётные записи, роли и профиль демо-клиента. Попадают в миграцию через HasData.
/// В репозитории только хеши паролей (формат ASP.NET Core Identity V3).
/// Новый пользователь: tools/make_password_hash.py и tools/add_user.sql.
/// </summary>
internal static class AccountSeedData
{
    private static readonly DateTime SeedDate = new(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc);

    public static readonly ApplicationRole[] Roles =
    [
        new()
        {
            Id = 1, Name = RoleNames.Client, NormalizedName = "КЛИЕНТ",
            AccessRights = "Каталог, оформление и отслеживание собственных заказов, документы заказов", ConcurrencyStamp = "b4a5ebcb-be30-4869-9a7c-2b32f27e0d0d",
        },
        new()
        {
            Id = 2, Name = RoleNames.Manager, NormalizedName = "МЕНЕДЖЕР",
            AccessRights = "Каталог и товары, обработка заказов, документы, отчёты", ConcurrencyStamp = "4231f10f-d269-4eca-a195-b1f34bc2452f",
        },
        new()
        {
            Id = 3, Name = RoleNames.Administrator, NormalizedName = "АДМИНИСТРАТОР",
            AccessRights = "Пользователи и роли, блокировка, журнал действий", ConcurrencyStamp = "69bee77c-61d8-4db5-8964-227707766914",
        },
    ];

    public static readonly ApplicationUser[] Users =
    [
        new()
        {
            Id = 1, UserName = "admin", NormalizedUserName = "ADMIN",
            Email = "admin@medorders.local", NormalizedEmail = "ADMIN@MEDORDERS.LOCAL", EmailConfirmed = true,
            PasswordHash = "AQAAAAEAAYagAAAAEDLzl6tC3Gdg1Wkq4RU7wP/87WZCS8f7/33ozgowcp1120JzkokcYU5DF7hkeHh+Rw==",
            SecurityStamp = "fe509b19-8979-4cff-a5a6-0f46d5067fe7", ConcurrencyStamp = "82b442ba-1d16-424a-995e-8fda9ac0469b",
            PhoneNumberConfirmed = false, TwoFactorEnabled = false,
            LockoutEnabled = true, AccessFailedCount = 0,
            FullName = "Администратор системы", RegisteredAt = SeedDate, IsActive = true, RoleId = 3,
        },
        new()
        {
            Id = 2, UserName = "manager", NormalizedUserName = "MANAGER",
            Email = "manager@medorders.local", NormalizedEmail = "MANAGER@MEDORDERS.LOCAL", EmailConfirmed = true,
            PasswordHash = "AQAAAAEAAYagAAAAEDLzl6tC3Gdg1Wkq4RU7wP/87WZCS8f7/33ozgowcp1120JzkokcYU5DF7hkeHh+Rw==",
            SecurityStamp = "fdf202c2-2873-410d-8a2e-ff9635f504e6", ConcurrencyStamp = "78879a0b-00f7-4efb-85cc-d82fea0e22ef",
            PhoneNumberConfirmed = false, TwoFactorEnabled = false,
            LockoutEnabled = true, AccessFailedCount = 0,
            FullName = "Петров Д.С.", RegisteredAt = SeedDate, IsActive = true, RoleId = 2,
        },
        new()
        {
            Id = 3, UserName = "ivanova", NormalizedUserName = "IVANOVA",
            Email = "ivanova@medorders.local", NormalizedEmail = "IVANOVA@MEDORDERS.LOCAL", EmailConfirmed = true,
            PasswordHash = "AQAAAAEAAYagAAAAEDLzl6tC3Gdg1Wkq4RU7wP/87WZCS8f7/33ozgowcp1120JzkokcYU5DF7hkeHh+Rw==",
            SecurityStamp = "6b47a1b3-efbc-4644-b5e2-ad6b94de74ee", ConcurrencyStamp = "af862b33-c456-4b06-8674-1039902c9abd",
            PhoneNumberConfirmed = false, TwoFactorEnabled = false,
            LockoutEnabled = true, AccessFailedCount = 0,
            FullName = "Иванова Е.С.", RegisteredAt = SeedDate, IsActive = true, RoleId = 1,
        },
    ];

    public static readonly Client[] Clients =
    [
        new()
        {
            Id = 1, UserId = 3, ClientType = ClientType.Individual,
            Address = "г. Владимир, ул. Студенческая, д. 1",
        },
    ];
}
