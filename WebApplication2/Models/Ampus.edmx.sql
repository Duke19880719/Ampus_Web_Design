
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/24/2019 17:43:54
-- Generated from EDMX file: C:\Users\wolfs\source\repos\WebApplication2\WebApplication2\Models\Ampus.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [test_shopping];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Order_Customer1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Customer1];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_GoodsInformation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_GoodsInformation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[GoodsInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodsInformation];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customer'
CREATE TABLE [dbo].[Customer] (
    [CustomerId] nchar(10)  NOT NULL,
    [CustomerName] nchar(10)  NULL,
    [CompanyName] nchar(10)  NULL,
    [Gender] nchar(10)  NULL,
    [PhoneNumber] int  NULL,
    [Adress] nvarchar(50)  NULL,
    [Account] nchar(10)  NULL,
    [Password] nchar(10)  NULL
);
GO

-- Creating table 'GoodsInformation'
CREATE TABLE [dbo].[GoodsInformation] (
    [GoodsId] nchar(10)  NOT NULL,
    [GoodsName] nchar(10)  NULL,
    [Price] int  NULL
);
GO

-- Creating table 'Order'
CREATE TABLE [dbo].[Order] (
    [OrderId] nchar(10)  NOT NULL,
    [DateBuy] nchar(10)  NULL,
    [CustomerId] nchar(10)  NULL,
    [GoodsId] nchar(10)  NULL,
    [Amount] nchar(10)  NULL,
    [TotalPrice] nchar(10)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CustomerId] in table 'Customer'
ALTER TABLE [dbo].[Customer]
ADD CONSTRAINT [PK_Customer]
    PRIMARY KEY CLUSTERED ([CustomerId] ASC);
GO

-- Creating primary key on [GoodsId] in table 'GoodsInformation'
ALTER TABLE [dbo].[GoodsInformation]
ADD CONSTRAINT [PK_GoodsInformation]
    PRIMARY KEY CLUSTERED ([GoodsId] ASC);
GO

-- Creating primary key on [OrderId] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [PK_Order]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerId] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_Order_Customer1]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customer]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Customer1'
CREATE INDEX [IX_FK_Order_Customer1]
ON [dbo].[Order]
    ([CustomerId]);
GO

-- Creating foreign key on [GoodsId] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_Order_GoodsInformation]
    FOREIGN KEY ([GoodsId])
    REFERENCES [dbo].[GoodsInformation]
        ([GoodsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_GoodsInformation'
CREATE INDEX [IX_FK_Order_GoodsInformation]
ON [dbo].[Order]
    ([GoodsId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------