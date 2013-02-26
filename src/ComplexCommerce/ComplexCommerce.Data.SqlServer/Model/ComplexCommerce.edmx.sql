
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/26/2013 20:50:41
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

IF OBJECT_ID(N'[dbo].[FK_Category_ProductXCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryXProductXStoreLocale] DROP CONSTRAINT [FK_Category_ProductXCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Chain_Store]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Store] DROP CONSTRAINT [FK_Chain_Store];
GO
IF OBJECT_ID(N'[dbo].[FK_ChainProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_ChainProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_ProductXStoreLocale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductXStoreLocale] DROP CONSTRAINT [FK_Product_ProductXStoreLocale];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductXStoreLocale_CategoryXProductXStoreLocale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryXProductXStoreLocale] DROP CONSTRAINT [FK_ProductXStoreLocale_CategoryXProductXStoreLocale];
GO
IF OBJECT_ID(N'[dbo].[FK_Store_StoreLocale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreLocale] DROP CONSTRAINT [FK_Store_StoreLocale];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreLocale_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Category] DROP CONSTRAINT [FK_StoreLocale_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreLocale_ProductXStoreLocale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductXStoreLocale] DROP CONSTRAINT [FK_StoreLocale_ProductXStoreLocale];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[CategoryXProductXStoreLocale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryXProductXStoreLocale];
GO
IF OBJECT_ID(N'[dbo].[Chain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chain];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductXStoreLocale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductXStoreLocale];
GO
IF OBJECT_ID(N'[dbo].[Store]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Store];
GO
IF OBJECT_ID(N'[dbo].[StoreLocale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreLocale];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [Id] uniqueidentifier  NOT NULL,
    [StoreLocaleId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [RouteUrl] nvarchar(1024)  NOT NULL
);
GO

-- Creating table 'CategoryXProductXStoreLocale'
CREATE TABLE [dbo].[CategoryXProductXStoreLocale] (
    [Id] uniqueidentifier  NOT NULL,
    [CategoryId] uniqueidentifier  NOT NULL,
    [ProductXStoreLocaleId] uniqueidentifier  NOT NULL,
    [RouteUrl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Chain'
CREATE TABLE [dbo].[Chain] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Description] nvarchar(1000)  NULL
);
GO

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [Id] uniqueidentifier  NOT NULL,
    [ChainId] int  NOT NULL,
    [SKU] nvarchar(100)  NULL,
    [ImageUrl] nvarchar(1024)  NULL,
    [Price] decimal(10,2)  NULL
);
GO

-- Creating table 'ProductXStoreLocale'
CREATE TABLE [dbo].[ProductXStoreLocale] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [StoreLocaleId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(200)  NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Store'
CREATE TABLE [dbo].[Store] (
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

-- Creating table 'Page'
CREATE TABLE [dbo].[Page] (
    [Id] uniqueidentifier  NOT NULL,
    [RouteUrl] nvarchar(1024)  NOT NULL,
    [ContentTypeId] int  NOT NULL,
    [ContentId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategoryXProductXStoreLocale'
ALTER TABLE [dbo].[CategoryXProductXStoreLocale]
ADD CONSTRAINT [PK_CategoryXProductXStoreLocale]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Chain'
ALTER TABLE [dbo].[Chain]
ADD CONSTRAINT [PK_Chain]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductXStoreLocale'
ALTER TABLE [dbo].[ProductXStoreLocale]
ADD CONSTRAINT [PK_ProductXStoreLocale]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Store'
ALTER TABLE [dbo].[Store]
ADD CONSTRAINT [PK_Store]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreLocale'
ALTER TABLE [dbo].[StoreLocale]
ADD CONSTRAINT [PK_StoreLocale]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Page'
ALTER TABLE [dbo].[Page]
ADD CONSTRAINT [PK_Page]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'CategoryXProductXStoreLocale'
ALTER TABLE [dbo].[CategoryXProductXStoreLocale]
ADD CONSTRAINT [FK_Category_ProductXCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Category]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Category_ProductXCategory'
CREATE INDEX [IX_FK_Category_ProductXCategory]
ON [dbo].[CategoryXProductXStoreLocale]
    ([CategoryId]);
GO

-- Creating foreign key on [StoreLocaleId] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [FK_StoreLocale_Category]
    FOREIGN KEY ([StoreLocaleId])
    REFERENCES [dbo].[StoreLocale]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreLocale_Category'
CREATE INDEX [IX_FK_StoreLocale_Category]
ON [dbo].[Category]
    ([StoreLocaleId]);
GO

-- Creating foreign key on [ProductXStoreLocaleId] in table 'CategoryXProductXStoreLocale'
ALTER TABLE [dbo].[CategoryXProductXStoreLocale]
ADD CONSTRAINT [FK_ProductXStoreLocale_CategoryXProductXStoreLocale]
    FOREIGN KEY ([ProductXStoreLocaleId])
    REFERENCES [dbo].[ProductXStoreLocale]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductXStoreLocale_CategoryXProductXStoreLocale'
CREATE INDEX [IX_FK_ProductXStoreLocale_CategoryXProductXStoreLocale]
ON [dbo].[CategoryXProductXStoreLocale]
    ([ProductXStoreLocaleId]);
GO

-- Creating foreign key on [ChainId] in table 'Store'
ALTER TABLE [dbo].[Store]
ADD CONSTRAINT [FK_Chain_Store]
    FOREIGN KEY ([ChainId])
    REFERENCES [dbo].[Chain]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Chain_Store'
CREATE INDEX [IX_FK_Chain_Store]
ON [dbo].[Store]
    ([ChainId]);
GO

-- Creating foreign key on [ChainId] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [FK_ChainProduct]
    FOREIGN KEY ([ChainId])
    REFERENCES [dbo].[Chain]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ChainProduct'
CREATE INDEX [IX_FK_ChainProduct]
ON [dbo].[Product]
    ([ChainId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductXStoreLocale'
ALTER TABLE [dbo].[ProductXStoreLocale]
ADD CONSTRAINT [FK_Product_ProductXStoreLocale]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Product]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_ProductXStoreLocale'
CREATE INDEX [IX_FK_Product_ProductXStoreLocale]
ON [dbo].[ProductXStoreLocale]
    ([ProductId]);
GO

-- Creating foreign key on [StoreLocaleId] in table 'ProductXStoreLocale'
ALTER TABLE [dbo].[ProductXStoreLocale]
ADD CONSTRAINT [FK_StoreLocale_ProductXStoreLocale]
    FOREIGN KEY ([StoreLocaleId])
    REFERENCES [dbo].[StoreLocale]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreLocale_ProductXStoreLocale'
CREATE INDEX [IX_FK_StoreLocale_ProductXStoreLocale]
ON [dbo].[ProductXStoreLocale]
    ([StoreLocaleId]);
GO

-- Creating foreign key on [StoreId] in table 'StoreLocale'
ALTER TABLE [dbo].[StoreLocale]
ADD CONSTRAINT [FK_Store_StoreLocale]
    FOREIGN KEY ([StoreId])
    REFERENCES [dbo].[Store]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Store_StoreLocale'
CREATE INDEX [IX_FK_Store_StoreLocale]
ON [dbo].[StoreLocale]
    ([StoreId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------