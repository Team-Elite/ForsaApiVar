
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2018 18:47:08
-- Generated from EDMX file: D:\Projects3\Forsa Phase 2\API\ForsaAPI\ForsaSignalRSocket\Data\ForsawebModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [forsaweb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblRateOfInterestOfBanks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblRateOfInterestOfBanks];
GO
IF OBJECT_ID(N'[dbo].[tblUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblRateOfInterestOfBanks'
CREATE TABLE [dbo].[tblRateOfInterestOfBanks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [IsPublished] bit  NOT NULL,
    [TimePeriodId] int  NOT NULL,
    [MinValue] decimal(18,4)  NULL,
    [MaxValue] decimal(18,4)  NULL,
    [BaseCurve] decimal(18,4)  NULL,
    [FractionRate] decimal(18,4)  NULL,
    [RateOfInterest] decimal(18,4)  NULL,
    [GroupIds] nvarchar(500)  NULL,
    [FractionRate2] decimal(18,4)  NULL,
    [RateOfInterest2] decimal(18,4)  NULL,
    [FractionRate3] decimal(18,4)  NULL,
    [RateOfInterest3] decimal(18,4)  NULL,
    [DateCreated] datetime  NULL,
    [DateModified] datetime  NULL,
    [ModifiedBy] int  NULL
);
GO

-- Creating table 'tblUsers'
CREATE TABLE [dbo].[tblUsers] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [NameOfCompany] nvarchar(max)  NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [PostalCode] nvarchar(max)  NOT NULL,
    [Place] nvarchar(max)  NOT NULL,
    [AccountHolder] nvarchar(max)  NOT NULL,
    [Bank] nvarchar(max)  NOT NULL,
    [IBAN] nvarchar(max)  NOT NULL,
    [BICCode] nvarchar(max)  NOT NULL,
    [GroupIds] nvarchar(max)  NOT NULL,
    [SubGroupId] nvarchar(max)  NOT NULL,
    [LEINumber] nvarchar(max)  NOT NULL,
    [FurtherField4] nvarchar(max)  NOT NULL,
    [Salutation] int  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [SurName] nvarchar(max)  NOT NULL,
    [ContactNumber] nvarchar(max)  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [FurtherField1] nvarchar(max)  NOT NULL,
    [FurtherField2] nvarchar(max)  NOT NULL,
    [FurtherField3] nvarchar(max)  NOT NULL,
    [UserTypeId] nvarchar(max)  NOT NULL,
    [RatingAgentur1] nvarchar(max)  NOT NULL,
    [RatingAgenturValue1] nvarchar(max)  NOT NULL,
    [RatingAgentur2] nvarchar(max)  NOT NULL,
    [RatingAgenturValue2] nvarchar(max)  NOT NULL,
    [DepositInsurance] int  NOT NULL,
    [ClientGroupId] int  NOT NULL,
    [AgreeToThePrivacyPolicy] bit  NOT NULL,
    [AgreeToTheRatingsMayPublish] bit  NOT NULL,
    [AgreeThatInformationOfCompanyMayBePublished] bit  NOT NULL,
    [AcceptAGBS] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NOT NULL,
    [ModifiedBy] int  NOT NULL,
    [NewPassword] nvarchar(max)  NOT NULL,
    [DepositInsuranceAmount] decimal(18,2)  NOT NULL,
    [MinVolume] nvarchar(max)  NOT NULL,
    [SubGroupName] nvarchar(max)  NOT NULL,
    [fo_assignment] nvarchar(max)  NOT NULL,
    [send_email] bit  NOT NULL,
    [status] bit  NOT NULL,
    [isDeleted] bit  NOT NULL,
    [kontakteIsProcessed] bit  NOT NULL,
    [Commission] nvarchar(max)  NOT NULL,
    [IsLoggedIn] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'tblRateOfInterestOfBanks'
ALTER TABLE [dbo].[tblRateOfInterestOfBanks]
ADD CONSTRAINT [PK_tblRateOfInterestOfBanks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [PK_tblUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------