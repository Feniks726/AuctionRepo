
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2016 16:34:25
-- Generated from EDMX file: C:\Users\alexey.chalmov\Documents\Visual Studio 2015\Projects\Auction\Auction.DAL\Data\AuctionDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AuctionDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LotAuction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auctions] DROP CONSTRAINT [FK_LotAuction];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAuction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auctions] DROP CONSTRAINT [FK_UserAuction];
GO
IF OBJECT_ID(N'[dbo].[FK_AuctionTemplateLot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lots] DROP CONSTRAINT [FK_AuctionTemplateLot];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAuctionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuctionTemplates] DROP CONSTRAINT [FK_UserAuctionTemplate];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Auctions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Auctions];
GO
IF OBJECT_ID(N'[dbo].[AuctionTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuctionTemplates];
GO
IF OBJECT_ID(N'[dbo].[Lots]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lots];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Auctions'
CREATE TABLE [dbo].[Auctions] (
    [Id] uniqueidentifier  NOT NULL,
    [LotId] uniqueidentifier  NOT NULL,
    [UserId] bigint  NOT NULL,
    [Rate] decimal(18,0)  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifidedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'AuctionTemplates'
CREATE TABLE [dbo].[AuctionTemplates] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifidedDateTime] datetime  NOT NULL,
    [IsStopped] bit  NOT NULL,
    [AuthorID] bigint  NOT NULL,
    [StartedDateTime] datetime  NULL
);
GO

-- Creating table 'Lots'
CREATE TABLE [dbo].[Lots] (
    [Id] uniqueidentifier  NOT NULL,
    [AuctionTemplateId] uniqueidentifier  NOT NULL,
    [Context] nvarchar(max)  NOT NULL,
    [Duration] datetime  NULL,
    [StartingPrice] decimal(18,0)  NOT NULL,
    [Direction] smallint  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifidedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [PK_Auctions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuctionTemplates'
ALTER TABLE [dbo].[AuctionTemplates]
ADD CONSTRAINT [PK_AuctionTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Lots'
ALTER TABLE [dbo].[Lots]
ADD CONSTRAINT [PK_Lots]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LotId] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [FK_LotAuction]
    FOREIGN KEY ([LotId])
    REFERENCES [dbo].[Lots]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LotAuction'
CREATE INDEX [IX_FK_LotAuction]
ON [dbo].[Auctions]
    ([LotId]);
GO

-- Creating foreign key on [UserId] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [FK_UserAuction]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAuction'
CREATE INDEX [IX_FK_UserAuction]
ON [dbo].[Auctions]
    ([UserId]);
GO

-- Creating foreign key on [AuctionTemplateId] in table 'Lots'
ALTER TABLE [dbo].[Lots]
ADD CONSTRAINT [FK_AuctionTemplateLot]
    FOREIGN KEY ([AuctionTemplateId])
    REFERENCES [dbo].[AuctionTemplates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuctionTemplateLot'
CREATE INDEX [IX_FK_AuctionTemplateLot]
ON [dbo].[Lots]
    ([AuctionTemplateId]);
GO

-- Creating foreign key on [AuthorID] in table 'AuctionTemplates'
ALTER TABLE [dbo].[AuctionTemplates]
ADD CONSTRAINT [FK_UserAuctionTemplate]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAuctionTemplate'
CREATE INDEX [IX_FK_UserAuctionTemplate]
ON [dbo].[AuctionTemplates]
    ([AuthorID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------