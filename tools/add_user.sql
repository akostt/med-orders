-- Ручное добавление пользователя.
-- 1) Хеш пароля: python3 tools/make_password_hash.py "Пароль1" (вставить в N'<ХЕШ>')
-- 2) Замените логин, email, ФИО, роль, адрес клиента.
-- Роли: N'клиент', N'менеджер', N'администратор'.

USE MedOrders;

DECLARE @roleId INT = (SELECT ID_Role FROM [Role] WHERE Name = N'клиент');

INSERT INTO [User] (Login, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash,
                    SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled,
                    LockoutEnabled, AccessFailedCount, FullName, RegisteredAt, IsActive, ID_Role)
VALUES (N'petrov_ds', N'PETROV_DS', N'petrov@example.com', N'PETROV@EXAMPLE.COM', 1, N'<ХЕШ>',
        CAST(NEWID() AS NVARCHAR(128)), CAST(NEWID() AS NVARCHAR(128)), 0, 0,
        1, 0, N'Петров Д.С.', SYSUTCDATETIME(), 1, @roleId);

-- Для роли «клиент» нужен профиль клиента (для менеджера и администратора не нужен):
DECLARE @userId INT = SCOPE_IDENTITY();
INSERT INTO [Client] (ID_User, ClientType, Address)
VALUES (@userId, N'Individual', N'г. Владимир, ул. Примерная, д. 1');
