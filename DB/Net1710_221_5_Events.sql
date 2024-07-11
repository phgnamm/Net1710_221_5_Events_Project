USE [master]

GO
CREATE DATABASE [Net1710_221_5_Events]
GO

USE [Net1710_221_5_Events]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/07/2024 8:33:48 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[CompanyPhone] [varchar](10) NOT NULL,
	[BusinessSector] [nvarchar](100) NOT NULL,
	[TaxesId] [varchar](50) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[City] [nvarchar](250) NOT NULL,
	[Country] [nvarchar](250) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/07/2024 8:33:48 CH ******/
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
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 11/07/2024 8:33:48 CH ******/
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
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/07/2024 8:33:48 CH ******/
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
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/07/2024 8:33:48 CH ******/
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
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 11/07/2024 8:33:48 CH ******/
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
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyId], [Name], [CompanyPhone], [BusinessSector], [TaxesId], [Address], [City], [Country], [CreatedDate], [UpdatedDate], [IsDelete]) VALUES (2, N'FPT Edu', N'0911911921', N'Education', N'1234567890', N'Ho Chi Minh', N'Ho Chi Minh', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Company] ([CompanyId], [Name], [CompanyPhone], [BusinessSector], [TaxesId], [Address], [City], [Country], [CreatedDate], [UpdatedDate], [IsDelete]) VALUES (3, N'FPT Software HCM', N'0989899899', N'Software', N'1234567890', N'Ho Chi Minh', N'Ho Chi Minh', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Company] ([CompanyId], [Name], [CompanyPhone], [BusinessSector], [TaxesId], [Address], [City], [Country], [CreatedDate], [UpdatedDate], [IsDelete]) VALUES (4, N'KMS', N'0123456789', N'Software', N'1234567890', N'Ho Chi Minh', N'Ho Chi Minh', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Company] ([CompanyId], [Name], [CompanyPhone], [BusinessSector], [TaxesId], [Address], [City], [Country], [CreatedDate], [UpdatedDate], [IsDelete]) VALUES (5, N'Vinamilk', N'0909233244', N'Business', N'1234567890', N'Ho Chi Minh', N'Ho Chi Minh', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [FullName], [Email], [PhoneNumber], [Gender], [DateOfBirth], [Address], [City], [Country], [CreateDate], [IsDelete]) VALUES (2, N'Dang Gia Duc', N'giaducdang@gmail.com', N'0909113114', N'MALE', CAST(N'2003-02-01T00:00:00.0000000' AS DateTime2), N'Long An', N'Tan An', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Customer] ([CustomerId], [FullName], [Email], [PhoneNumber], [Gender], [DateOfBirth], [Address], [City], [Country], [CreateDate], [IsDelete]) VALUES (3, N'Nguyen Thanh Tan', N'tannt@gmail.com', N'0912343333', N'MALE', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), N'Dong Thap', N'Dong Thap', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Customer] ([CustomerId], [FullName], [Email], [PhoneNumber], [Gender], [DateOfBirth], [Address], [City], [Country], [CreateDate], [IsDelete]) VALUES (4, N'Van Hoang Tien', N'tienvh@gmail.com', N'0123456789', N'MALE', CAST(N'2003-01-01T00:00:00.0000000' AS DateTime2), N'Ho Chi Minh', N'Ho Chi Minh', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Customer] ([CustomerId], [FullName], [Email], [PhoneNumber], [Gender], [DateOfBirth], [Address], [City], [Country], [CreateDate], [IsDelete]) VALUES (5, N'Dang Phuong Nam', N'namdang@gmail.com', N'0388237832', N'MALE', CAST(N'2003-01-01T00:00:00.0000000' AS DateTime2), N'Ho Chi Minh', N'Ho Chi Minh', N'Viet Nam', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([EventId], [Name], [Location], [Description], [ImageLink], [StartDate], [EndDate], [OpenTicket], [CloseTicket], [TicketPrice], [Quantity], [OperatorName], [IsDelete]) VALUES (1, N'Seminar EXE 1', N'NVH', N'Seminar EXE danh cho sinh vien FU', N'https://vcdn1-vnexpress.vnecdn.net/2019/10/24/1-1571885998-1571889781.jpg?w=460&h=0&q=100&dpr=2&fit=crop&s=dx1QkqlqAPDSN3GzmcI0og', CAST(N'2024-07-13T08:00:00.0000000' AS DateTime2), CAST(N'2024-07-14T20:00:00.0000000' AS DateTime2), CAST(N'2024-07-11T20:26:00.0000000' AS DateTime2), CAST(N'2024-07-12T20:26:00.0000000' AS DateTime2), 90000, 120, N'FPT University HCM', 0)
INSERT [dbo].[Event] ([EventId], [Name], [Location], [Description], [ImageLink], [StartDate], [EndDate], [OpenTicket], [CloseTicket], [TicketPrice], [Quantity], [OperatorName], [IsDelete]) VALUES (2, N'Seminar EXE 2', N'FPT University HCM', N'Su kien huong nghiep danh cho sinh vien FPT', N'https://daihoc.fpt.edu.vn/templates/fpt-university/images/header.jpg', CAST(N'2024-07-14T20:28:00.0000000' AS DateTime2), CAST(N'2024-07-15T20:28:00.0000000' AS DateTime2), CAST(N'2024-07-12T20:28:00.0000000' AS DateTime2), CAST(N'2024-07-13T20:28:00.0000000' AS DateTime2), 120000, 100, N'FPT University HCM', 0)
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF__Company__IsDelet__5BE2A6F2]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Ticket] ADD  DEFAULT ((0)) FOR [IsDelete]
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
