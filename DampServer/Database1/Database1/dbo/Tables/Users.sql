CREATE TABLE [dbo].[Users] (
    [userid]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [username]  NVARCHAR (32) NOT NULL,
    [password]  NVARCHAR (32) NOT NULL,
    [email]     NVARCHAR (32) NOT NULL,
    [authToken] NVARCHAR (40) NULL,
    [country] NCHAR(32) NULL , 
    [language] NCHAR(32) NULL , 
    [city] NCHAR(32) NULL , 
    [gender] NCHAR(10) NULL , 
    [photo] NCHAR(32) NULL , 
    [description] TEXT NULL , 
    CONSTRAINT [pk_Users] PRIMARY KEY CLUSTERED ([userid] ASC),
    UNIQUE NONCLUSTERED ([username] ASC)
);

