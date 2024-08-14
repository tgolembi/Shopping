IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Coupons] (
    [CouponId] int NOT NULL IDENTITY,
    [CouponCode] nvarchar(max) NOT NULL,
    [DiscountAmount] nvarchar(max) NOT NULL,
    [MinAmount] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Coupons] PRIMARY KEY ([CouponId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240629143045_AddCouponToDb', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Coupons]') AND [c].[name] = N'MinAmount');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Coupons] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Coupons] ALTER COLUMN [MinAmount] int NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Coupons]') AND [c].[name] = N'DiscountAmount');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Coupons] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Coupons] ALTER COLUMN [DiscountAmount] float NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240629143528_ModCouponColTypes', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CouponId', N'CouponCode', N'DiscountAmount', N'MinAmount') AND [object_id] = OBJECT_ID(N'[Coupons]'))
    SET IDENTITY_INSERT [Coupons] ON;
INSERT INTO [Coupons] ([CouponId], [CouponCode], [DiscountAmount], [MinAmount])
VALUES (1, N'10OFF', 10.0E0, 20),
(2, N'20OFF', 20.0E0, 40);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CouponId', N'CouponCode', N'DiscountAmount', N'MinAmount') AND [object_id] = OBJECT_ID(N'[Coupons]'))
    SET IDENTITY_INSERT [Coupons] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240629144701_CouponSeed', N'8.0.6');
GO

COMMIT;
GO

