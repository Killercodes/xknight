CREATE TABLE [dbo].[Hosts] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [CrawlId]         INT            NOT NULL,
    [HostName]        NVARCHAR (100) NOT NULL,
    [Port]            INT            NULL,
    [Status]          INT            NOT NULL,
    [LastVisit]       DATETIME       NULL,
    [IndexedPages]    INT            NOT NULL,
    [Time]            BIGINT         NOT NULL,
    [BytesDownloaded] BIGINT         NOT NULL,
    CONSTRAINT [PK_Hosts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Host_CrawlSetting] FOREIGN KEY ([CrawlId]) REFERENCES [dbo].[CrawlSettings] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Host_CrawlSetting]
    ON [dbo].[Hosts]([CrawlId] ASC);

