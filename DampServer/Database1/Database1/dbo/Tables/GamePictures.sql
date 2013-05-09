CREATE TABLE [dbo].[GamePictures]
(
	[id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [picture] NCHAR(32) NOT NULL, 
    [gameid] BIGINT NOT NULL
	)
