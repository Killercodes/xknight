
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/22/2012 11:40:21
-- Generated from EDMX file: c:\users\ehsan mirsaeedi\documents\visual studio 2010\Projects\xKnight\xKnight.Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [xKnight];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_XAttack_Attack]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[XAttacks] DROP CONSTRAINT [FK_XAttack_Attack];
GO
IF OBJECT_ID(N'[dbo].[FK_Host_CrawlSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Hosts] DROP CONSTRAINT [FK_Host_CrawlSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_Form_HostPage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Forms] DROP CONSTRAINT [FK_Form_HostPage];
GO
IF OBJECT_ID(N'[dbo].[FK_XAttack_Form]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[XAttacks] DROP CONSTRAINT [FK_XAttack_Form];
GO
IF OBJECT_ID(N'[dbo].[FK_HostPage_Host]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HostPages] DROP CONSTRAINT [FK_HostPage_Host];
GO
IF OBJECT_ID(N'[dbo].[FK_HostPage_HostPage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HostPages] DROP CONSTRAINT [FK_HostPage_HostPage];
GO
IF OBJECT_ID(N'[dbo].[FK_AttackHost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attacks] DROP CONSTRAINT [FK_AttackHost];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Attacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attacks];
GO
IF OBJECT_ID(N'[dbo].[CrawlSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CrawlSettings];
GO
IF OBJECT_ID(N'[dbo].[Forms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Forms];
GO
IF OBJECT_ID(N'[dbo].[Hosts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hosts];
GO
IF OBJECT_ID(N'[dbo].[HostPages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HostPages];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[XAttacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[XAttacks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attacks'
CREATE TABLE [dbo].[Attacks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AttackType] nvarchar(max)  NOT NULL,
    [StartTime] nchar(10)  NULL,
    [FinishTime] nchar(10)  NULL,
    [Succeed] bit  NOT NULL,
    [HostId] int  NOT NULL
);
GO

-- Creating table 'CrawlSettings'
CREATE TABLE [dbo].[CrawlSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MaxPages] int  NULL,
    [MaxDepth] int  NULL,
    [MaxTime] bigint  NULL,
    [MaxByte] bigint  NULL,
    [StartTime] datetime  NULL,
    [FinishTime] datetime  NULL
);
GO

-- Creating table 'Forms'
CREATE TABLE [dbo].[Forms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PageId] int  NOT NULL,
    [FormContents] nvarchar(max)  NOT NULL,
    [Action] nvarchar(max)  NOT NULL,
    [Method] varchar(50)  NOT NULL
);
GO

-- Creating table 'Hosts'
CREATE TABLE [dbo].[Hosts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CrawlId] int  NOT NULL,
    [HostName] nvarchar(100)  NOT NULL,
    [Port] int  NOT NULL,
    [Status] int  NOT NULL,
    [LastVisit] datetime  NULL,
    [IndexedPages] int  NOT NULL,
    [Time] bigint  NOT NULL,
    [BytesDownloaded] bigint  NOT NULL
);
GO

-- Creating table 'HostPages'
CREATE TABLE [dbo].[HostPages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HostId] int  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Html] nvarchar(max)  NOT NULL,
    [Depth] int  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [RefererId] int  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'XAttacks'
CREATE TABLE [dbo].[XAttacks] (
    [Id] int  NOT NULL,
    [AttackId] int  NOT NULL,
    [FormId] int  NOT NULL,
    [AttackContent] nvarchar(max)  NOT NULL,
    [ResponsePage] nvarchar(max)  NOT NULL,
    [xAttackType] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Attacks'
ALTER TABLE [dbo].[Attacks]
ADD CONSTRAINT [PK_Attacks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CrawlSettings'
ALTER TABLE [dbo].[CrawlSettings]
ADD CONSTRAINT [PK_CrawlSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [PK_Forms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Hosts'
ALTER TABLE [dbo].[Hosts]
ADD CONSTRAINT [PK_Hosts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HostPages'
ALTER TABLE [dbo].[HostPages]
ADD CONSTRAINT [PK_HostPages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'XAttacks'
ALTER TABLE [dbo].[XAttacks]
ADD CONSTRAINT [PK_XAttacks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AttackId] in table 'XAttacks'
ALTER TABLE [dbo].[XAttacks]
ADD CONSTRAINT [FK_XAttack_Attack]
    FOREIGN KEY ([AttackId])
    REFERENCES [dbo].[Attacks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_XAttack_Attack'
CREATE INDEX [IX_FK_XAttack_Attack]
ON [dbo].[XAttacks]
    ([AttackId]);
GO

-- Creating foreign key on [CrawlId] in table 'Hosts'
ALTER TABLE [dbo].[Hosts]
ADD CONSTRAINT [FK_Host_CrawlSetting]
    FOREIGN KEY ([CrawlId])
    REFERENCES [dbo].[CrawlSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Host_CrawlSetting'
CREATE INDEX [IX_FK_Host_CrawlSetting]
ON [dbo].[Hosts]
    ([CrawlId]);
GO

-- Creating foreign key on [PageId] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_Form_HostPage]
    FOREIGN KEY ([PageId])
    REFERENCES [dbo].[HostPages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Form_HostPage'
CREATE INDEX [IX_FK_Form_HostPage]
ON [dbo].[Forms]
    ([PageId]);
GO

-- Creating foreign key on [FormId] in table 'XAttacks'
ALTER TABLE [dbo].[XAttacks]
ADD CONSTRAINT [FK_XAttack_Form]
    FOREIGN KEY ([FormId])
    REFERENCES [dbo].[Forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_XAttack_Form'
CREATE INDEX [IX_FK_XAttack_Form]
ON [dbo].[XAttacks]
    ([FormId]);
GO

-- Creating foreign key on [HostId] in table 'HostPages'
ALTER TABLE [dbo].[HostPages]
ADD CONSTRAINT [FK_HostPage_Host]
    FOREIGN KEY ([HostId])
    REFERENCES [dbo].[Hosts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HostPage_Host'
CREATE INDEX [IX_FK_HostPage_Host]
ON [dbo].[HostPages]
    ([HostId]);
GO

-- Creating foreign key on [RefererId] in table 'HostPages'
ALTER TABLE [dbo].[HostPages]
ADD CONSTRAINT [FK_HostPage_HostPage]
    FOREIGN KEY ([RefererId])
    REFERENCES [dbo].[HostPages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HostPage_HostPage'
CREATE INDEX [IX_FK_HostPage_HostPage]
ON [dbo].[HostPages]
    ([RefererId]);
GO

-- Creating foreign key on [HostId] in table 'Attacks'
ALTER TABLE [dbo].[Attacks]
ADD CONSTRAINT [FK_AttackHost]
    FOREIGN KEY ([HostId])
    REFERENCES [dbo].[Hosts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AttackHost'
CREATE INDEX [IX_FK_AttackHost]
ON [dbo].[Attacks]
    ([HostId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------