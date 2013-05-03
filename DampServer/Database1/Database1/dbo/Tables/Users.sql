CREATE TABLE [dbo].[Users] (
    [userid]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [username]  NVARCHAR (32) NOT NULL,
    [password]  NVARCHAR (32) NOT NULL,
    [email]     NVARCHAR (32) NOT NULL,
    [authToken] NVARCHAR (40) NULL,
    CONSTRAINT [pk_Users] PRIMARY KEY CLUSTERED ([userid] ASC),
    UNIQUE NONCLUSTERED ([username] ASC)
);

