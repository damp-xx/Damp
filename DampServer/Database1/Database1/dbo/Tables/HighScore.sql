CREATE TABLE [dbo].[HighScore]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [gameid] BIGINT NOT NULL, 
    [userid] BIGINT NOT NULL, 
    [score] BIGINT NOT NULL
)
