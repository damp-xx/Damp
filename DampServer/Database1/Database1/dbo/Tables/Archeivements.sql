CREATE TABLE [dbo].[Archeivements] (
    [archeivementid] BIGINT        IDENTITY (1, 1) NOT NULL,
    [title]          NVARCHAR (32) NOT NULL,
    [description]    TEXT          NOT NULL,
    [gameid]         BIGINT        NOT NULL,
    [picturePath]    NVARCHAR (32) NOT NULL,
    [gameid1]        BIGINT        NULL,
    CONSTRAINT [pk_Archeivements] PRIMARY KEY CLUSTERED ([archeivementid] ASC),
    CONSTRAINT [fk_Archeivements] FOREIGN KEY ([gameid1]) REFERENCES [dbo].[Games] ([gameid]) ON UPDATE CASCADE
);

