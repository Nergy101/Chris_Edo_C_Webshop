
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/30/2018 15:59:17
-- Generated from EDMX file: C:\Users\Edo\source\repos\Chris_Edo_C_Webshop\WinkelServiceLibrary\Linq2Entity.edmx
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

IF OBJECT_ID(N'[dbo].[FK_USERPURCHASE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PURCHASEs] DROP CONSTRAINT [FK_USERPURCHASE];
GO
IF OBJECT_ID(N'[dbo].[FK_PURCHASEITEM]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ITEMs] DROP CONSTRAINT [FK_PURCHASEITEM];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[USERs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[USERs];
GO
IF OBJECT_ID(N'[dbo].[ITEMs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ITEMs];
GO
IF OBJECT_ID(N'[dbo].[PURCHASEs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PURCHASEs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'USERs'
CREATE TABLE [dbo].[USERs] (
    [username] nvarchar(50)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [saldo] float  NOT NULL
);
GO

-- Creating table 'ITEMs'
CREATE TABLE [dbo].[ITEMs] (
    [name] nvarchar(50)  NOT NULL,
    [price] smallint  NOT NULL,
    [stock] smallint  NOT NULL,
    [PURCHASE_id] smallint  NOT NULL
);
GO

-- Creating table 'PURCHASEs'
CREATE TABLE [dbo].[PURCHASEs] (
    [amount] smallint  NOT NULL,
    [id] smallint  NOT NULL,
    [USER_username] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [username] in table 'USERs'
ALTER TABLE [dbo].[USERs]
ADD CONSTRAINT [PK_USERs]
    PRIMARY KEY CLUSTERED ([username] ASC);
GO

-- Creating primary key on [name] in table 'ITEMs'
ALTER TABLE [dbo].[ITEMs]
ADD CONSTRAINT [PK_ITEMs]
    PRIMARY KEY CLUSTERED ([name] ASC);
GO

-- Creating primary key on [id] in table 'PURCHASEs'
ALTER TABLE [dbo].[PURCHASEs]
ADD CONSTRAINT [PK_PURCHASEs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [USER_username] in table 'PURCHASEs'
ALTER TABLE [dbo].[PURCHASEs]
ADD CONSTRAINT [FK_USERPURCHASE]
    FOREIGN KEY ([USER_username])
    REFERENCES [dbo].[USERs]
        ([username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERPURCHASE'
CREATE INDEX [IX_FK_USERPURCHASE]
ON [dbo].[PURCHASEs]
    ([USER_username]);
GO

-- Creating foreign key on [PURCHASE_id] in table 'ITEMs'
ALTER TABLE [dbo].[ITEMs]
ADD CONSTRAINT [FK_PURCHASEITEM]
    FOREIGN KEY ([PURCHASE_id])
    REFERENCES [dbo].[PURCHASEs]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PURCHASEITEM'
CREATE INDEX [IX_FK_PURCHASEITEM]
ON [dbo].[ITEMs]
    ([PURCHASE_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------