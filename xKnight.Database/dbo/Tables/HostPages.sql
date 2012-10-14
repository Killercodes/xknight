CREATE TABLE [dbo].[HostPages] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [HostId]    INT            NOT NULL,
    [Url]       NVARCHAR (MAX) NOT NULL,
    [Html]      NVARCHAR (MAX) NOT NULL,
    [Depth]     INT            NOT NULL,
    [DateTime]  DATETIME       NOT NULL,
    [RefererId] INT            NULL,
    CONSTRAINT [PK_HostPages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HostPage_Host] FOREIGN KEY ([HostId]) REFERENCES [dbo].[Hosts] ([Id]),
    CONSTRAINT [FK_HostPage_HostPage] FOREIGN KEY ([RefererId]) REFERENCES [dbo].[HostPages] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_HostPage_Host]
    ON [dbo].[HostPages]([HostId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_HostPage_HostPage]
    ON [dbo].[HostPages]([RefererId] ASC);

