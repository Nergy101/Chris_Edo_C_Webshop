
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/01/2018 23:21:24
-- Generated from EDMX file: C:\Users\Edo\source\repos\Chris_Edo_C_Webshop\WinkelServiceLibrary\WebShop.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StoreDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserPurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchases] DROP CONSTRAINT [FK_UserPurchase];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemPurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchases] DROP CONSTRAINT [FK_ItemPurchase];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Items];
GO
IF OBJECT_ID(N'[dbo].[Purchases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Purchases];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Saldo] float  NOT NULL
);
GO

-- Creating table 'Items'
CREATE TABLE [dbo].[Items] (
    [Name] nvarchar(50)  NOT NULL,
    [Price] float  NOT NULL,
    [Stock] smallint  NOT NULL
);
GO

-- Creating table 'Purchases'
CREATE TABLE [dbo].[Purchases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserUsername] nvarchar(50)  NOT NULL,
    [Amount] smallint  NOT NULL,
    [ItemName] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Username] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [Name] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [PK_Purchases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserUsername] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_UserPurchase]
    FOREIGN KEY ([UserUsername])
    REFERENCES [dbo].[Users]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPurchase'
CREATE INDEX [IX_FK_UserPurchase]
ON [dbo].[Purchases]
    ([UserUsername]);
GO

-- Creating foreign key on [ItemName] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_ItemPurchase]
    FOREIGN KEY ([ItemName])
    REFERENCES [dbo].[Items]
        ([Name])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemPurchase'
CREATE INDEX [IX_FK_ItemPurchase]
ON [dbo].[Purchases]
    ([ItemName]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------