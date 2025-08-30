
create database [naidisprojekt];
go

USE [naidisprojekt]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 30/08/2025 22:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](100) NULL,
	[category_image] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Listings]    Script Date: 30/08/2025 22:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Listings](
	[listing_id] [int] IDENTITY(1,1) NOT NULL,
	[price] [decimal](10, 2) NULL,
	[listing_name] [nvarchar](100) NULL,
	[listing_description] [nvarchar](max) NULL,
	[listing_image] [varbinary](max) NULL,
	[category_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[listing_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFavorites]    Script Date: 30/08/2025 22:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFavorites](
	[listing_id] [int] NULL,
	[user_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserListings]    Script Date: 30/08/2025 22:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserListings](
	[listing_id] [int] NULL,
	[user_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 30/08/2025 22:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[password] [varbinary](512) NULL,
	[created_at] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (sysdatetime()) FOR [created_at]
GO
ALTER TABLE [dbo].[Listings]  WITH CHECK ADD  CONSTRAINT [Listings_Categories_FK] FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO
ALTER TABLE [dbo].[Listings] CHECK CONSTRAINT [Listings_Categories_FK]
GO
ALTER TABLE [dbo].[UserFavorites]  WITH CHECK ADD  CONSTRAINT [UserFavorites_Listings] FOREIGN KEY([listing_id])
REFERENCES [dbo].[Listings] ([listing_id])
GO
ALTER TABLE [dbo].[UserFavorites] CHECK CONSTRAINT [UserFavorites_Listings]
GO
ALTER TABLE [dbo].[UserFavorites]  WITH CHECK ADD  CONSTRAINT [UserFavorites_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[UserFavorites] CHECK CONSTRAINT [UserFavorites_Users]
GO
ALTER TABLE [dbo].[UserListings]  WITH CHECK ADD  CONSTRAINT [UserListings_Listings] FOREIGN KEY([listing_id])
REFERENCES [dbo].[Listings] ([listing_id])
GO
ALTER TABLE [dbo].[UserListings] CHECK CONSTRAINT [UserListings_Listings]
GO
ALTER TABLE [dbo].[UserListings]  WITH CHECK ADD  CONSTRAINT [UserListings_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[UserListings] CHECK CONSTRAINT [UserListings_Users]
GO
