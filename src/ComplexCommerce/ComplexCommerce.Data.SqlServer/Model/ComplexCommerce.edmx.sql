
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/26/2013 05:22:44
-- Generated from EDMX file: F:\My Documents\Visual Studio 2012\InHouse\Prototypes\ComplexCommerce\src\ComplexCommerce\ComplexCommerce.Data.SqlServer\Model\ComplexCommerce.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ComplexCommerce];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[CategoryXProductXLocale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryXProductXLocale];
GO
IF OBJECT_ID(N'[dbo].[Chain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chain];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductXStoreXLocale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductXStoreXLocale];
GO
IF OBJECT_ID(N'[dbo].[Store]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Store];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [RouteUrl] nvarchar(1024)  NOT NULL,
    [StoreId] uniqueidentifier  NOT NULL,
    [LocaleId] int  NOT NULL
);
GO

-- Creating table 'CategoryXProducts'
CREATE TABLE [dbo].[CategoryXProducts] (
    [CategoryId] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [RouteUrl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Chains'
CREATE TABLE [dbo].[Chains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Description] nvarchar(1000)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] uniqueidentifier  NOT NULL,
    [ChainId] int  NOT NULL,
    [SKU] nvarchar(100)  NULL,
    [ImageUrl] nvarchar(1024)  NULL,
    [Price] decimal(10,2)  NULL
);
GO

-- Creating table 'ProductXStoreXLocales'
CREATE TABLE [dbo].[ProductXStoreXLocales] (
    [ProductId] uniqueidentifier  NOT NULL,
    [StoreId] uniqueidentifier  NOT NULL,
    [LocaleId] int  NOT NULL,
    [Name] nvarchar(200)  NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Stores'
CREATE TABLE [dbo].[Stores] (
    [Id] uniqueidentifier  NOT NULL,
    [ChainId] int  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [LogoUrl] nvarchar(1024)  NULL,
    [Domain] nvarchar(200)  NULL,
    [DefaultLocaleId] int  NULL
);
GO

-- Creating table 'StoreLocale'
CREATE TABLE [dbo].[StoreLocale] (
    [Id] uniqueidentifier  NOT NULL,
    [StoreId] uniqueidentifier  NOT NULL,
    [LocaleId] int  NOT NULL,
    [SiteMap] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CategoryId], [ProductId] in table 'CategoryXProducts'
ALTER TABLE [dbo].[CategoryXProducts]
ADD CONSTRAINT [PK_CategoryXProducts]
    PRIMARY KEY CLUSTERED ([CategoryId], [ProductId] ASC);
GO

-- Creating primary key on [Id] in table 'Chains'
ALTER TABLE [dbo].[Chains]
ADD CONSTRAINT [PK_Chains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ProductId], [StoreId], [LocaleId] in table 'ProductXStoreXLocales'
ALTER TABLE [dbo].[ProductXStoreXLocales]
ADD CONSTRAINT [PK_ProductXStoreXLocales]
    PRIMARY KEY CLUSTERED ([ProductId], [StoreId], [LocaleId] ASC);
GO

-- Creating primary key on [Id] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [PK_Stores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [StoreId], [LocaleId] in table 'StoreLocale'
ALTER TABLE [dbo].[StoreLocale]
ADD CONSTRAINT [PK_StoreLocale]
    PRIMARY KEY CLUSTERED ([StoreId], [LocaleId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ChainId] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [FK_ChainStore]
    FOREIGN KEY ([ChainId])
    REFERENCES [dbo].[Chains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ChainStore'
CREATE INDEX [IX_FK_ChainStore]
ON [dbo].[Stores]
    ([ChainId]);
GO

-- Creating foreign key on [ChainId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ChainProduct]
    FOREIGN KEY ([ChainId])
    REFERENCES [dbo].[Chains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ChainProduct'
CREATE INDEX [IX_FK_ChainProduct]
ON [dbo].[Products]
    ([ChainId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductXStoreXLocales'
ALTER TABLE [dbo].[ProductXStoreXLocales]
ADD CONSTRAINT [FK_ProductProductXStoreXLocale]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [StoreId] in table 'StoreLocale'
ALTER TABLE [dbo].[StoreLocale]
ADD CONSTRAINT [FK_StoreStoreLocale]
    FOREIGN KEY ([StoreId])
    REFERENCES [dbo].[Stores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [StoreId], [LocaleId] in table 'ProductXStoreXLocales'
ALTER TABLE [dbo].[ProductXStoreXLocales]
ADD CONSTRAINT [FK_StoreLocaleProductXStoreXLocale]
    FOREIGN KEY ([StoreId], [LocaleId])
    REFERENCES [dbo].[StoreLocale]
        ([StoreId], [LocaleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreLocaleProductXStoreXLocale'
CREATE INDEX [IX_FK_StoreLocaleProductXStoreXLocale]
ON [dbo].[ProductXStoreXLocales]
    ([StoreId], [LocaleId]);
GO

-- Creating foreign key on [StoreId], [LocaleId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_StoreLocaleCategory]
    FOREIGN KEY ([StoreId], [LocaleId])
    REFERENCES [dbo].[StoreLocale]
        ([StoreId], [LocaleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreLocaleCategory'
CREATE INDEX [IX_FK_StoreLocaleCategory]
ON [dbo].[Categories]
    ([StoreId], [LocaleId]);
GO

-- Creating foreign key on [CategoryId] in table 'CategoryXProducts'
ALTER TABLE [dbo].[CategoryXProducts]
ADD CONSTRAINT [FK_CategoryCategoryXProduct]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'CategoryXProducts'
ALTER TABLE [dbo].[CategoryXProducts]
ADD CONSTRAINT [FK_ProductCategoryXProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategoryXProduct'
CREATE INDEX [IX_FK_ProductCategoryXProduct]
ON [dbo].[CategoryXProducts]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------