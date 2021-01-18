CREATE DATABASE lachlanbarclaynet
go
USE [lachlanbarclaynet]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 18/01/2021 12:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostDescription] [nvarchar](255) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[PostText] [nvarchar](max) NULL,
	[PostTypeID] [int] NOT NULL,
	[PostTitle] [nvarchar](255) NOT NULL,
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[PostUrl] [nvarchar](255) NULL,
	[Published] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComment](
	[PostCommentId] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[PostCommentDate] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[IsVisible] [bit] NULL,
	[PostID] [int] NULL,
 CONSTRAINT [PK_PostComment] PRIMARY KEY CLUSTERED 
(
	[PostCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostType](
	[PostTypeID] [int] NOT NULL,
	[PostTypeName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_PostType] PRIMARY KEY CLUSTERED 
(
	[PostTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NULL,
	[Password] [nvarchar](255) NULL,
	[QrCode] [nvarchar](255) NULL,
	[Attempts] [int] NOT NULL,
	[LockedOutUntil] [datetime] NULL,
	[UserEmail] [nvarchar](255) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[EmailConfirmCode] [nvarchar](50) NULL,
	[PasswordForgotCode] [nvarchar](50) NULL,
	[IsQuizAdmin] [bit] NOT NULL,
	[DoNotEmail] [bit] NOT NULL,
	[AboutMe] [nvarchar](1000) NULL,
	[NerdCred] [decimal](9, 2) NULL,
	[AvgScore] [decimal](9, 2) NULL,
	[QuizzesCompleted] [decimal](9, 2) NULL,
	[SpeakerID] [int] NULL,
	[QrCodeTemp] [varchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Post] ADD  DEFAULT ((1)) FOR [Published]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Attempts]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [EmailConfirmed]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsQuizAdmin]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [DoNotEmail]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD FOREIGN KEY([PostTypeID])
REFERENCES [dbo].[PostType] ([PostTypeID])
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD FOREIGN KEY([PostTypeID])
REFERENCES [dbo].[PostType] ([PostTypeID])
GO
ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD  CONSTRAINT [FK_PostComment_Post] FOREIGN KEY([PostID])
REFERENCES [dbo].[Post] ([PostID])
GO
ALTER TABLE [dbo].[PostComment] CHECK CONSTRAINT [FK_PostComment_Post]
GO
INSERT [dbo].[PostType] ([PostTypeID], [PostTypeName]) VALUES (1, N'technical')
GO
INSERT [dbo].[PostType] ([PostTypeID], [PostTypeName]) VALUES (2, N'music')
GO
INSERT [dbo].[PostType] ([PostTypeID], [PostTypeName]) VALUES (3, N'other')
GO
insert into Users (USername, password, qrcode) values ('admin', null, null)
go
