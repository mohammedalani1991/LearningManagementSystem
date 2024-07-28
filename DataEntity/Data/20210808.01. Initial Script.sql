USE [master]
GO
/****** Object:  Database [LearningManagementSystem]    Script Date: 8/8/2022 10:37:50 AM ******/
CREATE DATABASE [LearningManagementSystem]
GO
USE [LearningManagementSystem]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/8/2022 10:37:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AboutDic]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutDic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](300) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_AboutDic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AboutDicTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutDicTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AboutDicId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AboutDicTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[AbsenceNote] [nvarchar](500) NULL,
	[IsPresent] [bit] NULL,
	[Date] [datetime] NULL,
	[FromHour] [time](7) NULL,
	[ToHour] [time](7) NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditLogs]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](200) NOT NULL,
	[Controller] [nvarchar](200) NOT NULL,
	[IpAddress] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[RequestDetails] [nvarchar](max) NULL,
 CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ColorId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_BranchTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommunicationChannel]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommunicationChannel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_CommunicationChannel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommunicationChannelTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommunicationChannelTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommunicationChannelId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_CommunicationChannelTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommunicationLogs]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommunicationLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[ContactType] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[TypeId] [int] NOT NULL,
	[TypeText] [nvarchar](300) NULL,
	[LogText] [nvarchar](300) NULL,
	[IsForExtraType] [bit] NULL,
	[CreatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_CommunicationLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mobile] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[FullName] [nvarchar](100) NULL,
	[FirstName] [nvarchar](100) NULL,
	[SecondName] [nvarchar](100) NULL,
	[ThirdName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[GenderId] [int] NULL,
	[BranchId] [int] NULL,
	[Email] [nvarchar](250) NULL,
	[IdNumber] [nvarchar](200) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[SecondName] [nvarchar](100) NULL,
	[ThirdName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[FullName] [nvarchar](100) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_ContactTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[InsertDate] [datetime] NULL,
	[Mobile] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataBaseScripts]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataBaseScripts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_DataBaseScripts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailsLookup]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailsLookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Code] [nvarchar](300) NULL,
	[Value] [nvarchar](200) NULL,
 CONSTRAINT [PK_DetailsLookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailsLookupTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailsLookupTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DetailsLookupId] [int] NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_DetailsLookupTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Email]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[ContactType] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[TypeId] [int] NOT NULL,
	[Email] [nvarchar](300) NULL,
	[Body] [nvarchar](1000) NULL,
	[Subject] [nvarchar](256) NULL,
	[ImageLink] [nvarchar](1000) NULL,
	[cycleLink] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[StartWorkDate] [datetime] NOT NULL,
	[Address] [nvarchar](300) NULL,
	[JobId] [int] NULL,
	[JobTypeId] [int] NULL,
	[CreatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Address] [nvarchar](300) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Generalization]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Generalization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[BranchId] [int] NULL,
	[GeneralizationTypeId] [int] NULL,
	[JobId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Generalization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneralizationEmployee]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralizationEmployee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GeneralizationId] [int] NULL,
	[ContactId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_GeneralizationEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneralizationTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralizationTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GeneralizationId] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_GeneralizationTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemFile]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[FileId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_ItemFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterLookup]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterLookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](200) NULL,
	[Code] [nvarchar](200) NULL,
 CONSTRAINT [PK_MasterLookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterLookupTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterLookupTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterLookupId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_MasterLookupTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToId] [int] NULL,
	[TypeId] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[Mobile] [nvarchar](50) NULL,
	[IsExtraMobile] [bit] NULL,
	[ExtraMobile] [nvarchar](50) NULL,
	[BranchId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Source] [nvarchar](250) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[SortOrder] [int] NULL,
	[BaseUrl] [nvarchar](500) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_ModuleTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GeneralizationId] [int] NOT NULL,
	[IsRead] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ContactId] [int] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageUrl] [nvarchar](max) NOT NULL,
	[PageName] [nvarchar](300) NULL,
	[PermissionKey] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[ModuleId] [int] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PageName] [nvarchar](300) NULL,
 CONSTRAINT [PK_PermissionTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[BirthDate] [datetime] NULL,
	[BirthPlace] [nvarchar](200) NULL,
	[Work] [nvarchar](200) NOT NULL,
	[EducationalLevelId] [int] NOT NULL,
	[Country] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[WorkPlace] [nvarchar](200) NULL,
	[Email] [nvarchar](100) NULL,
	[ExtraMobile] [nvarchar](100) NULL,
	[CollegeId] [int] NULL,
	[SpecialtyId] [int] NULL,
	[IsExternalStudy] [bit] NULL,
	[IsMedicalPast] [bit] NULL,
	[IsFastSubscription] [bit] NULL,
	[MedicalDescription] [nvarchar](250) NULL,
	[TrainingConsultantId] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentNotes]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](512) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_StudentNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentNotesTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentNotesTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentNoteId] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_StudentNotesTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[BirthPlace] [nvarchar](200) NULL,
	[Work] [nvarchar](200) NULL,
	[Country] [nvarchar](200) NULL,
	[Address] [nvarchar](200) NULL,
	[WorkPlace] [nvarchar](200) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_StudentTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCommunicationChannel]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCommunicationChannel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommunicationChannelId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_SubCommunicationChannel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCommunicationChannelTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCommunicationChannelTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubCommunicationChannelId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_SubCommunicationChannelTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemFile]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [int] NULL,
	[FileUrl] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[DisplayName] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[AltText] [nvarchar](max) NULL,
 CONSTRAINT [PK_SystemFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemFileTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFileTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemFileId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[DisplayName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[AltText] [nvarchar](max) NULL,
 CONSTRAINT [PK_SystemFileTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemGroup]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemGroupTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemGroupTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemGroupId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_SystemGroupTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemGroupUsers]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemGroupUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemGroupId] [int] NOT NULL,
	[UserProfileId] [int] NULL,
 CONSTRAINT [PK_SystemGroupUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Component] [nvarchar](200) NOT NULL,
	[StackTrace] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemSetting]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
	[TypeId] [int] NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemSettingTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSettingTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SettingId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SystemSettingTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainer]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[StartWorkDate] [datetime] NOT NULL,
	[GeneralSpecialtyId] [int] NULL,
	[WorkHouers] [int] NULL,
	[UserProfileId] [int] NULL,
	[IsUser] [bit] NULL,
	[IsFullTimeWorker] [bit] NULL,
 CONSTRAINT [PK_Trainer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[ContactId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[ProfilePhoto] [nvarchar](max) NULL,
	[PreferedLanguageId] [int] NULL,
	[StartWorkDate] [datetime] NULL,
	[JobId] [int] NULL,
	[EmployeeColorId] [int] NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfileTranslation]    Script Date: 8/8/2022 10:37:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfileTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[UserProfileId] [int] NOT NULL,
 CONSTRAINT [PK_UserProfileTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AboutDic] ON 
GO
INSERT [dbo].[AboutDic] ([Id], [GroupName], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [SortOrder]) VALUES (1, N'HomePage', CAST(N'2021-08-09T11:14:51.627' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Salam 2', N'<p>Salam 1<br></p>', 0)
GO
SET IDENTITY_INSERT [dbo].[AboutDic] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'Administrator', N'Administrator', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'Admin', N'Admin', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3', N'Employee', N'Employee', NULL)
GO
SET IDENTITY_INSERT [dbo].[AspNetUserRoles] ON 
GO
INSERT [dbo].[AspNetUserRoles] ([Id], [UserId], [RoleId]) VALUES (1013, N'5f0dda34-9055-4ac2-8ef6-e3fae1e8378c', N'1')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserRoles] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5f0dda34-9055-4ac2-8ef6-e3fae1e8378c', N'salam-cs@hotmail.com', N'SALAM-CS@HOTMAIL.COM', N'salam-cs@hotmail.com', N'SALAM-CS@HOTMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJFRP4X6NP3R/Hxakn8BolnW/Wkl605xRSJOvsMamLg0PT/3Ny09O61eLfV405wDmA==', N'7ZM723H6XZJ4EKXKHCVV26FHJ5D6Y3OP', N'3664be74-cb4e-4c46-9ff0-c6e8d88df989', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'5f0dda34-9055-4ac2-8ef6-e3fae1e8378c', N'[AspNetUserStore]', N'AuthenticatorKey', N'LTUGMJ4GYAG6UHIOLAOS75AEN4DS5RHY')
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 
GO
INSERT [dbo].[Branch] ([Id], [Name], [Code], [ColorId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (1, N'Tulkarem', N'Tul', 2017, CAST(N'2021-11-29T23:53:26.467' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 
GO
INSERT [dbo].[Contact] ([Id], [Mobile], [CreatedOn], [Status], [DeletedOn], [FullName], [FirstName], [SecondName], [ThirdName], [LastName], [GenderId], [BranchId], [Email], [IdNumber]) VALUES (1, N'1212412', CAST(N'2021-12-01T20:01:18.890' AS DateTime), 1, NULL, N'Salam   Modallal', N'Salam', NULL, NULL, N'Modallal', 9, 1, N'salam-cs@hotmail.com', NULL)
GO
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[ContactType] ON 
GO
INSERT [dbo].[ContactType] ([Id], [ContactId], [TypeId]) VALUES (67, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[ContactType] OFF
GO
SET IDENTITY_INSERT [dbo].[DetailsLookup] ON 
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (7, 10, CAST(N'2019-12-19T15:29:42.440' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Arabic', N'Arabic')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (8, 10, CAST(N'2019-12-19T15:31:39.180' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'English', N'English')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (9, 11, CAST(N'2019-12-21T17:59:46.870' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'M', N'Male')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2012, 1015, CAST(N'2021-12-23T09:32:05.853' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'CS', N'IT')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2013, 1015, CAST(N'2021-12-23T09:32:20.920' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Mang', N'Manegment')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2014, 1019, CAST(N'2021-12-23T09:35:31.420' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'FTime', N'Full time')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2015, 1019, CAST(N'2021-12-23T09:35:45.597' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'PTime', N'Part Time')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2016, 13, CAST(N'2021-12-23T09:37:38.057' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'red', N'red')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2018, 13, CAST(N'2021-12-23T09:38:02.230' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'yellow', N'yellow')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2019, 12, CAST(N'2021-12-23T09:54:43.963' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'10', N'10')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2020, 12, CAST(N'2021-12-23T09:54:51.227' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'20', N'20')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2021, 12, CAST(N'2021-12-23T09:54:59.183' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'50', N'50')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2023, 1016, CAST(N'2021-12-23T10:30:49.120' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'public', N'Public')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2024, 1016, CAST(N'2021-12-23T10:31:04.387' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'important', N'Important')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2025, 2018, CAST(N'2021-12-27T12:32:08.187' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'public', N'public')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2029, 2020, CAST(N'2022-01-02T13:16:04.893' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Gal', N'Galaxy')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2030, 2020, CAST(N'2022-01-02T13:16:18.853' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'edu', N'Education')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2031, 2020, CAST(N'2022-01-02T13:17:00.543' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'work', N'Work')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2032, 2021, CAST(N'2022-01-02T13:18:47.793' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Individ', N'Individually')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2033, 2021, CAST(N'2022-01-02T13:19:05.520' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'prog', N'program')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2034, 2021, CAST(N'2022-01-02T13:19:34.457' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Package', N'Package')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2035, 13, CAST(N'2022-01-02T14:16:29.960' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'black', N'Black')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2036, 13, CAST(N'2022-01-02T14:17:06.580' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'blue', N'blue')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2037, 11, CAST(N'2022-01-02T14:42:21.397' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'F', N'Female')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2038, 12, CAST(N'2022-01-02T14:43:13.063' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'100', N'100')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2039, 1016, CAST(N'2022-01-02T14:44:29.133' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Attention', N'Attention')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2040, 1014, CAST(N'2022-01-02T14:46:38.613' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'datetime-local', N'Date Time')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2041, 1014, CAST(N'2022-01-02T14:46:52.493' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'number', N'Number')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2042, 1014, CAST(N'2022-01-02T14:47:10.977' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'checkbox', N'Boolean')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2043, 1014, CAST(N'2022-01-02T14:47:26.503' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'text', N'Text')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2044, 1022, CAST(N'2022-01-02T14:50:04.640' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'1', N'Level one')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2045, 1022, CAST(N'2022-01-02T14:50:13.610' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'2', N'Level two')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2046, 1022, CAST(N'2022-01-02T14:50:21.740' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'3', N'Level three')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2054, 2014, CAST(N'2022-01-03T08:31:50.643' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'l.T.D', N'less than  Direction')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2055, 2014, CAST(N'2022-01-03T08:32:03.620' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'S.D', N'successful direction')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2056, 2014, CAST(N'2022-01-03T08:32:14.883' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'F.D', N'Failed direction')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2057, 2014, CAST(N'2022-01-03T08:32:26.337' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'diploma', N'diploma')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2058, 2014, CAST(N'2022-01-03T08:32:40.453' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Bachelor', N'Bachelor')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2059, 2014, CAST(N'2022-01-03T08:33:05.413' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'M.A.', N'Master''s')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2060, 2014, CAST(N'2022-01-03T08:33:17.240' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'PhD', N'Doctorate')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2063, 2015, CAST(N'2022-01-03T08:42:35.927' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'AM', N'AM')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2064, 2015, CAST(N'2022-01-03T08:43:24.440' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'PM', N'PM')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2065, 1019, CAST(N'2022-01-03T08:45:58.287' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Trainee', N'Trainee')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2066, 1015, CAST(N'2022-01-03T08:47:06.527' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'market', N'Marketing')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2067, 1015, CAST(N'2022-01-03T08:47:18.907' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Sales', N'Sales')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2068, 1015, CAST(N'2022-01-03T08:47:55.150' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Proc', N'Processes')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2069, 1015, CAST(N'2022-01-03T08:48:12.100' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'T.R', N'Training')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2070, 1015, CAST(N'2022-01-03T08:48:42.160' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'correspon', N'Correspondents')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2071, 1015, CAST(N'2022-01-03T08:48:58.523' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'cosm', N'Cosmetic')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2072, 1015, CAST(N'2022-01-03T08:49:12.073' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'HR', N'HR')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2073, 1015, CAST(N'2022-01-03T08:49:33.953' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'ACC', N'Accounting')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2074, 13, CAST(N'2022-01-03T08:58:01.073' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'purple', N'purple')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2075, 1018, CAST(N'2022-01-03T09:57:17.103' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Emp', N'Employee')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2076, 1021, CAST(N'2022-01-03T10:48:53.623' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'5-10', N'5-10')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2077, 1021, CAST(N'2022-01-03T10:49:10.100' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'10-20', N'10-20')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2078, 1021, CAST(N'2022-01-03T10:49:20.517' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'20-50', N'20-50')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2079, 1020, CAST(N'2022-01-03T10:50:32.997' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'1', N'1')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2080, 1020, CAST(N'2022-01-03T10:50:40.237' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'2', N'2')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2081, 1020, CAST(N'2022-01-03T10:50:46.763' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'3', N'3')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2082, 1020, CAST(N'2022-01-03T10:50:58.283' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'4', N'4')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2083, 1020, CAST(N'2022-01-03T10:51:06.890' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'5', N'5')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2084, 2030, CAST(N'2022-01-27T14:36:20.627' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'0', N'0%')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2085, 2030, CAST(N'2022-01-27T14:36:42.540' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'25', N'25%')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2086, 2030, CAST(N'2022-01-27T14:37:20.700' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'50', N'50%')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2087, 2030, CAST(N'2022-01-27T14:37:34.663' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'75', N'75%')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2088, 2030, CAST(N'2022-01-27T14:37:46.977' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'100', N'100%')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2089, 2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Saturday', N'Saturday')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2090, 2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Sunday', N'Sunday')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2091, 2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Monday', N'Monday')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2092, 2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Tuesday', N'Tuesday')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2093, 2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Wednesday', N'Wednesday')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2094, 2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Thursday', N'Thursday')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2095, 2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Friday', N'Friday')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2096, 2022, CAST(N'2022-01-31T09:59:49.733' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'tprogram', N'Training program')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2097, 2022, CAST(N'2022-01-31T10:00:56.533' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'bundle', N'Bundle')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2100, 1015, CAST(N'2022-02-01T08:32:11.500' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'teach', N'Teacher')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2101, 2029, CAST(N'2022-02-15T09:18:23.400' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'galaxy', N'Galaxy')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2121, 12, CAST(N'2022-03-28T14:55:18.540' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'5', N'5')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2123, 2035, CAST(N'2022-04-12T17:15:58.150' AS DateTime), N'majed.bakeer@galaxy.ps', 1, NULL, N'taw', N'توجيهي')
GO
INSERT [dbo].[DetailsLookup] ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Code], [Value]) VALUES (2125, 2037, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'NormalNote', N'Normal Note')
GO
SET IDENTITY_INSERT [dbo].[DetailsLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([Id], [ContactId], [CreatedOn], [Status], [DeletedOn], [StartWorkDate], [Address], [JobId], [JobTypeId], [CreatedBy]) VALUES (1, 1, CAST(N'2021-12-01T20:01:50.140' AS DateTime), 1, NULL, CAST(N'2021-12-30T00:00:00.000' AS DateTime), N'عقابا', 14, 15, NULL)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[MasterLookup] ON 
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (10, CAST(N'2019-12-19T10:01:44.800' AS DateTime), N'', 1, NULL, N'Languages', N'Languages')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (11, CAST(N'2019-12-21T17:55:47.497' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Gender', N'Gender')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (12, CAST(N'2021-09-04T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Pagination', N'Pagination')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (13, CAST(N'2021-11-29T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Colors', N'Colors')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1014, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Setting Types', N'SettingTypes')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1015, CAST(N'2021-12-02T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Jobs', N'Job')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1016, CAST(N'2021-12-02T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Generalization Types', N'GeneralizationTypes')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1018, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Contact Types', N'ContactTypes')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1019, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Job Type', N'JobType')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1020, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Company Rate', N'CompanyRate')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1021, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'EmloyeeNumber', N'EmloyeeNumber')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (1022, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Chamber Commerce Level', N'ChamberCommerceLevel')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2014, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Educational Level', N'EducationalLevel')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2015, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Right Time', N'RightTime')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2018, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'General Specialty', N'GeneralSpecialty')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2020, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Certificate Issued', N'CertificateIssued')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2021, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Course Dependency', N'CourseDependency')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2022, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Program Type', N'ProgramTypeId')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2024, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 2, CAST(N'2022-01-31T12:00:27.480' AS DateTime), N'Communication Channel', N'CommunicationChannel')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2025, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 2, CAST(N'2022-02-15T08:40:11.517' AS DateTime), N'First Sub Channel', N'FirstSubChannel')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2026, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 2, CAST(N'2022-02-15T08:40:05.777' AS DateTime), N'Second Sub Channel', N'SecondSubChannel')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2029, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Course Type DDL', N'CourseTypeDdl')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2030, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'How Serious', N'HowSerious')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2031, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Weekdays', N'Weekdays')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2035, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 2, CAST(N'2022-04-13T11:27:10.163' AS DateTime), N'Educational Level', N'EducationalLevel')
GO
INSERT [dbo].[MasterLookup] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Code]) VALUES (2037, CAST(N'2021-11-30T13:57:59.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Note Type', N'NoteType')
GO
SET IDENTITY_INSERT [dbo].[MasterLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[Modules] ON 
GO
INSERT [dbo].[Modules] ([Id], [Name], [Code], [Description], [SortOrder], [BaseUrl], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (1, N'Control Panel', N'Panel', N'Control Panel', 1, N'/ControlPanel/Dashboard', CAST(N'2021-11-15T00:00:00.000' AS DateTime), N'salam.cs@hotmail.com', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Modules] OFF
GO
SET IDENTITY_INSERT [dbo].[Permission] ON 
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2, N'', N'Dashboard', N'View', N'Dashboard Admin', CAST(N'2019-12-22T13:08:02.753' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (7, N'', N'Lookups', N'View', N'View', CAST(N'2019-12-22T15:00:57.330' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (8, N'', N'Lookups', N'Create', N'Create', CAST(N'2019-12-22T15:01:10.720' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (9, N'', N'Lookups', N'Edit', N'Edit', CAST(N'2019-12-22T15:01:22.493' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (10, N'', N'Lookups', N'Delete', N'Delete', CAST(N'2019-12-22T15:01:31.933' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (11, N'', N'UserProfiles', N'View', N'View', CAST(N'2019-12-25T13:05:24.133' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (12, N'', N'UserProfiles', N'Edit', N'Edit', CAST(N'2019-12-25T13:05:35.337' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (13, N'', N'UserProfiles', N'Delete', N'Delete', CAST(N'2019-12-25T13:05:42.960' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (14, N'', N'UserProfiles', N'Create', N'Create', CAST(N'2019-12-25T13:06:06.193' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (15, N'', N'Roles', N'View', N'View', CAST(N'2019-12-25T13:06:50.563' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (16, N'', N'Roles', N'Edit', N'Edit', CAST(N'2019-12-25T13:07:04.233' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (17, N'', N'Roles', N'Create', N'Create', CAST(N'2019-12-25T13:07:16.983' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (18, N'', N'Roles', N'Delete', N'Delete', CAST(N'2019-12-25T13:07:42.657' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (23, N'', N'Permissions', N'View', N'View', CAST(N'2019-12-25T13:10:11.277' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (24, N'', N'Permissions', N'Edit', N'Edit', CAST(N'2019-12-25T13:10:18.663' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (25, N'', N'Permissions', N'Create', N'Create', CAST(N'2019-12-25T13:10:29.417' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (26, N'', N'Permissions', N'Delete', N'Delete', CAST(N'2019-12-25T13:10:40.180' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (31, N'', N'SystemLogs', N'View', N'View', CAST(N'2019-12-25T13:12:09.110' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (32, N'', N'SystemLogs', N'Delete', N'Delete', CAST(N'2019-12-25T13:12:16.693' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (33, N'', N'Settings', N'View', N'View', CAST(N'2019-12-25T13:12:55.273' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (34, N'', N'Settings', N'Edit', N'Edit', CAST(N'2019-12-25T13:13:06.227' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (35, N'', N'Settings', N'Delete', N'Delete', CAST(N'2019-12-25T13:13:14.407' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (36, N'', N'Settings', N'Create', N'Create', CAST(N'2019-12-25T13:13:23.667' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (37, N'', N'ContactUs', N'View', N'View', CAST(N'2019-12-27T11:34:54.177' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (38, N'', N'ContactUs', N'Edit', N'Edit', CAST(N'2019-12-27T11:35:10.317' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (39, N'', N'ContactUs', N'Delete', N'Delete', CAST(N'2019-12-27T11:35:19.523' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (40, N'', N'ContactUs', N'Create', N'Create', CAST(N'2019-12-27T11:35:32.063' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (41, N'', N'AboutDics', N'View', N'View', CAST(N'2019-12-28T12:29:13.110' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (42, N'', N'AboutDics', N'Edit', N'Edit', CAST(N'2019-12-28T12:29:22.773' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (43, N'', N'AboutDics', N'Delete', N'Delete', CAST(N'2019-12-28T12:29:31.577' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (44, N'', N'AboutDics', N'Create', N'Create', CAST(N'2019-12-28T12:29:39.483' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (53, N'', N'Files', N'Delete', N'Delete', CAST(N'2020-01-08T19:55:23.120' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (54, N'', N'UserProfiles', N'AddRole', N'Add Role', CAST(N'2021-09-08T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (55, N'', N'UserProfiles', N'DeleteRole', N'Delete Role', CAST(N'2021-09-08T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (56, N'', N'UserProfiles', N'ResetPassword', N'Reset Password', CAST(N'2021-09-08T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (57, N'', N'AuditLogs', N'View', N'View', CAST(N'2021-09-08T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (58, N'', N'AuditLogs', N'Delete', N'Delete', CAST(N'2021-09-08T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (59, N'', N'Home', N'View', N'View Home', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (60, N'', N'Organization', N'View', N'View Organization', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (61, N'', N'Organization', N'Create', N'Create Organization', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (62, N'', N'Organization', N'Edit', N'Edit Organization', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (63, N'', N'Organization', N'Delete', N'Delete Organization', CAST(N'2021-09-27T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (64, N'', N'PerspectiveWeight', N'View', N'View Perspective Weight', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (65, N'', N'PerspectiveWeight', N'ViewStrategicObjectiveWeight', N'View Strategic Objective Weight', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (66, N'', N'PerspectiveWeight', N'EditPrespectivesWeight', N'Edit Prespectives Weight', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (67, N'', N'PerspectiveWeight', N'EditStrategicObjectiveWeight', N'Edit Strategic Objective Weight', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (68, N'', N'StrategyTheme', N'View', N'View Strategy Theme', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (69, N'', N'StrategyTheme', N'Create', N'Create Strategy Theme', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (70, N'', N'StrategyTheme', N'Edit', N'Edit Strategy Theme', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (71, N'', N'StrategyTheme', N'Delete', N'Delete Strategy Theme', CAST(N'2021-09-27T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (72, N'', N'StrategyMap', N'View', N'View Strategy Map', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (73, N'', N'StrategicInitiative', N'View', N'View StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (74, N'', N'StrategicInitiative', N'Create', N'Create StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (75, N'', N'StrategicInitiative', N'Edit', N'Edit StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (76, N'', N'StrategicInitiative', N'Delete', N'Delete StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (77, N'', N'StrategicInitiativeProject', N'View', N'View StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (78, N'', N'StrategicInitiativeProject', N'Create', N'Create StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (79, N'', N'StrategicInitiativeProject', N'Edit', N'Edit StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (80, N'', N'StrategicInitiativeProject', N'Delete', N'Delete StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (81, N'', N'StrategicInitiativeProjectTasks', N'View', N'View StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (82, N'', N'StrategicInitiativeProjectTasks', N'Create', N'Create StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (83, N'', N'StrategicInitiativeProjectTasks', N'Edit', N'Edit StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (84, N'', N'StrategicInitiativeProjectTasks', N'Delete', N'Delete StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (85, N'', N'StrategicInitiativeProjectMilestone', N'View', N'View StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (86, N'', N'StrategicInitiativeProjectMilestone', N'Create', N'Create StrategicInitiative', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (87, N'', N'StrategicInitiativeProjectMilestone', N'Edit', N'Edit StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (88, N'', N'StrategicInitiativeProjectMilestone', N'Delete', N'Delete StrategicInitiative', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (89, N'', N'SystemGroups', N'View', N'View SYstemGroups', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (90, N'', N'SystemGroups', N'Create', N'Create SystemGroups', CAST(N'2021-09-27T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (91, N'', N'SystemGroups', N'Edit', N'Edit SystemGroups', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (92, N'', N'SystemGroups', N'Delete', N'Delete SystemGroups', CAST(N'2021-09-27T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (93, N'', N'Modules', N'View', N'View', CAST(N'2021-11-29T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (94, N'', N'Modules', N'Create', N'Create', CAST(N'2021-11-29T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (95, N'', N'Modules', N'Edit', N'Edit', CAST(N'2021-11-29T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (96, N'', N'Modules', N'Delete', N'Delete', CAST(N'2021-11-29T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (97, N'', N'Branches', N'View', N'View', CAST(N'2021-11-29T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (98, N'', N'Branches', N'Create', N'Create', CAST(N'2021-11-29T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (99, N'', N'Branches', N'Edit', N'Edit', CAST(N'2021-11-29T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (100, N'', N'Branches', N'Delete', N'Delete', CAST(N'2021-11-29T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (101, N'test', N'test', N'test', N'test', CAST(N'2021-11-29T23:41:41.223' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (102, N'test', N'شسي', N'k', N'شيس', CAST(N'2021-11-30T00:04:26.967' AS DateTime), N'salam-cs@hotmail.com', 2, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (103, N'test', N'شسي', N'k', N'شيس', CAST(N'2021-11-30T00:06:06.260' AS DateTime), N'salam-cs@hotmail.com', 2, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (104, N'test', N'شسي', N'k', N'شيس', CAST(N'2021-11-30T00:06:33.540' AS DateTime), N'salam-cs@hotmail.com', 2, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (105, N'test', N'test', N'k', N'test', CAST(N'2021-11-30T00:11:49.677' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (106, N'', N'Branches', N'Select', N'Select Branch', CAST(N'2021-11-30T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1106, N'', N'Generalizations', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1107, N'', N'Generalizations', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1108, N'', N'Generalizations', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1109, N'', N'Generalizations', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1110, N'', N'Companies', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1111, N'', N'Companies', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1112, N'', N'Companies', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1113, N'', N'Companies', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1114, N'', N'Companies', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1115, N'', N'CompanyTenders', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1116, N'', N'CompanyTenders', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1117, N'', N'CompanyTenders', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1118, N'', N'CompanyTenders', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1119, N'', N'CompanyTenders', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1120, N'', N'CommunicationLogs', N'View', N'View', CAST(N'2021-12-06T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1121, N'', N'CommunicationLogs', N'Delete', N'Delete', CAST(N'2021-12-06T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1122, N'', N'CommunicationLogs', N'SendMessage', N'Send Message', CAST(N'2021-12-06T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (1123, N'', N'CommunicationLogs', N'SendEmail', N'Send Email', CAST(N'2021-12-06T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2106, N'', N'CompanyTenderFiles', N'View', N'View', CAST(N'2021-12-06T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2107, N'', N'CompanyTenderFiles', N'Delete', N'Delete', CAST(N'2021-12-06T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2108, N'', N'CompanyTenderFiles', N'Download', N'Download', CAST(N'2021-12-06T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, NULL)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2109, N'', N'CompanyBranches', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2110, N'', N'CompanyBranches', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2111, N'', N'CompanyBranches', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2112, N'', N'CompanyBranches', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2113, N'', N'CompanyBranches', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2114, N'', N'Companies', N'ShowAdvisors', N'ShowAdvisors', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2115, N'', N'Companies', N'DeleteAdvisors', N'ShowAdvisors', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2116, N'', N'Students', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2117, N'', N'Students', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2118, N'', N'Students', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2119, N'', N'Students', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2120, N'', N'Students', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2121, N'', N'CompanyIdentifiers', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2122, N'', N'CompanyIdentifiers', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2123, N'', N'CompanyIdentifiers', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2124, N'', N'CompanyIdentifiers', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2125, N'', N'CompanyIdentifiers', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2126, N'', N'Regiments', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2127, N'', N'Regiments', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2128, N'', N'Regiments', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2129, N'', N'Regiments', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2130, N'', N'Regiments', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2131, N'', N'CompanyPayments', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2132, N'', N'CompanyPayments', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2133, N'', N'CompanyPayments', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2134, N'', N'CompanyPayments', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2135, N'', N'CompanyPayments', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2136, N'', N'CompanyNotes', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2137, N'', N'CompanyNotes', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2138, N'', N'CompanyNotes', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2139, N'', N'CompanyNotes', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2140, N'', N'CompanyNotes', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2141, N'', N'CompanyServices', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2142, N'', N'CompanyServices', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2143, N'', N'CompanyServices', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2144, N'', N'CompanyServices', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2145, N'', N'CompanyServices', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2146, N'', N'Trainers', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2147, N'', N'Trainers', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2148, N'', N'Trainers', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2149, N'', N'Trainers', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2150, N'', N'Trainers', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2151, N'', N'Courses', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2152, N'', N'Courses', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2153, N'', N'Courses', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2154, N'', N'Courses', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2155, N'', N'Courses', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2156, N'', N'Programs', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2157, N'', N'Programs', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2158, N'', N'Programs', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2159, N'', N'Programs', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2160, N'', N'Programs', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2161, N'', N'Fees', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2162, N'', N'Fees', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2163, N'', N'Fees', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2164, N'', N'Fees', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2165, N'', N'Fees', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2166, N'', N'Rooms', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2167, N'', N'Rooms', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2168, N'', N'Rooms', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2169, N'', N'Rooms', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2170, N'', N'Rooms', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2171, N'', N'Rooms', N'Reservation', N'Reservation', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2172, N'', N'Attendances', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2173, N'', N'Attendances', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2174, N'', N'Attendances', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2175, N'', N'Attendances', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2176, N'', N'Attendances', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2177, N'', N'ProgramSections', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2178, N'', N'ProgramSections', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2179, N'', N'ProgramSections', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2180, N'', N'ProgramSections', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2181, N'', N'ProgramSections', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2182, N'', N'VisitorInterests', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2183, N'', N'VisitorInterests', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2184, N'', N'VisitorInterests', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2185, N'', N'VisitorInterests', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2186, N'', N'VisitorInterests', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2187, N'', N'Services', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2188, N'', N'Services', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2189, N'', N'Services', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2190, N'', N'Services', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2191, N'', N'Services', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2192, N'', N'Equipments', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2193, N'', N'Equipments', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2194, N'', N'Equipments', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2195, N'', N'Equipments', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2196, N'', N'Equipments', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2197, N'', N'ProgramEquipment', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2198, N'', N'ProgramEquipment', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2199, N'', N'ProgramEquipment', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2200, N'', N'ProgramEquipment', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2201, N'', N'ProgramEquipment', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2202, N'', N'RoomEquipment', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2203, N'', N'RoomEquipment', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2204, N'', N'RoomEquipment', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2205, N'', N'RoomEquipment', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2206, N'', N'RoomEquipment', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2207, N'', N'StudentAttendance', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2208, N'', N'StudentAttendance', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2209, N'', N'StudentAttendance', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2210, N'', N'StudentAttendance', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2211, N'', N'StudentAttendance', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2212, N'', N'Targets', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2213, N'', N'Targets', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2214, N'', N'Targets', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2215, N'', N'Targets', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2216, N'', N'Targets', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2217, N'', N'TargetTypes', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2218, N'', N'TargetTypes', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2219, N'', N'TargetTypes', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2220, N'', N'TargetTypes', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2221, N'', N'TargetTypes', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2222, N'', N'TargetAdvisors', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2223, N'', N'TargetAdvisors', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2224, N'', N'TargetAdvisors', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2225, N'', N'TargetsProgram', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2226, N'', N'TargetsProgram', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2227, N'', N'TargetsProgram', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2228, N'', N'TargetsProgram', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2229, N'', N'TargetsProgram', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2230, N'', N'Leads', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2231, N'', N'Leads', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2232, N'', N'Leads', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2233, N'', N'Leads', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2234, N'', N'Leads', N'Details', N'Details', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2235, N'', N'TargetProgramAdvisors', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2236, N'', N'TargetProgramAdvisors', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2237, N'', N'TargetProgramAdvisors', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2238, N'', N'LeadNotes', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2239, N'', N'LeadNotes', N'Create', N'Create', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2240, N'', N'SyncProgramPlanCourses', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2241, N'', N'hala test', N'create', NULL, CAST(N'2022-04-14T13:20:39.617' AS DateTime), N'hudasalameh97@gmail.com', 2, CAST(N'2022-04-14T14:35:26.273' AS DateTime), 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2242, N'', N'AdvicerLead', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2243, N'', N'AdvicerLead', N'Create', N'Create', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2244, N'', N'AdvicerLead', N'Edit', N'Edit', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2245, N'', N'AdvicerLead', N'Delete', N'Delete', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2246, N'', N'AdvicerLead', N'Transfer', N'Transfer', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2247, N'', N'AdvicerLead', N'Message', N'Message', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2248, N'', N'FollowUps', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2249, N'', N'FollowUps', N'Create', N'Create', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2250, N'', N'FollowUps', N'Edit', N'Edit', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2251, N'', N'FollowUps', N'Delete', N'Delete', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2252, N'', N'AdvicerLead', N'Details', N'Details', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2253, N'', N'SortPublicFetchData', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2254, N'', N'SortPublicFetchData', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2255, N'', N'SortPrivateFetchData', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2256, N'', N'SortPrivateFetchData', N'Edit', N'Edit', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2257, N'', N'Inquiry', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2258, N'', N'Inquiry', N'Details', N'Details', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2259, N'', N'Leads', N'FetchData', N'FetchData', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (2383, N'', N'Area', N'Management', N'Management', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3260, N'', N'CallCenterEmployees', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3261, N'', N'CallCenterEmployees', N'Create', N'Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3262, N'', N'CallCenterEmployees', N'Edit', N'Edit', CAST(N'2021-12-02T00:03:30.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3263, N'', N'CallCenterEmployees', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3264, N'', N'CallCenterFetch', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3265, N'', N'CallCenterFetch', N'Fetch', N'Fetch', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3266, N'', N'CallCenterFetch', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3267, N'', N'CallCenterFollowUps', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3268, N'', N'CallCenterFollowUps', N'Edit', N'Edit', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3269, N'', N'CallCenterFollowUps', N'Message', N'Message', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3270, N'', N'CallCenterFollowUps', N'NoteView', N'Note View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3271, N'', N'CallCenterFollowUps', N'NoteCreate', N'Note Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3272, N'', N'CallCenterFollowUps', N'InterestView', N'InterestView', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3273, N'', N'CallCenterFollowUps', N'InterestCreate', N'Interest Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3274, N'', N'CallCenterFollowUps', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3275, N'', N'CallCenterFollowUpStudents', N'View', N'View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3276, N'', N'CallCenterFollowUpStudents', N'Edit', N'Edit', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3277, N'', N'CallCenterFollowUpStudents', N'Message', N'Message', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3278, N'', N'CallCenterFollowUpStudents', N'NoteView', N'Note View', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3279, N'', N'CallCenterFollowUpStudents', N'NoteCreate', N'Note Create', CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (3280, N'', N'CallCenterFollowUpStudents', N'Delete', N'Delete', CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
SET IDENTITY_INSERT [dbo].[RolePermissions] ON 
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (345, N'1', 44)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (346, N'1', 43)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (347, N'1', 42)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (348, N'1', 41)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (349, N'1', 40)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (350, N'1', 39)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (351, N'1', 38)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (352, N'1', 37)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (353, N'1', 36)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (354, N'1', 35)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (355, N'1', 34)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (356, N'1', 33)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (357, N'1', 32)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (358, N'1', 31)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (359, N'1', 53)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (360, N'1', 26)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (361, N'1', 24)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (362, N'1', 23)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (363, N'1', 18)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (364, N'1', 17)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (365, N'1', 16)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (366, N'1', 15)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (367, N'1', 14)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (368, N'1', 13)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (369, N'1', 12)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (370, N'1', 11)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (371, N'1', 10)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (372, N'1', 9)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (373, N'1', 8)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (374, N'1', 7)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (375, N'1', 25)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (376, N'1', 2)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (377, N'1', 54)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (378, N'1', 55)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (379, N'1', 56)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (380, N'1', 57)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (381, N'1', 58)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (382, N'1', 59)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (383, N'1', 60)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (384, N'1', 61)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (385, N'1', 62)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (386, N'1', 63)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (387, N'1', 64)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (388, N'1', 65)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (389, N'1', 66)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (390, N'1', 67)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (391, N'1', 68)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (392, N'1', 69)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (393, N'1', 70)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (394, N'1', 71)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (395, N'1', 72)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (396, N'2', 2)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (397, N'2', 11)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (398, N'2', 12)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (399, N'2', 13)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (400, N'2', 14)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (401, N'2', 54)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (402, N'2', 55)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (403, N'2', 56)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (404, N'2', 59)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (405, N'2', 60)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (406, N'2', 61)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (407, N'2', 62)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (408, N'2', 63)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (409, N'2', 64)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (410, N'2', 65)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (411, N'2', 66)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (412, N'2', 67)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (413, N'2', 68)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (414, N'2', 69)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (415, N'2', 70)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (416, N'2', 71)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (417, N'2', 72)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (418, N'1', 73)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (419, N'1', 74)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (420, N'1', 75)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (421, N'1', 76)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (422, N'1', 77)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (423, N'1', 78)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (424, N'1', 79)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (425, N'1', 80)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (426, N'1', 81)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (427, N'1', 82)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (428, N'1', 83)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (429, N'1', 84)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (430, N'1', 85)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (431, N'1', 86)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (432, N'1', 87)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (433, N'1', 88)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (434, N'1', 89)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (435, N'1', 90)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (436, N'1', 91)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (437, N'1', 92)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (438, N'1', 93)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (439, N'1', 94)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (440, N'1', 95)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (441, N'1', 96)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (442, N'1', 97)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (443, N'1', 98)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (444, N'1', 99)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (445, N'1', 100)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (446, N'1', 106)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1446, N'1', 1106)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1447, N'1', 1107)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1448, N'1', 1108)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1449, N'1', 1109)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1450, N'1', 1110)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1451, N'1', 1111)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1452, N'1', 1112)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1453, N'1', 1113)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1454, N'1', 1114)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1455, N'1', 1115)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1456, N'1', 1116)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1457, N'1', 1117)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1458, N'1', 1118)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1459, N'1', 1119)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1460, N'1', 1120)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1461, N'1', 1121)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1462, N'1', 1122)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (1463, N'1', 1123)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2446, N'1', 2106)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2447, N'1', 2107)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2448, N'1', 2108)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2449, N'1', 2109)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2450, N'1', 2110)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2451, N'1', 2111)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2452, N'1', 2112)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2453, N'1', 2113)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2454, N'1', 2114)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2455, N'1', 2115)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2456, N'1', 2116)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2457, N'1', 2117)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2458, N'1', 2118)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2459, N'1', 2119)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2460, N'1', 2120)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2461, N'1', 2121)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2462, N'1', 2122)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2463, N'1', 2123)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2464, N'1', 2124)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2465, N'1', 2125)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2466, N'1', 2126)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2467, N'1', 2127)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2468, N'1', 2128)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2469, N'1', 2129)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2470, N'1', 2130)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2471, N'1', 2131)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2472, N'1', 2132)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2473, N'1', 2133)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2474, N'1', 2134)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2475, N'1', 2135)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2476, N'1', 2136)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2477, N'1', 2137)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2478, N'1', 2138)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2479, N'1', 2139)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2480, N'1', 2140)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2481, N'1', 2141)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2482, N'1', 2142)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2483, N'1', 2143)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2484, N'1', 2144)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2485, N'1', 2145)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2486, N'1', 2146)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2487, N'1', 2147)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2488, N'1', 2148)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2489, N'1', 2149)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2490, N'1', 2150)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2491, N'1', 2151)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2492, N'1', 2152)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2493, N'1', 2153)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2494, N'1', 2154)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2495, N'1', 2155)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2496, N'1', 2156)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2497, N'1', 2157)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2498, N'1', 2158)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2499, N'1', 2159)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2500, N'1', 2160)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2501, N'1', 2161)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2502, N'1', 2162)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2503, N'1', 2163)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2504, N'1', 2164)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2505, N'1', 2165)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2506, N'1', 2166)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2507, N'1', 2167)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2508, N'1', 2168)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2509, N'1', 2169)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2510, N'1', 2170)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2511, N'1', 2171)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2512, N'1', 2172)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2513, N'1', 2173)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2514, N'1', 2174)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2515, N'1', 2175)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2516, N'1', 2176)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2517, N'1', 2177)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2518, N'1', 2178)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2519, N'1', 2179)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2520, N'1', 2180)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2521, N'1', 2181)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2522, N'1', 2182)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2523, N'1', 2183)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2524, N'1', 2184)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2525, N'1', 2185)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2526, N'1', 2186)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2527, N'1', 2187)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2528, N'1', 2188)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2529, N'1', 2189)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2530, N'1', 2190)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2531, N'1', 2191)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2532, N'1', 2192)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2533, N'1', 2193)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2534, N'1', 2194)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2535, N'1', 2195)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2536, N'1', 2196)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2537, N'1', 2197)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2538, N'1', 2198)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2539, N'1', 2199)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2540, N'1', 2200)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2541, N'1', 2201)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2542, N'1', 2202)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2543, N'1', 2203)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2544, N'1', 2204)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2545, N'1', 2205)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2546, N'1', 2206)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2547, N'1', 2207)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2548, N'1', 2208)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2549, N'1', 2209)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2550, N'1', 2210)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2551, N'1', 2211)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2552, N'1', 2212)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2553, N'1', 2213)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2554, N'1', 2214)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2555, N'1', 2215)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2556, N'1', 2216)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2557, N'1', 2217)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2558, N'1', 2218)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2559, N'1', 2219)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2560, N'1', 2220)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2561, N'1', 2221)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2562, N'1', 2222)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2563, N'1', 2223)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2564, N'1', 2224)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2565, N'1', 2225)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2566, N'1', 2226)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2567, N'1', 2227)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2568, N'1', 2228)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2569, N'1', 2229)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2570, N'1', 2230)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2571, N'1', 2231)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2572, N'1', 2232)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2573, N'1', 2233)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2574, N'1', 2234)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2575, N'1', 2235)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2576, N'1', 2236)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2577, N'1', 2237)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2578, N'1', 2238)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2579, N'1', 2239)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2580, N'1', 2240)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2637, N'3', 2)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2638, N'3', 2126)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2639, N'3', 2127)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2640, N'3', 2128)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2641, N'3', 2129)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2642, N'3', 2130)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2643, N'3', 2230)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2644, N'3', 2231)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2645, N'3', 2232)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2646, N'3', 2233)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2647, N'3', 2234)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2648, N'3', 2235)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2649, N'3', 2236)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2650, N'3', 2237)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2651, N'1', 2242)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2652, N'1', 2243)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2653, N'1', 2244)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2654, N'1', 2245)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2655, N'1', 2246)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2656, N'1', 2247)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2657, N'1', 2248)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2658, N'1', 2249)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2659, N'1', 2250)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2660, N'1', 2251)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2661, N'1', 2252)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2662, N'1', 2253)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2663, N'1', 2254)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2664, N'1', 2255)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2665, N'1', 2256)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2666, N'1', 2257)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2667, N'1', 2258)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (2668, N'1', 2259)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3669, N'1', 3260)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3670, N'1', 3261)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3671, N'1', 3262)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3672, N'1', 3263)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3673, N'1', 3264)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3674, N'1', 3265)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3675, N'1', 3266)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3676, N'1', 3267)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3677, N'1', 3268)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3678, N'1', 3269)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3679, N'1', 3270)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3680, N'1', 3271)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3681, N'1', 3272)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3682, N'1', 3273)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3683, N'1', 3274)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3684, N'1', 3275)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3685, N'1', 3276)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3686, N'1', 3277)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3687, N'1', 3278)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3688, N'1', 3279)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3689, N'1', 3280)
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES (3767, N'1', 2383)
GO
SET IDENTITY_INSERT [dbo].[RolePermissions] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemSetting] ON 
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (1, CAST(N'2021-08-09T01:00:01.260' AS DateTime), N'System', 1, NULL, N'System_ControlPanel_PageSize', N'10', NULL, NULL)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (2, CAST(N'2021-08-31T19:04:33.420' AS DateTime), N'System', 1, NULL, N'System Last Script', N'', NULL, NULL)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (3, CAST(N'2021-12-01T20:12:37.300' AS DateTime), N'System', 1, NULL, N'System_Sending_Email', N'fivecreative2@gmail.com', NULL, NULL)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (4, CAST(N'2021-12-01T20:12:37.333' AS DateTime), N'System', 1, NULL, N'System_TestReply_Email', N'true', NULL, NULL)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (5, CAST(N'2021-12-01T20:12:37.337' AS DateTime), N'System', 1, NULL, N'System_Emails_ReplyTo', N'fivecreative2@gmail.com', NULL, NULL)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (6, CAST(N'2021-12-01T20:12:39.393' AS DateTime), N'System', 1, NULL, N'System_Emails_SmtpClient', N'fivecreative2@gmail.com', NULL, NULL)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (7, CAST(N'2021-12-01T20:12:39.397' AS DateTime), N'System', 1, NULL, N'System_Emails_SmtpPassword', N'Qwer1234/', NULL, NULL)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (1003, CAST(N'2022-03-03T09:08:42.770' AS DateTime), N'System', 1, NULL, N'TargetTypeId', N'2', NULL, 1)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (1004, CAST(N'2022-03-31T08:44:08.570' AS DateTime), N'System', 1, NULL, N'EmailConfirmation', N'true', NULL, 1)
GO
INSERT [dbo].[SystemSetting] ([Id], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [TypeId], [BranchId]) VALUES (1005, CAST(N'2022-04-13T10:52:53.063' AS DateTime), N'System', 1, NULL, N'Target_Collection_Multiplier', N'1.25', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SystemSetting] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemSettingTranslation] ON 
GO
INSERT [dbo].[SystemSettingTranslation] ([Id], [SettingId], [LanguageId], [IsDefault], [Name], [Value]) VALUES (1, 1, 2, 1, N'System_ControlPanel_PageSize', N'10')
GO
SET IDENTITY_INSERT [dbo].[SystemSettingTranslation] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 
GO
INSERT [dbo].[UserProfile] ([Id], [Username], [ContactId], [CreatedOn], [Status], [DeletedOn], [LastLogin], [ProfilePhoto], [PreferedLanguageId], [StartWorkDate], [JobId], [EmployeeColorId]) VALUES (1, N'salam-cs@hotmail.com', 1, CAST(N'2019-12-21T16:30:21.200' AS DateTime), 0, NULL, CAST(N'2022-08-08T09:51:39.000' AS DateTime), N'/Document/1/23.jpg', NULL, NULL, NULL, 2016)
GO
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 8/8/2022 10:37:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 8/8/2022 10:37:51 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 8/8/2022 10:37:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 8/8/2022 10:37:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 8/8/2022 10:37:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 8/8/2022 10:37:51 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 8/8/2022 10:37:51 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AboutDicTranslation]  WITH CHECK ADD  CONSTRAINT [FK_AboutDicTranslation_AboutDicTranslation] FOREIGN KEY([AboutDicId])
REFERENCES [dbo].[AboutDic] ([Id])
GO
ALTER TABLE [dbo].[AboutDicTranslation] CHECK CONSTRAINT [FK_AboutDicTranslation_AboutDicTranslation]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Contact]
GO
ALTER TABLE [dbo].[BranchTranslation]  WITH CHECK ADD  CONSTRAINT [FK_BranchTranslation_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[BranchTranslation] CHECK CONSTRAINT [FK_BranchTranslation_Branch]
GO
ALTER TABLE [dbo].[CommunicationChannelTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CommunicationChannelTranslation_CommunicationChannel] FOREIGN KEY([CommunicationChannelId])
REFERENCES [dbo].[CommunicationChannel] ([Id])
GO
ALTER TABLE [dbo].[CommunicationChannelTranslation] CHECK CONSTRAINT [FK_CommunicationChannelTranslation_CommunicationChannel]
GO
ALTER TABLE [dbo].[CommunicationLogs]  WITH CHECK ADD  CONSTRAINT [FK_CommunicationLogs_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[CommunicationLogs] CHECK CONSTRAINT [FK_CommunicationLogs_Contact]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Branch]
GO
ALTER TABLE [dbo].[ContactTranslation]  WITH CHECK ADD  CONSTRAINT [FK_ContactTranslation_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[ContactTranslation] CHECK CONSTRAINT [FK_ContactTranslation_Contact]
GO
ALTER TABLE [dbo].[ContactType]  WITH CHECK ADD  CONSTRAINT [FK_ContactType_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[ContactType] CHECK CONSTRAINT [FK_ContactType_Contact]
GO
ALTER TABLE [dbo].[DetailsLookup]  WITH CHECK ADD  CONSTRAINT [FK_DetailsLookup_MasterLookup] FOREIGN KEY([MasterId])
REFERENCES [dbo].[MasterLookup] ([Id])
GO
ALTER TABLE [dbo].[DetailsLookup] CHECK CONSTRAINT [FK_DetailsLookup_MasterLookup]
GO
ALTER TABLE [dbo].[DetailsLookupTranslation]  WITH CHECK ADD  CONSTRAINT [FK_DetailsLookupTranslation_DetailsLookup] FOREIGN KEY([DetailsLookupId])
REFERENCES [dbo].[DetailsLookup] ([Id])
GO
ALTER TABLE [dbo].[DetailsLookupTranslation] CHECK CONSTRAINT [FK_DetailsLookupTranslation_DetailsLookup]
GO
ALTER TABLE [dbo].[Email]  WITH CHECK ADD  CONSTRAINT [FK_Email_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Email] CHECK CONSTRAINT [FK_Email_Contact]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Contact]
GO
ALTER TABLE [dbo].[EmployeeTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTranslation_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeTranslation] CHECK CONSTRAINT [FK_EmployeeTranslation_Employee]
GO
ALTER TABLE [dbo].[Generalization]  WITH CHECK ADD  CONSTRAINT [FK_Generalization_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Generalization] CHECK CONSTRAINT [FK_Generalization_Branch]
GO
ALTER TABLE [dbo].[GeneralizationEmployee]  WITH CHECK ADD  CONSTRAINT [FK_GeneralizationEmployee_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[GeneralizationEmployee] CHECK CONSTRAINT [FK_GeneralizationEmployee_Contact]
GO
ALTER TABLE [dbo].[GeneralizationEmployee]  WITH CHECK ADD  CONSTRAINT [FK_GeneralizationEmployee_Generalization] FOREIGN KEY([GeneralizationId])
REFERENCES [dbo].[Generalization] ([Id])
GO
ALTER TABLE [dbo].[GeneralizationEmployee] CHECK CONSTRAINT [FK_GeneralizationEmployee_Generalization]
GO
ALTER TABLE [dbo].[GeneralizationTranslation]  WITH CHECK ADD  CONSTRAINT [FK_GeneralizationTranslation_Generalization] FOREIGN KEY([GeneralizationId])
REFERENCES [dbo].[Generalization] ([Id])
GO
ALTER TABLE [dbo].[GeneralizationTranslation] CHECK CONSTRAINT [FK_GeneralizationTranslation_Generalization]
GO
ALTER TABLE [dbo].[ItemFile]  WITH CHECK ADD  CONSTRAINT [FK_ItemFile_SystemFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[SystemFile] ([Id])
GO
ALTER TABLE [dbo].[ItemFile] CHECK CONSTRAINT [FK_ItemFile_SystemFile]
GO
ALTER TABLE [dbo].[MasterLookupTranslation]  WITH CHECK ADD  CONSTRAINT [FK_MasterLookupTranslation_MasterLookup] FOREIGN KEY([MasterLookupId])
REFERENCES [dbo].[MasterLookup] ([Id])
GO
ALTER TABLE [dbo].[MasterLookupTranslation] CHECK CONSTRAINT [FK_MasterLookupTranslation_MasterLookup]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Branch]
GO
ALTER TABLE [dbo].[ModuleTranslation]  WITH CHECK ADD  CONSTRAINT [FK_ModuleTranslation_Modules] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[ModuleTranslation] CHECK CONSTRAINT [FK_ModuleTranslation_Modules]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Contact]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Generalization] FOREIGN KEY([GeneralizationId])
REFERENCES [dbo].[Generalization] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Generalization]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Modules] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Modules]
GO
ALTER TABLE [dbo].[PermissionTranslation]  WITH CHECK ADD  CONSTRAINT [FK_PermissionTranslation_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([Id])
GO
ALTER TABLE [dbo].[PermissionTranslation] CHECK CONSTRAINT [FK_PermissionTranslation_Permission]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permission]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_RolePermissions] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_RolePermissions]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Contact]
GO
ALTER TABLE [dbo].[StudentNotes]  WITH CHECK ADD  CONSTRAINT [FK_StudentNotes_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[StudentNotes] CHECK CONSTRAINT [FK_StudentNotes_Student]
GO
ALTER TABLE [dbo].[StudentNotesTranslation]  WITH CHECK ADD  CONSTRAINT [FK_StudentNotesTranslation_StudentNotes] FOREIGN KEY([StudentNoteId])
REFERENCES [dbo].[StudentNotes] ([Id])
GO
ALTER TABLE [dbo].[StudentNotesTranslation] CHECK CONSTRAINT [FK_StudentNotesTranslation_StudentNotes]
GO
ALTER TABLE [dbo].[StudentTranslation]  WITH CHECK ADD  CONSTRAINT [FK_StudentTranslation_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[StudentTranslation] CHECK CONSTRAINT [FK_StudentTranslation_Student]
GO
ALTER TABLE [dbo].[SubCommunicationChannel]  WITH CHECK ADD  CONSTRAINT [FK_SubCommunicationChannel_CommunicationChannel] FOREIGN KEY([CommunicationChannelId])
REFERENCES [dbo].[CommunicationChannel] ([Id])
GO
ALTER TABLE [dbo].[SubCommunicationChannel] CHECK CONSTRAINT [FK_SubCommunicationChannel_CommunicationChannel]
GO
ALTER TABLE [dbo].[SubCommunicationChannel]  WITH CHECK ADD  CONSTRAINT [FK_SubCommunicationChannel_SubCommunicationChannel] FOREIGN KEY([ParentId])
REFERENCES [dbo].[SubCommunicationChannel] ([Id])
GO
ALTER TABLE [dbo].[SubCommunicationChannel] CHECK CONSTRAINT [FK_SubCommunicationChannel_SubCommunicationChannel]
GO
ALTER TABLE [dbo].[SubCommunicationChannelTranslation]  WITH CHECK ADD  CONSTRAINT [FK_SubCommunicationChannelTranslation_SubCommunicationChannel] FOREIGN KEY([SubCommunicationChannelId])
REFERENCES [dbo].[SubCommunicationChannel] ([Id])
GO
ALTER TABLE [dbo].[SubCommunicationChannelTranslation] CHECK CONSTRAINT [FK_SubCommunicationChannelTranslation_SubCommunicationChannel]
GO
ALTER TABLE [dbo].[SystemFileTranslation]  WITH CHECK ADD  CONSTRAINT [FK_SystemFileTranslation_SystemFile] FOREIGN KEY([SystemFileId])
REFERENCES [dbo].[SystemFile] ([Id])
GO
ALTER TABLE [dbo].[SystemFileTranslation] CHECK CONSTRAINT [FK_SystemFileTranslation_SystemFile]
GO
ALTER TABLE [dbo].[SystemGroupTranslation]  WITH CHECK ADD  CONSTRAINT [FK_SystemGroupTranslation_SystemGroup] FOREIGN KEY([SystemGroupId])
REFERENCES [dbo].[SystemGroup] ([Id])
GO
ALTER TABLE [dbo].[SystemGroupTranslation] CHECK CONSTRAINT [FK_SystemGroupTranslation_SystemGroup]
GO
ALTER TABLE [dbo].[SystemGroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_SystemGroupUsers_SystemGroup] FOREIGN KEY([SystemGroupId])
REFERENCES [dbo].[SystemGroup] ([Id])
GO
ALTER TABLE [dbo].[SystemGroupUsers] CHECK CONSTRAINT [FK_SystemGroupUsers_SystemGroup]
GO
ALTER TABLE [dbo].[SystemGroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_SystemGroupUsers_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([Id])
GO
ALTER TABLE [dbo].[SystemGroupUsers] CHECK CONSTRAINT [FK_SystemGroupUsers_UserProfile]
GO
ALTER TABLE [dbo].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_Branch]
GO
ALTER TABLE [dbo].[SystemSettingTranslation]  WITH CHECK ADD  CONSTRAINT [FK_SystemSettingTranslation_SystemSetting] FOREIGN KEY([SettingId])
REFERENCES [dbo].[SystemSetting] ([Id])
GO
ALTER TABLE [dbo].[SystemSettingTranslation] CHECK CONSTRAINT [FK_SystemSettingTranslation_SystemSetting]
GO
ALTER TABLE [dbo].[Trainer]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[Trainer] CHECK CONSTRAINT [FK_Trainer_Contact]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Contact]
GO
ALTER TABLE [dbo].[UserProfileTranslation]  WITH CHECK ADD  CONSTRAINT [FK_UserProfileTranslation_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([Id])
GO
ALTER TABLE [dbo].[UserProfileTranslation] CHECK CONSTRAINT [FK_UserProfileTranslation_UserProfile]
GO
USE [master]
GO
ALTER DATABASE [LearningManagementSystem] SET  READ_WRITE 
GO
