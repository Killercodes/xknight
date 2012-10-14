CREATE TABLE [dbo].[CrawlSettings] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [MaxPages]   INT      NULL,
    [MaxDepth]   INT      NULL,
    [MaxTime]    BIGINT   NULL,
    [MaxByte]    BIGINT   NULL,
    [StartTime]  DATETIME NULL,
    [FinishTime] DATETIME NULL,
    CONSTRAINT [PK_CrawlSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

