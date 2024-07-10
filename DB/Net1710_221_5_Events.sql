USE [master]
GO
/****** Object:  Database [Net1710_221_5_Events]    Script Date: 5/19/2024 8:15:03 PM ******/
CREATE DATABASE [Net1710_221_5_Events]

ALTER DATABASE [Net1710_221_5_Events] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Net1710_221_5_Events].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Net1710_221_5_Events] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET ARITHABORT OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Net1710_221_5_Events] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Net1710_221_5_Events] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Net1710_221_5_Events] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Net1710_221_5_Events] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Net1710_221_5_Events] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Net1710_221_5_Events] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET RECOVERY FULL 
GO
ALTER DATABASE [Net1710_221_5_Events] SET  MULTI_USER 
GO
ALTER DATABASE [Net1710_221_5_Events] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Net1710_221_5_Events] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Net1710_221_5_Events] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Net1710_221_5_Events] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Net1710_221_5_Events] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Net1710_221_5_Events] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Net1710_221_5_Events] SET QUERY_STORE = OFF
GO

USE [Net1710_221_5_Events]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 21/05/2024 7:47:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[BusinessSector] [nvarchar](100) NOT NULL,
	[TaxesId] [varchar](50) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 21/05/2024 7:47:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[Gender] [nvarchar](15) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[City] [nvarchar](250) NOT NULL,
	[Country] [nvarchar](250) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 21/05/2024 7:47:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Location] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[ImageLink] [nvarchar](max) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[OpenTicket] [datetime2](7) NOT NULL,
	[CloseTicket] [datetime2](7) NOT NULL,
	[TicketPrice] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[OperatorName] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 21/05/2024 7:47:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[TicketQuantity] [int] NOT NULL,
	[TotalAmount] [int] NOT NULL,
	[PaymentStatus] [nvarchar](25) NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[PaymentMethod] [nvarchar](12) NOT NULL,
	[Status] [nvarchar](25) NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 21/05/2024 7:47:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 21/05/2024 7:47:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[QRCode] [nvarchar](max) NOT NULL,
	[ParticipantName] [nvarchar](50) NOT NULL,
	[ParticipantMail] [nvarchar](100) NOT NULL,
	[ParticipantPhone] [nvarchar](15) NOT NULL,
	[SpecialNote] [nvarchar](max) NOT NULL,
	[TicketType] [nvarchar](50) NOT NULL,
	[OrderDetailId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer_CustomerId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Event_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Event_EventId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Order_OrderId]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_OrderDetails_OrderDetailId] FOREIGN KEY([OrderDetailId])
REFERENCES [dbo].[OrderDetails] ([OrderDetailId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_OrderDetails_OrderDetailId]
GO

ALTER TABLE [dbo].[Customer] ADD IsDelete BIT DEFAULT 0;
ALTER TABLE [dbo].[Order] ADD IsDelete BIT DEFAULT 0;
ALTER TABLE [dbo].[Event] ADD IsDelete BIT DEFAULT 0;
ALTER TABLE [dbo].[OrderDetails] ADD IsDelete BIT DEFAULT 0;
ALTER TABLE [dbo].[Ticket] ADD IsDelete BIT DEFAULT 0;
ALTER TABLE [dbo].[Company] ADD IsDelete BIT DEFAULT 0;
