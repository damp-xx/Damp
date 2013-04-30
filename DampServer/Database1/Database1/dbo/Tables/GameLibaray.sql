CREATE TABLE [dbo].[GameLibaray] (
    [userid] BIGINT NOT NULL,
    [gameid] BIGINT NOT NULL,
    CONSTRAINT [pk_GameLibaray] PRIMARY KEY CLUSTERED ([userid] ASC, [gameid] ASC),
    CONSTRAINT [fk_GameLibaray] FOREIGN KEY ([userid]) REFERENCES [dbo].[Users] ([userid]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [fk_GameLibaray2] FOREIGN KEY ([gameid]) REFERENCES [dbo].[Games] ([gameid]) ON DELETE CASCADE ON UPDATE CASCADE
);

