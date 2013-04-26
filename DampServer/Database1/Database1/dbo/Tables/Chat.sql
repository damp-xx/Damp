CREATE TABLE [dbo].[Chat] (
    [chatId]   BIGINT   NULL,
    [sender]   BIGINT   NOT NULL,
    [receiver] BIGINT   NOT NULL,
    [time]     DATETIME NOT NULL,
    [seen]     SMALLINT NOT NULL,
    [message]  TEXT     NULL
);

