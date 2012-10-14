CREATE TABLE [dbo].[Forms] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [PageId]       INT            NOT NULL,
    [FormContents] NVARCHAR (MAX) NOT NULL,
    [Action]       NVARCHAR (MAX) NOT NULL,
    [Method]       VARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_Forms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Form_HostPage] FOREIGN KEY ([PageId]) REFERENCES [dbo].[HostPages] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Form_HostPage]
    ON [dbo].[Forms]([PageId] ASC);

