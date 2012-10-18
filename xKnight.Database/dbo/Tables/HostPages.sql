CREATE TABLE [dbo].[Webpages] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [HostId]    INT            NOT NULL,
    [Url]       NVARCHAR (MAX) NOT NULL,
    [Html]      NVARCHAR (MAX) NOT NULL,
    [Depth]     INT            NOT NULL,
    [DateTime]  DATETIME       NOT NULL,
    [RefererId] INT            NULL,
    CONSTRAINT [PK_Webpages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Webpage_Host] FOREIGN KEY ([HostId]) REFERENCES [dbo].[Hosts] ([Id]),
    CONSTRAINT [FK_Webpage_Webpage] FOREIGN KEY ([RefererId]) REFERENCES [dbo].[Webpages] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Webpage_Host]
    ON [dbo].[Webpages]([HostId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Webpage_Webpage]
    ON [dbo].[Webpages]([RefererId] ASC);

