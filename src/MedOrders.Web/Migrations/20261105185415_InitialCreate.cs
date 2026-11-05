using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedOrders.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionLog",
                columns: table => new
                {
                    ID_ActionLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_User = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecordID = table.Column<int>(type: "int", nullable: true),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLog", x => x.ID_ActionLog);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID_Category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID_Category);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    ID_Manufacturer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ID_Manufacturer);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID_Role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessRights = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID_Role);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    ID_Warehouse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.ID_Warehouse);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Article = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ID_Category = table.Column<int>(type: "int", nullable: false),
                    ID_Manufacturer = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StorageConditions = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WarrantyMonths = table.Column<int>(type: "int", nullable: false),
                    ShelfLifeMonths = table.Column<int>(type: "int", nullable: false),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID_Product);
                    table.ForeignKey(
                        name: "FK_Product_Category_ID_Category",
                        column: x => x.ID_Category,
                        principalTable: "Category",
                        principalColumn: "ID_Category",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Manufacturer_ID_Manufacturer",
                        column: x => x.ID_Manufacturer,
                        principalTable: "Manufacturer",
                        principalColumn: "ID_Manufacturer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "ID_Role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ID_Role = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID_User);
                    table.ForeignKey(
                        name: "FK_User_Role_ID_Role",
                        column: x => x.ID_Role,
                        principalTable: "Role",
                        principalColumn: "ID_Role",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    ID_Certificate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Product = table.Column<int>(type: "int", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpiresAt = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.ID_Certificate);
                    table.ForeignKey(
                        name: "FK_Certificate_Product_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Product",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    ID_Stock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Product = table.Column<int>(type: "int", nullable: false),
                    ID_Warehouse = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.ID_Stock);
                    table.ForeignKey(
                        name: "FK_Stock_Product_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Product",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stock_Warehouse_ID_Warehouse",
                        column: x => x.ID_Warehouse,
                        principalTable: "Warehouse",
                        principalColumn: "ID_Warehouse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "ID_Role",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID_Client = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_User = table.Column<int>(type: "int", nullable: false),
                    ClientType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID_Client);
                    table.ForeignKey(
                        name: "FK_Client_User_ID_User",
                        column: x => x.ID_User,
                        principalTable: "User",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID_Order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ID_Client = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DeliveryMethod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DeliveryCost = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID_Order);
                    table.ForeignKey(
                        name: "FK_Order_Client_ID_Client",
                        column: x => x.ID_Client,
                        principalTable: "Client",
                        principalColumn: "ID_Client",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    ID_OrderItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Order = table.Column<int>(type: "int", nullable: false),
                    ID_Product = table.Column<int>(type: "int", nullable: false),
                    ID_Warehouse = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.ID_OrderItem);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_ID_Order",
                        column: x => x.ID_Order,
                        principalTable: "Order",
                        principalColumn: "ID_Order",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Product",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Warehouse_ID_Warehouse",
                        column: x => x.ID_Warehouse,
                        principalTable: "Warehouse",
                        principalColumn: "ID_Warehouse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "ID_Category", "Name" },
                values: new object[,]
                {
                    { 1, "Диагностика" },
                    { 2, "Мониторинг пациента" },
                    { 3, "Реабилитация" },
                    { 4, "Расходные материалы" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "ID_Manufacturer", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "Россия", "МедЭлектроника" },
                    { 2, "Россия", "КардиоТех" },
                    { 3, "Германия", "ВитаМед" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID_Role", "AccessRights", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "Каталог, оформление и отслеживание собственных заказов, документы заказов", "b4a5ebcb-be30-4869-9a7c-2b32f27e0d0d", "клиент", "КЛИЕНТ" },
                    { 2, "Каталог и товары, обработка заказов, документы, отчёты", "4231f10f-d269-4eca-a195-b1f34bc2452f", "менеджер", "МЕНЕДЖЕР" },
                    { 3, "Пользователи и роли, блокировка, журнал действий", "69bee77c-61d8-4db5-8964-227707766914", "администратор", "АДМИНИСТРАТОР" }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "ID_Warehouse", "Address", "Name" },
                values: new object[] { 1, "Адрес склада уточняется", "Основной склад" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID_Product", "Article", "ID_Category", "IsActive", "ID_Manufacturer", "Name", "Price", "RegistrationNumber", "ShelfLifeMonths", "Specifications", "StorageConditions", "WarrantyMonths" },
                values: new object[,]
                {
                    { 1, "MT-1043", 2, true, 1, "Монитор пациента М-7", 189000m, "РУ-2024/0173", 36, "Тип: Настольный монитор, 6 параметров\nДиапазон ЧСС: 30–250 уд/мин\nЭкран: 12,1\", сенсорный, TFT\nПитание: ~220 В / встроенный аккумулятор\nМасса: 3,2 кг\nКомплектация: монитор, кабель ЭКГ, манжета SpO₂, руководство\nПроизводитель обязан обеспечить сервисное обслуживание в течение гарантии.", "+5…+40 °C, сухо", 24 },
                    { 2, "PO-2210", 1, true, 2, "Пульсоксиметр ОксиЛайт 3", 1890m, "РУ-2023/1184", 48, "Тип: Портативный пульсоксиметр\nДиапазон SpO₂: 70–100 %\nПитание: 2 батареи AAA", "+5…+40 °C, сухо", 12 },
                    { 3, "IN-0875", 3, true, 3, "Ингалятор «Бриз-компрессор»", 3250m, null, 60, "Тип: Компрессорный ингалятор\nПроизводительность: 0,25 мл/мин\nПитание: ~220 В", "+5…+30 °C, сухо", 12 },
                    { 4, "TN-0130", 1, true, 2, "Тонометр «Кардио-Про»", 4590m, "РУ-2022/0455", 60, "Тип: Автоматический тонометр на плечо\nДиапазон измерения: 0–299 мм рт. ст.", "+5…+40 °C, сухо", 24 },
                    { 5, "EK-3310", 1, true, 2, "Электрокардиограф ЭКГ-3К", 412500m, "РУ-2021/0912", 96, "Тип: Трёхканальный электрокардиограф\nКаналы: 3\nПитание: ~220 В", "+10…+35 °C, сухо", 24 },
                    { 6, "KU-0200", 3, true, 1, "Кушетка медицинская КМ-200", 27800m, null, 120, "Тип: Смотровая кушетка\nНагрузка: до 150 кг\nМатериал: эко-кожа", "Сухое помещение", 12 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID_User", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisteredAt", "ID_Role", "SecurityStamp", "TwoFactorEnabled", "Login" },
                values: new object[,]
                {
                    { 1, 0, "82b442ba-1d16-424a-995e-8fda9ac0469b", "admin@medorders.local", true, "Администратор системы", true, true, null, "ADMIN@MEDORDERS.LOCAL", "ADMIN", "AQAAAAEAAYagAAAAEDLzl6tC3Gdg1Wkq4RU7wP/87WZCS8f7/33ozgowcp1120JzkokcYU5DF7hkeHh+Rw==", null, false, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "fe509b19-8979-4cff-a5a6-0f46d5067fe7", false, "admin" },
                    { 2, 0, "78879a0b-00f7-4efb-85cc-d82fea0e22ef", "manager@medorders.local", true, "Петров Д.С.", true, true, null, "MANAGER@MEDORDERS.LOCAL", "MANAGER", "AQAAAAEAAYagAAAAEDLzl6tC3Gdg1Wkq4RU7wP/87WZCS8f7/33ozgowcp1120JzkokcYU5DF7hkeHh+Rw==", null, false, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "fdf202c2-2873-410d-8a2e-ff9635f504e6", false, "manager" },
                    { 3, 0, "af862b33-c456-4b06-8674-1039902c9abd", "ivanova@medorders.local", true, "Иванова Е.С.", true, true, null, "IVANOVA@MEDORDERS.LOCAL", "IVANOVA", "AQAAAAEAAYagAAAAEDLzl6tC3Gdg1Wkq4RU7wP/87WZCS8f7/33ozgowcp1120JzkokcYU5DF7hkeHh+Rw==", null, false, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "6b47a1b3-efbc-4644-b5e2-ad6b94de74ee", false, "ivanova" }
                });

            migrationBuilder.InsertData(
                table: "Certificate",
                columns: new[] { "ID_Certificate", "DocType", "ExpiresAt", "IssuedAt", "Number", "ID_Product" },
                values: new object[,]
                {
                    { 1, "RegistrationCertificate", null, new DateOnly(2024, 1, 15), "РУ-2024/0173", 1 },
                    { 2, "RegistrationCertificate", null, new DateOnly(2023, 6, 1), "РУ-2023/1184", 2 },
                    { 3, "ConformityCertificate", null, new DateOnly(2025, 3, 10), "ТР ТС 020/2011", 3 },
                    { 4, "RegistrationCertificate", null, new DateOnly(2022, 9, 20), "РУ-2022/0455", 4 },
                    { 5, "RegistrationCertificate", null, new DateOnly(2021, 11, 5), "РУ-2021/0912", 5 },
                    { 6, "ConformityCertificate", null, new DateOnly(2025, 5, 30), "ГОСТ Р 52015", 6 }
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ID_Client", "Address", "ClientType", "ID_User" },
                values: new object[] { 1, "г. Владимир, ул. Студенческая, д. 1", "Individual", 3 });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "ID_Stock", "ID_Product", "Quantity", "ReservedQuantity", "UpdatedAt", "ID_Warehouse" },
                values: new object[,]
                {
                    { 1, 1, 12, 0, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 2, 40, 0, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, 3, 2, 0, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, 4, 3, 0, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 5, 5, 0, 0, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 6, 6, 5, 0, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_ID_Product",
                table: "Certificate",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ID_User",
                table: "Client",
                column: "ID_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ID_Client",
                table: "Order",
                column: "ID_Client");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Number",
                table: "Order",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ID_Order",
                table: "OrderItem",
                column: "ID_Order");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ID_Product",
                table: "OrderItem",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ID_Warehouse",
                table: "OrderItem",
                column: "ID_Warehouse");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Article",
                table: "Product",
                column: "Article",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ID_Category",
                table: "Product",
                column: "ID_Category");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ID_Manufacturer",
                table: "Product",
                column: "ID_Manufacturer");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ID_Product_ID_Warehouse",
                table: "Stock",
                columns: new[] { "ID_Product", "ID_Warehouse" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ID_Warehouse",
                table: "Stock",
                column: "ID_Warehouse");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_ID_Role",
                table: "User",
                column: "ID_Role");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLog");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
