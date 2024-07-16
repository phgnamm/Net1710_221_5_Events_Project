﻿USE [master]

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
Go
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [Code], [Description], [TicketQuantity], [TotalAmount], [PaymentStatus], [PaymentDate], [PaymentMethod], [Status], [CustomerId], [IsDelete]) VALUES (1, N'ORD001', N'Order', 2, 600, N'Paid', CAST(N'2024-05-15T00:00:00.0000000' AS DateTime2), N'Credit Card', N'Confirmed', 2, 0)
INSERT [dbo].[Order] ([OrderId], [Code], [Description], [TicketQuantity], [TotalAmount], [PaymentStatus], [PaymentDate], [PaymentMethod], [Status], [CustomerId], [IsDelete]) VALUES (2, N'ORD002', N'Order', 4, 200, N'Paid', CAST(N'2024-04-15T00:00:00.0000000' AS DateTime2), N'PayPal', N'Confirmed', 2, 0)
INSERT [dbo].[Order] ([OrderId], [Code], [Description], [TicketQuantity], [TotalAmount], [PaymentStatus], [PaymentDate], [PaymentMethod], [Status], [CustomerId], [IsDelete]) VALUES (3, N'ORD003', N'Order', 3, 600, N'Paid', CAST(N'2024-03-10T00:00:00.0000000' AS DateTime2), N'Credit Card', N'Confirmed', 3, 0)
INSERT [dbo].[Order] ([OrderId], [Code], [Description], [TicketQuantity], [TotalAmount], [PaymentStatus], [PaymentDate], [PaymentMethod], [Status], [CustomerId], [IsDelete]) VALUES (4, N'1', N'1', 1, 1, N'Pending', CAST(N'1111-01-01T00:00:00.0000000' AS DateTime2), N'1', N'New', 2, 0)
INSERT [dbo].[Order] ([OrderId], [Code], [Description], [TicketQuantity], [TotalAmount], [PaymentStatus], [PaymentDate], [PaymentMethod], [Status], [CustomerId], [IsDelete]) VALUES (5, N'1', N'1', 1, 1, N'Pending', CAST(N'1111-11-11T00:00:00.0000000' AS DateTime2), N'1', N'Cancelled', 2, 0)
INSERT [dbo].[Order] ([OrderId], [Code], [Description], [TicketQuantity], [TotalAmount], [PaymentStatus], [PaymentDate], [PaymentMethod], [Status], [CustomerId], [IsDelete]) VALUES (6, N'1', N'1', 1, 1, N'Pending', CAST(N'1111-11-11T00:00:00.0000000' AS DateTime2), N'1', N'Cancelled', 2, 0)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailId], [OrderId], [EventId], [Quantity], [Price], [IsDelete]) VALUES (1, 1, 1, 2, CAST(300.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[OrderDetails] ([OrderDetailId], [OrderId], [EventId], [Quantity], [Price], [IsDelete]) VALUES (2, 2, 2, 4, CAST(50.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[OrderDetails] ([OrderDetailId], [OrderId], [EventId], [Quantity], [Price], [IsDelete]) VALUES (3, 3, 1, 3, CAST(200.00 AS Decimal(18, 2)), 0)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (1, N'TK01', N'QR1', N'Nguyen Van An', N'annguyen@gmail.com', N'0901 234 567', N'None', N'VIP', 1, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (2, N'TK02', N'QR2', N'Tran Thi Bao', N'baotran@gmail.com', N'0912 345 678', N'None', N'Regular', 1, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Inactive', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (3, N'TK03', N'QR3', N'Le Minh Cuong', N'cuongle@gmail.com', N'0923 456 789', N'Wheelchair Access', N'VIP', 1, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (4, N'TK04', N'QR4', N'Pham Thanh Dung', N'dungpham@gmail.com', N'0934 567 890', N'None', N'Regular', 2, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (5, N'TK05', N'QR5', N'Hoang Van Hai', N'haihoang@gmail.com', N'0945 678 901', N'None', N'Regular', 2, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (6, N'TK06', N'QR6', N'Dang Ngoc Linh', N'linhdang@gmail.com', N'0956 789 012', N'None', N'VIP', 1, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (7, N'TK07', N'QR7', N'Bui Tien Minh', N'minhbui@gmail.com', N'0967 890 123', N'None', N'Regular', 1, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (8, N'TK08', N'QR8', N'Vu Thu Ngoc', N'ngocvu@gmail.com', N'0978 901 234', N'None', N'Regular', 1, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (9, N'TK09', N'QR9', N'Nguyen Hoai Thao', N'thaonguyen@gmail.com', N'0989 012 345', N'None', N'VIP', 2, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
INSERT [dbo].[Ticket] ([TicketId], [Code], [QRCode], [ParticipantName], [ParticipantMail], [ParticipantPhone], [SpecialNote], [TicketType], [OrderDetailId], [CreatedDate], [Status], [IsDelete]) VALUES (10, N'TK10', N'QR10', N'Phan Quoc Tuan', N'tuanphan@gmail.com', N'0990 123 456', N'None', N'Regular', 2, CAST(N'2024-07-10T11:45:04.7921215' AS DateTime2), N'Active', 0)
SET IDENTITY_INSERT [dbo].[Ticket] OFF
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
