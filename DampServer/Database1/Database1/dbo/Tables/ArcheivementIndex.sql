CREATE TABLE [dbo].[ArcheivementIndex] (
    [userid]         BIGINT NOT NULL,
    [archeivementid] BIGINT NOT NULL,
    CONSTRAINT [pk_ArcheivementIndex] PRIMARY KEY CLUSTERED ([userid] ASC, [archeivementid] ASC),
    CONSTRAINT [fk_ArcheivementIndex] FOREIGN KEY ([userid]) REFERENCES [dbo].[Users] ([userid]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [fk_ArcheivementIndex2] FOREIGN KEY ([archeivementid]) REFERENCES [dbo].[Archeivements] ([archeivementid]) ON DELETE CASCADE ON UPDATE CASCADE
);

