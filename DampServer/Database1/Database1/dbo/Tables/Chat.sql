CREATE TABLE [dbo].[Chat] (
    [chatId]   BIGINT   NOT NULL IDENTITY,
    [sender]   BIGINT   NOT NULL,
    [receiver] BIGINT   NOT NULL,
    [time]     DATETIME NOT NULL DEFAULT getdate(),
    [seen]     SMALLINT NOT NULL,
    [message]  TEXT     NULL, 
    CONSTRAINT [PK_Chat] PRIMARY KEY ([chatId])
);

