USE [MyShop]
GO
/****** Object:  Table [dbo].[_user]    Script Date: 05/01/2024 10:28:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_user](
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[UserID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 05/01/2024 10:28:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CatName] [nvarchar](50) NULL,
	[CatLogo] [nvarchar](100) NULL,
	[CatDescription] [nvarchar](max) NULL,
	[CatID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 05/01/2024 10:28:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[ProName] [nvarchar](100) NULL,
	[Ram] [int] NULL,
	[ScreenSize] [float] NULL,
	[Price] [int] NULL,
	[ImagePath] [nvarchar](100) NULL,
	[BatteryCapacity] [int] NULL,
	[CatID] [int] NULL,
	[Rom] [int] NULL,
	[PromoID] [int] NULL,
	[PromotionPrice] [money] NULL,
	[Quantity] [int] NULL,
	[Manufacturer] [nvarchar](100) NULL,
	[ProID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[promotion]    Script Date: 05/01/2024 10:28:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[promotion](
	[PromoCode] [nvarchar](50) NULL,
	[DiscountPercent] [int] NULL,
	[PromoID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PromoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase]    Script Date: 05/01/2024 10:28:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase](
	[OrderID] [int] NULL,
	[ProID] [int] NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [money] NULL,
	[PurchaseID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_purchase] PRIMARY KEY CLUSTERED 
(
	[PurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shop_order]    Script Date: 05/01/2024 10:28:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shop_order](
	[CusName] [nvarchar](50) NULL,
	[CusAddress] [nvarchar](100) NULL,
	[OrderDate] [date] NULL,
	[PriceTotal] [money] NULL,
	[ProfitTotal] [money] NULL,
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_category] FOREIGN KEY([CatID])
REFERENCES [dbo].[category] ([CatID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_category]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_promotion] FOREIGN KEY([PromoID])
REFERENCES [dbo].[promotion] ([PromoID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_promotion]
GO
ALTER TABLE [dbo].[purchase]  WITH CHECK ADD  CONSTRAINT [FK_purchase_order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[shop_order] ([OrderID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[purchase] CHECK CONSTRAINT [FK_purchase_order]
GO
ALTER TABLE [dbo].[purchase]  WITH CHECK ADD  CONSTRAINT [FK_purchase_product] FOREIGN KEY([ProID])
REFERENCES [dbo].[product] ([ProID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[purchase] CHECK CONSTRAINT [FK_purchase_product]
GO
