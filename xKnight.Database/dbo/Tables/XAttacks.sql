CREATE TABLE [dbo].[XAttacks] (
    [Id]            INT            NOT NULL,
    [AttackId]      INT            NOT NULL,
    [FormId]        INT            NOT NULL,
    [AttackContent] NVARCHAR (MAX) NOT NULL,
    [ResponsePage]  NVARCHAR (MAX) NOT NULL,
    [xAttackType]   INT            NOT NULL,
    CONSTRAINT [PK_XAttacks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_XAttack_Attack] FOREIGN KEY ([AttackId]) REFERENCES [dbo].[Attacks] ([Id]),
    CONSTRAINT [FK_XAttack_Form] FOREIGN KEY ([FormId]) REFERENCES [dbo].[Forms] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_XAttack_Attack]
    ON [dbo].[XAttacks]([AttackId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_XAttack_Form]
    ON [dbo].[XAttacks]([FormId] ASC);

