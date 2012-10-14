CREATE TABLE [dbo].[Attacks] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [AttackType] NVARCHAR (MAX) NOT NULL,
    [StartTime]  NCHAR (10)     NULL,
    [FinishTime] NCHAR (10)     NULL,
    [Succeed]    BIT            NOT NULL,
    [HostId]     INT            NOT NULL,
    CONSTRAINT [PK_Attacks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttackHost] FOREIGN KEY ([HostId]) REFERENCES [dbo].[Hosts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_AttackHost]
    ON [dbo].[Attacks]([HostId] ASC);

