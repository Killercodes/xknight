
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/18/2012 22:06:06
-- Generated from EDMX file: C:\Users\Ehsan Mirsaeedi\Documents\Visual Studio 2010\Projects\xKnight\xKnight.Models\xKnightModel.edmx
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
    ALTER TABLE [dbo].[Webpages] DROP CONSTRAINT [FK_HostPage_Host];
GO
IF OBJECT_ID(N'[dbo].[FK_HostPage_HostPage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Webpages] DROP CONSTRAINT [FK_HostPage_HostPage];
GO
IF OBJECT_ID(N'[dbo].[FK_FormElementForm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FormElements] DROP CONSTRAINT [FK_FormElementForm];
GO
IF OBJECT_ID(N'[dbo].[FK_AttackCrawlSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attacks] DROP CONSTRAINT [FK_AttackCrawlSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_XAttackParamFormElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[XAttackParams] DROP CONSTRAINT [FK_XAttackParamFormElement];
GO
IF OBJECT_ID(N'[dbo].[FK_XAttackParamXAttack]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[XAttackParams] DROP CONSTRAINT [FK_XAttackParamXAttack];
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
IF OBJECT_ID(N'[dbo].[Webpages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Webpages];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[XAttacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[XAttacks];
GO
IF OBJECT_ID(N'[dbo].[FormElements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FormElements];
GO
IF OBJECT_ID(N'[dbo].[XAttackParams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[XAttackParams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attacks'
CREATE TABLE [dbo].[Attacks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AttackType] int  NOT NULL,
    [StartTime] datetime  NULL,
    [FinishTime] datetime  NULL,
    [HostId] int  NOT NULL,
    [CrawlSettingId] int  NOT NULL
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
    [WebpageId] int  NOT NULL,
    [Action] nvarchar(max)  NOT NULL,
    [Method] varchar(50)  NOT NULL
);
GO

-- Creating table 'Hosts'
CREATE TABLE [dbo].[Hosts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CrawlId] int  NOT NULL,
    [HostName] nvarchar(100)  NOT NULL,
    [IndexedPages] int  NOT NULL,
    [BytesDownloaded] bigint  NOT NULL,
    [StartTime] datetime  NULL,
    [FinishTime] datetime  NULL
);
GO

-- Creating table 'Webpages'
CREATE TABLE [dbo].[Webpages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HostId] int  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Html] nvarchar(max)  NULL,
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
    [StartTime] datetime  NULL,
    [FinishTime] datetime  NULL
);
GO

-- Creating table 'FormElements'
CREATE TABLE [dbo].[FormElements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Value] nvarchar(max)  NULL,
    [Type] nvarchar(max)  NULL,
    [FormId] int  NOT NULL
);
GO

-- Creating table 'XAttackParams'
CREATE TABLE [dbo].[XAttackParams] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NULL,
    [FormElementId] int  NOT NULL,
    [XAttackId] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Webpages'
ALTER TABLE [dbo].[Webpages]
ADD CONSTRAINT [PK_Webpages]
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

-- Creating primary key on [Id] in table 'FormElements'
ALTER TABLE [dbo].[FormElements]
ADD CONSTRAINT [PK_FormElements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'XAttackParams'
ALTER TABLE [dbo].[XAttackParams]
ADD CONSTRAINT [PK_XAttackParams]
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

-- Creating foreign key on [WebpageId] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_Form_HostPage]
    FOREIGN KEY ([WebpageId])
    REFERENCES [dbo].[Webpages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Form_HostPage'
CREATE INDEX [IX_FK_Form_HostPage]
ON [dbo].[Forms]
    ([WebpageId]);
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

-- Creating foreign key on [HostId] in table 'Webpages'
ALTER TABLE [dbo].[Webpages]
ADD CONSTRAINT [FK_HostPage_Host]
    FOREIGN KEY ([HostId])
    REFERENCES [dbo].[Hosts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HostPage_Host'
CREATE INDEX [IX_FK_HostPage_Host]
ON [dbo].[Webpages]
    ([HostId]);
GO

-- Creating foreign key on [RefererId] in table 'Webpages'
ALTER TABLE [dbo].[Webpages]
ADD CONSTRAINT [FK_HostPage_HostPage]
    FOREIGN KEY ([RefererId])
    REFERENCES [dbo].[Webpages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HostPage_HostPage'
CREATE INDEX [IX_FK_HostPage_HostPage]
ON [dbo].[Webpages]
    ([RefererId]);
GO

-- Creating foreign key on [FormId] in table 'FormElements'
ALTER TABLE [dbo].[FormElements]
ADD CONSTRAINT [FK_FormElementForm]
    FOREIGN KEY ([FormId])
    REFERENCES [dbo].[Forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FormElementForm'
CREATE INDEX [IX_FK_FormElementForm]
ON [dbo].[FormElements]
    ([FormId]);
GO

-- Creating foreign key on [CrawlSettingId] in table 'Attacks'
ALTER TABLE [dbo].[Attacks]
ADD CONSTRAINT [FK_AttackCrawlSetting]
    FOREIGN KEY ([CrawlSettingId])
    REFERENCES [dbo].[CrawlSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AttackCrawlSetting'
CREATE INDEX [IX_FK_AttackCrawlSetting]
ON [dbo].[Attacks]
    ([CrawlSettingId]);
GO

-- Creating foreign key on [FormElementId] in table 'XAttackParams'
ALTER TABLE [dbo].[XAttackParams]
ADD CONSTRAINT [FK_XAttackParamFormElement]
    FOREIGN KEY ([FormElementId])
    REFERENCES [dbo].[FormElements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_XAttackParamFormElement'
CREATE INDEX [IX_FK_XAttackParamFormElement]
ON [dbo].[XAttackParams]
    ([FormElementId]);
GO

-- Creating foreign key on [XAttackId] in table 'XAttackParams'
ALTER TABLE [dbo].[XAttackParams]
ADD CONSTRAINT [FK_XAttackParamXAttack]
    FOREIGN KEY ([XAttackId])
    REFERENCES [dbo].[XAttacks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_XAttackParamXAttack'
CREATE INDEX [IX_FK_XAttackParamXAttack]
ON [dbo].[XAttackParams]
    ([XAttackId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------