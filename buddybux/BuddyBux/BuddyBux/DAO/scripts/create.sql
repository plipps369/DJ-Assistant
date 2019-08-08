USE [master]
GO
/****** Object:  Database [BuddyBux]    Script Date: 6/3/2019 5:24:24 PM ******/
CREATE DATABASE [BuddyBux]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CryptIOU', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CryptIOU.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CryptIOU_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CryptIOU_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BuddyBux] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BuddyBux].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BuddyBux] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BuddyBux] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BuddyBux] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BuddyBux] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BuddyBux] SET ARITHABORT OFF 
GO
ALTER DATABASE [BuddyBux] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BuddyBux] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BuddyBux] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BuddyBux] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BuddyBux] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BuddyBux] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BuddyBux] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BuddyBux] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BuddyBux] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BuddyBux] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BuddyBux] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BuddyBux] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BuddyBux] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BuddyBux] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BuddyBux] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BuddyBux] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BuddyBux] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BuddyBux] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BuddyBux] SET  MULTI_USER 
GO
ALTER DATABASE [BuddyBux] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BuddyBux] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BuddyBux] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BuddyBux] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BuddyBux] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BuddyBux] SET QUERY_STORE = OFF
GO
USE [BuddyBux]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [BuddyBux]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 6/3/2019 5:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ImageFileId] [int] NULL,
	[OwnerId] [int] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Currency_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Currency_OwnerId] UNIQUE NONCLUSTERED 
(
	[OwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[File]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginalName] [varchar](250) NOT NULL,
	[StorageName] [varchar](50) NOT NULL,
	[StoragePath] [varchar](500) NOT NULL,
	[Size] [int] NOT NULL,
	[Hash] [varchar](50) NOT NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_File] UNIQUE NONCLUSTERED 
(
	[StorageName] ASC,
	[StoragePath] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ImageFileId] [int] NULL,
	[IsCharitable] [bit] NOT NULL,
	[CreatorId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreProduct]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[Cost] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
 CONSTRAINT [PK_StoreProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trade]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginatorId] [int] NOT NULL,
	[DesigneeId] [int] NOT NULL,
	[ProvidedCurrencyId] [int] NOT NULL,
	[RequestedCurrencyId] [int] NOT NULL,
	[ProvidedCurrencyQty] [int] NOT NULL,
	[RequestedCurrencyQty] [int] NOT NULL,
	[TradeStatusId] [int] NOT NULL,
 CONSTRAINT [PK_Trade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TradeStatus]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TradeStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TradeStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreProductId] [int] NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[Qty] [int] NOT NULL,
	[PurchaserId] [int] NOT NULL,
	[Cost] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Hash] [varchar](50) NOT NULL,
	[Salt] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ImageFileId] [int] NULL,
	[Rating] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCurrency]    Script Date: 6/3/2019 5:24:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCurrency](
	[CurrencyId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_UserCurrency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_UserCurrency] UNIQUE NONCLUSTERED 
(
	[CurrencyId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Currency]  WITH CHECK ADD  CONSTRAINT [FK_Currency_File] FOREIGN KEY([ImageFileId])
REFERENCES [dbo].[File] ([Id])
GO
ALTER TABLE [dbo].[Currency] CHECK CONSTRAINT [FK_Currency_File]
GO
ALTER TABLE [dbo].[Currency]  WITH CHECK ADD  CONSTRAINT [FK_Currency_User] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Currency] CHECK CONSTRAINT [FK_Currency_User]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_File] FOREIGN KEY([ImageFileId])
REFERENCES [dbo].[File] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_File]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_User] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_User]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_User]
GO
ALTER TABLE [dbo].[StoreProduct]  WITH CHECK ADD  CONSTRAINT [FK_StoreProduct_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[StoreProduct] CHECK CONSTRAINT [FK_StoreProduct_Currency]
GO
ALTER TABLE [dbo].[StoreProduct]  WITH CHECK ADD  CONSTRAINT [FK_StoreProduct_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[StoreProduct] CHECK CONSTRAINT [FK_StoreProduct_Product]
GO
ALTER TABLE [dbo].[StoreProduct]  WITH CHECK ADD  CONSTRAINT [FK_StoreProduct_Store] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[StoreProduct] CHECK CONSTRAINT [FK_StoreProduct_Store]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Currency_Provided] FOREIGN KEY([ProvidedCurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Currency_Provided]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Currency_Requested] FOREIGN KEY([RequestedCurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Currency_Requested]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_TradeStatus] FOREIGN KEY([TradeStatusId])
REFERENCES [dbo].[TradeStatus] ([Id])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_TradeStatus]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_User_Designee] FOREIGN KEY([DesigneeId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_User_Designee]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_User_Originator] FOREIGN KEY([OriginatorId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_User_Originator]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Currency]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_StoreProduct] FOREIGN KEY([StoreProductId])
REFERENCES [dbo].[StoreProduct] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_StoreProduct]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_User] FOREIGN KEY([PurchaserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_File] FOREIGN KEY([ImageFileId])
REFERENCES [dbo].[File] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_File]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[UserCurrency]  WITH CHECK ADD  CONSTRAINT [FK_UserCurrency_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCurrency] CHECK CONSTRAINT [FK_UserCurrency_Currency]
GO
ALTER TABLE [dbo].[UserCurrency]  WITH CHECK ADD  CONSTRAINT [FK_UserCurrency_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCurrency] CHECK CONSTRAINT [FK_UserCurrency_User]
GO
USE [master]
GO
ALTER DATABASE [BuddyBux] SET  READ_WRITE 
GO
