CREATE TABLE [dbo].[Games] (
    [gameid]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [title]          NVARCHAR (32) NOT NULL,
    [path]           NVARCHAR (32) NOT NULL,
    [description]    TEXT          NOT NULL,
    [picture]        NVARCHAR (32) NOT NULL,
    [genre]          NVARCHAR (32) NOT NULL,
    [recommendedage] INT           NOT NULL,
    [developer]      NVARCHAR (32) NOT NULL,
    CONSTRAINT [pk_Games] PRIMARY KEY CLUSTERED ([gameid] ASC)
);

