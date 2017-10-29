
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/25/2017 20:42:44
-- Generated from EDMX file: D:\PMO301\Projectoree\Projectoree\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Projectoree];
GO
IF SCHEMA_ID(N'Projectoree') IS NULL EXECUTE(N'CREATE SCHEMA [Projectoree]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Projectoree].[FK__EXPERIENC__useri__6EF57B66]', 'F') IS NOT NULL
    ALTER TABLE [Projectoree].[EXPERIENCE] DROP CONSTRAINT [FK__EXPERIENC__useri__6EF57B66];
GO
IF OBJECT_ID(N'[Projectoree].[FK__LISTINGS__userid__71D1E811]', 'F') IS NOT NULL
    ALTER TABLE [Projectoree].[LISTINGS] DROP CONSTRAINT [FK__LISTINGS__userid__71D1E811];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Projectoree].[EXPERIENCE]', 'U') IS NOT NULL
    DROP TABLE [Projectoree].[EXPERIENCE];
GO
IF OBJECT_ID(N'[Projectoree].[LISTINGS]', 'U') IS NOT NULL
    DROP TABLE [Projectoree].[LISTINGS];
GO
IF OBJECT_ID(N'[Projectoree].[PROFILES]', 'U') IS NOT NULL
    DROP TABLE [Projectoree].[PROFILES];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PROFILES'
CREATE TABLE [Projectoree].[PROFILES] (
    [userid] nvarchar(128)  NOT NULL,
    [firstname] varchar(255)  NOT NULL,
    [lastname] varchar(255)  NOT NULL,
    [email] varchar(255)  NOT NULL,
    [discipline] varchar(255)  NOT NULL,
    [contactnumber] bigint  NULL,
    [skills] varchar(5000)  NULL,
    [units] varchar(255)  NULL,
    [interests] varchar(2000)  NULL,
    [bio] varchar(2000)  NULL
);
GO

-- Creating table 'EXPERIENCEs'
CREATE TABLE [Projectoree].[EXPERIENCEs] (
    [experienceid] bigint IDENTITY(1,1) NOT NULL,
    [userid] nvarchar(128)  NOT NULL,
    [experience1] varchar(2000)  NOT NULL
);
GO

-- Creating table 'LISTINGS'
CREATE TABLE [Projectoree].[LISTINGS] (
    [projectid] int IDENTITY(1,1) NOT NULL,
    [userid] nvarchar(128)  NOT NULL,
    [title] varchar(255)  NOT NULL,
    [listingType] varchar(255)  NOT NULL,
    [seeker] bit  NOT NULL,
    [discipline] varchar(255)  NOT NULL,
    [description] varchar(6000)  NOT NULL,
    [email] varchar(255)  NULL,
    [contactnumber] bigint  NULL,
    [subcategory] varchar(255)  NULL,
    [supervisors] varchar(765)  NULL,
    [timeframe] int  NULL,
    [startdate] datetime  NULL,
    [expiredate] datetime  NULL,
    [mode] int  NULL,
    [location] varchar(255)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [userid] in table 'PROFILES'
ALTER TABLE [Projectoree].[PROFILES]
ADD CONSTRAINT [PK_PROFILES]
    PRIMARY KEY CLUSTERED ([userid] ASC);
GO

-- Creating primary key on [experienceid] in table 'EXPERIENCEs'
ALTER TABLE [Projectoree].[EXPERIENCEs]
ADD CONSTRAINT [PK_EXPERIENCEs]
    PRIMARY KEY CLUSTERED ([experienceid] ASC);
GO

-- Creating primary key on [projectid] in table 'LISTINGS'
ALTER TABLE [Projectoree].[LISTINGS]
ADD CONSTRAINT [PK_LISTINGS]
    PRIMARY KEY CLUSTERED ([projectid] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [userid] in table 'EXPERIENCEs'
ALTER TABLE [Projectoree].[EXPERIENCEs]
ADD CONSTRAINT [FK__EXPERIENC__useri__6EF57B66]
    FOREIGN KEY ([userid])
    REFERENCES [Projectoree].[PROFILES]
        ([userid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__EXPERIENC__useri__6EF57B66'
CREATE INDEX [IX_FK__EXPERIENC__useri__6EF57B66]
ON [Projectoree].[EXPERIENCEs]
    ([userid]);
GO

-- Creating foreign key on [userid] in table 'LISTINGS'
ALTER TABLE [Projectoree].[LISTINGS]
ADD CONSTRAINT [FK__LISTINGS__userid__71D1E811]
    FOREIGN KEY ([userid])
    REFERENCES [Projectoree].[PROFILES]
        ([userid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__LISTINGS__userid__71D1E811'
CREATE INDEX [IX_FK__LISTINGS__userid__71D1E811]
ON [Projectoree].[LISTINGS]
    ([userid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------