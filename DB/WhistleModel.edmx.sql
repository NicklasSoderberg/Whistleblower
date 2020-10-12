
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2020 09:56:18
-- Generated from EDMX file: C:\Users\Gaming\source\repos\Whistleblower\DB\WhistleModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WhistleDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Conversation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conversation];
GO
IF OBJECT_ID(N'[dbo].[File]', 'U') IS NOT NULL
    DROP TABLE [dbo].[File];
GO
IF OBJECT_ID(N'[dbo].[Lawyer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lawyer];
GO
IF OBJECT_ID(N'[dbo].[Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Message];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[Whistle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Whistle];
GO
IF OBJECT_ID(N'[WhistleDBModelStoreContainer].[MessageConversation]', 'U') IS NOT NULL
    DROP TABLE [WhistleDBModelStoreContainer].[MessageConversation];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admin'
CREATE TABLE [dbo].[Admin] (
    [AdminID] int  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Conversation'
CREATE TABLE [dbo].[Conversation] (
    [ConversationID] int IDENTITY(1,1) NOT NULL,
    [WhistleID] int  NOT NULL
);
GO

-- Creating table 'File'
CREATE TABLE [dbo].[File] (
    [FileID] int  NOT NULL,
    [Base64] nvarchar(max)  NOT NULL,
    [Extension] nchar(10)  NOT NULL,
    [WhistleID] int  NOT NULL
);
GO

-- Creating table 'Lawyer'
CREATE TABLE [dbo].[Lawyer] (
    [LawyerID] int  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Message'
CREATE TABLE [dbo].[Message] (
    [MessageID] int IDENTITY(1,1) NOT NULL,
    [Message1] nvarchar(max)  NOT NULL,
    [Sender] int  NOT NULL
);
GO

-- Creating table 'Whistle'
CREATE TABLE [dbo].[Whistle] (
    [WhistleID] int  NOT NULL,
    [LawyerID] int  NOT NULL,
    [About] nvarchar(280)  NOT NULL,
    [C_When] nvarchar(280)  NOT NULL,
    [C_Where] nvarchar(280)  NOT NULL,
    [Description] nvarchar(280)  NOT NULL,
    [Description_OtherEmployees] nvarchar(280)  NOT NULL,
    [UploadID] int  NULL,
    [isActive] bit  NOT NULL
);
GO

-- Creating table 'MessageConversation'
CREATE TABLE [dbo].[MessageConversation] (
    [MessageID] int  NOT NULL,
    [ConversationID] int  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [UniqueID] nvarchar(50)  NOT NULL,
    [Password] nvarchar(30)  NOT NULL,
    [WhistleID] int  NOT NULL,
    [ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AdminID] in table 'Admin'
ALTER TABLE [dbo].[Admin]
ADD CONSTRAINT [PK_Admin]
    PRIMARY KEY CLUSTERED ([AdminID] ASC);
GO

-- Creating primary key on [ConversationID] in table 'Conversation'
ALTER TABLE [dbo].[Conversation]
ADD CONSTRAINT [PK_Conversation]
    PRIMARY KEY CLUSTERED ([ConversationID] ASC);
GO

-- Creating primary key on [FileID] in table 'File'
ALTER TABLE [dbo].[File]
ADD CONSTRAINT [PK_File]
    PRIMARY KEY CLUSTERED ([FileID] ASC);
GO

-- Creating primary key on [LawyerID] in table 'Lawyer'
ALTER TABLE [dbo].[Lawyer]
ADD CONSTRAINT [PK_Lawyer]
    PRIMARY KEY CLUSTERED ([LawyerID] ASC);
GO

-- Creating primary key on [MessageID] in table 'Message'
ALTER TABLE [dbo].[Message]
ADD CONSTRAINT [PK_Message]
    PRIMARY KEY CLUSTERED ([MessageID] ASC);
GO

-- Creating primary key on [WhistleID] in table 'Whistle'
ALTER TABLE [dbo].[Whistle]
ADD CONSTRAINT [PK_Whistle]
    PRIMARY KEY CLUSTERED ([WhistleID] ASC);
GO

-- Creating primary key on [MessageID], [ConversationID] in table 'MessageConversation'
ALTER TABLE [dbo].[MessageConversation]
ADD CONSTRAINT [PK_MessageConversation]
    PRIMARY KEY CLUSTERED ([MessageID], [ConversationID] ASC);
GO

-- Creating primary key on [ID] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------