USE [master]
GO
/****** Object:  Database [TopicTalks] ******/
CREATE DATABASE [TopicTalks]
GO
USE [TopicTalks]
GO
/****** Object:  Schema [auth] ******/
CREATE SCHEMA [auth]
GO
/****** Object:  Schema [core] ******/
CREATE SCHEMA [core]
GO
/****** Object:  Schema [enum] ******/
CREATE SCHEMA [enum]
GO
/****** Object:  Schema [post] ******/
CREATE SCHEMA [post]
GO
/****** Object:  Table [auth].[Otps] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[Otps](
	[Email] [nvarchar](255) NOT NULL,
	[Code] [nvarchar](6) NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Otps] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[UserDetails] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[UserDetails](
	[UserDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[InstituteName] [nvarchar](255) NOT NULL,
	[IdCardNumber] [nvarchar](50) NOT NULL,
	[UserId] [bigint] NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[UserRoles] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[UserRoles](
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [auth].[Users] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [auth].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Salt] [nvarchar](255) NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ImageFileId] [nvarchar](255) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[CloudFiles] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[CloudFiles](
	[CloudFileId] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ContentType] [nvarchar](255) NOT NULL,
	[Size] [bigint] NOT NULL,
	[WebContentLink] [nvarchar](255) NOT NULL,
	[WebViewLink] [nvarchar](255) NOT NULL,
	[DirectLink] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UserId] [bigint] NULL,
 CONSTRAINT [PK_CloudFiles] PRIMARY KEY CLUSTERED 
(
	[CloudFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogEvents] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
 CONSTRAINT [PK_LogEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MigrationsHistory] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_MigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [enum].[Roles] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [enum].[Roles](
	[RoleId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [post].[Answers] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [post].[Answers](
	[AnswerId] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentAnswerId] [bigint] NULL,
	[Explanation] [nvarchar](max) NOT NULL,
	[IsNotified] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UserId] [bigint] NULL,
	[QuestionId] [bigint] NOT NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [post].[Questions] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [post].[Questions](
	[QuestionId] [bigint] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](50) NOT NULL,
	[Explanation] [nvarchar](max) NOT NULL,
	[IsNotified] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[UserId] [bigint] NULL,
	[ImageFileId] [nvarchar](255) NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [auth].[UserDetails] ON 
GO
INSERT [auth].[UserDetails] ([UserDetailsId], [FullName], [InstituteName], [IdCardNumber], [UserId]) VALUES (1, N'Zaid Amin Rawfin', N'AIUB', N'20-42459-1', 1)
GO
INSERT [auth].[UserDetails] ([UserDetailsId], [FullName], [InstituteName], [IdCardNumber], [UserId]) VALUES (2, N'Oweo Yec Wev', N'QWDA', N'29-1655-31', 4)
GO
INSERT [auth].[UserDetails] ([UserDetailsId], [FullName], [InstituteName], [IdCardNumber], [UserId]) VALUES (3, N'Voer Eor Oec', N'CREX', N'3-16614-43', 5)
GO
INSERT [auth].[UserDetails] ([UserDetailsId], [FullName], [InstituteName], [IdCardNumber], [UserId]) VALUES (6, N'Zcnow Oewb Onc', N'CWEC', N'13-51534-6', 6)
GO
INSERT [auth].[UserDetails] ([UserDetailsId], [FullName], [InstituteName], [IdCardNumber], [UserId]) VALUES (7, N'Wqepx Aorne Xovo', N'XOVO', N'14-035-453', 7)
GO
INSERT [auth].[UserDetails] ([UserDetailsId], [FullName], [InstituteName], [IdCardNumber], [UserId]) VALUES (8, N'Pesve Oenpw', N'PCXOW', N'51-26532-1', 8)
GO
INSERT [auth].[UserDetails] ([UserDetailsId], [FullName], [InstituteName], [IdCardNumber], [UserId]) VALUES (9, N'Xjow Cbree', N'COEP', N'36-4565-15', 9)
GO
SET IDENTITY_INSERT [auth].[UserDetails] OFF
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (4, 1)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (5, 1)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (6, 1)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (7, 1)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (8, 1)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (9, 1)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (2, 2)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (10, 2)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (11, 2)
GO
INSERT [auth].[UserRoles] ([UserId], [RoleId]) VALUES (3, 3)
GO
SET IDENTITY_INSERT [auth].[Users] ON 
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (1, N'Rawfin', N'hello@rawfin.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 1, CAST(N'2024-01-27T13:13:46.463' AS DateTime), N'1s2pAFC-YdhvCQdE8qtJx6GCPUQ_AL7Rb')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (2, N'Doe', N'doe@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-01-27T13:13:46.463' AS DateTime), N'1RH6EHcpS-D9cyiq5sTVfy0VRSKdOEhER')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (3, N'Bob', N'bob@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-01-27T13:13:46.463' AS DateTime), N'1HFwyzkIXtEeRg6VlzFt2DyZPPvQQMhtC')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (4, N'Oweo', N'oec@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-01-27T13:13:46.463' AS DateTime), N'163syanIdd5XjZs-u2ubCDdytYqQ0SeWx')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (5, N'Eorc', N'eor@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-01-27T13:13:46.463' AS DateTime), N'1D_8MB5PVseZUS1ZyWwE09TZZoJMR_yqV')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (6, N'Zcnow', N'onaw@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-05-05T07:39:16.090' AS DateTime), N'1qL7VdGmvTxO7ZJGfSjAudUfk8mjw5ZeF')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (7, N'Wqepx', N'qepx@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-05-05T07:37:10.270' AS DateTime), N'1IwkMeSwz_EdmFm9d40Sw72oXV09obcPh')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (8, N'Pesve', N'wfoi@@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-05-05T07:37:51.847' AS DateTime), N'1rFNZYv5egCA4gvMNyIE8PVwtppwuQ1t3')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (9, N'Xjow', N'wgvsa@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-05-05T07:38:14.193' AS DateTime), N'1Rr34cO4puqDz3L5KvmkDD7-kmDjfGm2l')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (10, N'Ownr', N'weofn@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-05-05T07:38:35.240' AS DateTime), N'1mjUahqIA0gE_KUR1k9F3GMlgsk1UHHTe')
GO
INSERT [auth].[Users] ([UserId], [Username], [Email], [PasswordHash], [Salt], [IsVerified], [CreatedAt], [ImageFileId]) VALUES (11, N'Uwnl', N'asfse@topictalks.net', N'AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==', N'vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=', 0, CAST(N'2024-05-05T07:39:03.157' AS DateTime), N'1DvXKSlEvGd-MJX7k0fAY18Yb4nubLQMW')
GO
SET IDENTITY_INSERT [auth].[Users] OFF
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'10QNwdTNdSf1VSckCwT6VL0_7JJohPrF8', N'question-2.jpg', N'image/jpeg', 193331, N'https://drive.google.com/uc?id=10QNwdTNdSf1VSckCwT6VL0_7JJohPrF8&export=download', N'https://drive.google.com/file/d/10QNwdTNdSf1VSckCwT6VL0_7JJohPrF8/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/10QNwdTNdSf1VSckCwT6VL0_7JJohPrF8', CAST(N'2024-05-05T13:06:21.477' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'163syanIdd5XjZs-u2ubCDdytYqQ0SeWx', N'student-1.jpg', N'image/jpeg', 153834, N'https://drive.google.com/uc?id=163syanIdd5XjZs-u2ubCDdytYqQ0SeWx&export=download', N'https://drive.google.com/file/d/163syanIdd5XjZs-u2ubCDdytYqQ0SeWx/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/163syanIdd5XjZs-u2ubCDdytYqQ0SeWx', CAST(N'2024-05-05T07:07:59.473' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'17uG0VRB01PsSpdihChMZPlk9ZI0geMTO', N'question-5.png', N'image/png', 1472225, N'https://drive.google.com/uc?id=17uG0VRB01PsSpdihChMZPlk9ZI0geMTO&export=download', N'https://drive.google.com/file/d/17uG0VRB01PsSpdihChMZPlk9ZI0geMTO/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/17uG0VRB01PsSpdihChMZPlk9ZI0geMTO', CAST(N'2024-05-05T13:06:44.557' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'192iT9x42YzDf3mAI6LWEUvSBVpe06gcw', N'question-3.jpg', N'image/jpeg', 1016725, N'https://drive.google.com/uc?id=192iT9x42YzDf3mAI6LWEUvSBVpe06gcw&export=download', N'https://drive.google.com/file/d/192iT9x42YzDf3mAI6LWEUvSBVpe06gcw/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/192iT9x42YzDf3mAI6LWEUvSBVpe06gcw', CAST(N'2024-05-05T07:21:03.247' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1BirBbhwoQj6TF0mtCkGNFCNWDnxOtW85', N'question-4.jpg', N'image/jpeg', 1477028, N'https://drive.google.com/uc?id=1BirBbhwoQj6TF0mtCkGNFCNWDnxOtW85&export=download', N'https://drive.google.com/file/d/1BirBbhwoQj6TF0mtCkGNFCNWDnxOtW85/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1BirBbhwoQj6TF0mtCkGNFCNWDnxOtW85', CAST(N'2024-05-05T13:06:36.787' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1D_8MB5PVseZUS1ZyWwE09TZZoJMR_yqV', N'student-2.jpg', N'image/jpeg', 162367, N'https://drive.google.com/uc?id=1D_8MB5PVseZUS1ZyWwE09TZZoJMR_yqV&export=download', N'https://drive.google.com/file/d/1D_8MB5PVseZUS1ZyWwE09TZZoJMR_yqV/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1D_8MB5PVseZUS1ZyWwE09TZZoJMR_yqV', CAST(N'2024-05-05T07:08:07.310' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1DvXKSlEvGd-MJX7k0fAY18Yb4nubLQMW', N'teacher-3.jpg', N'image/jpeg', 152302, N'https://drive.google.com/uc?id=1DvXKSlEvGd-MJX7k0fAY18Yb4nubLQMW&export=download', N'https://drive.google.com/file/d/1DvXKSlEvGd-MJX7k0fAY18Yb4nubLQMW/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1DvXKSlEvGd-MJX7k0fAY18Yb4nubLQMW', CAST(N'2024-05-05T07:09:43.507' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1FO-ccpvykk5ARPYDHbrmUV_hSwSjVxDU', N'student-7.jpg', N'image/jpeg', 159874, N'https://drive.google.com/uc?id=1FO-ccpvykk5ARPYDHbrmUV_hSwSjVxDU&export=download', N'https://drive.google.com/file/d/1FO-ccpvykk5ARPYDHbrmUV_hSwSjVxDU/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1FO-ccpvykk5ARPYDHbrmUV_hSwSjVxDU', CAST(N'2024-05-05T07:08:49.187' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1HFwyzkIXtEeRg6VlzFt2DyZPPvQQMhtC', N'teacher-4.jpg', N'image/jpeg', 154151, N'https://drive.google.com/uc?id=1HFwyzkIXtEeRg6VlzFt2DyZPPvQQMhtC&export=download', N'https://drive.google.com/file/d/1HFwyzkIXtEeRg6VlzFt2DyZPPvQQMhtC/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1HFwyzkIXtEeRg6VlzFt2DyZPPvQQMhtC', CAST(N'2024-05-05T07:09:50.987' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1IwkMeSwz_EdmFm9d40Sw72oXV09obcPh', N'student-3.jpg', N'image/jpeg', 162593, N'https://drive.google.com/uc?id=1IwkMeSwz_EdmFm9d40Sw72oXV09obcPh&export=download', N'https://drive.google.com/file/d/1IwkMeSwz_EdmFm9d40Sw72oXV09obcPh/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1IwkMeSwz_EdmFm9d40Sw72oXV09obcPh', CAST(N'2024-05-05T07:08:15.070' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1mjUahqIA0gE_KUR1k9F3GMlgsk1UHHTe', N'teacher-2.jpg', N'image/jpeg', 151696, N'https://drive.google.com/uc?id=1mjUahqIA0gE_KUR1k9F3GMlgsk1UHHTe&export=download', N'https://drive.google.com/file/d/1mjUahqIA0gE_KUR1k9F3GMlgsk1UHHTe/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1mjUahqIA0gE_KUR1k9F3GMlgsk1UHHTe', CAST(N'2024-05-05T07:09:35.803' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1oPg6rqgogFDLy3NbKMNngRhaXHlh8DDu', N'question-1.jpg', N'image/jpeg', 228175, N'https://drive.google.com/uc?id=1oPg6rqgogFDLy3NbKMNngRhaXHlh8DDu&export=download', N'https://drive.google.com/file/d/1oPg6rqgogFDLy3NbKMNngRhaXHlh8DDu/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1oPg6rqgogFDLy3NbKMNngRhaXHlh8DDu', CAST(N'2024-05-05T13:05:17.187' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1QJXdin6maCowgCnfG0yKb5az5kLxDwpU', N'student-10.jpg', N'image/jpeg', 148371, N'https://drive.google.com/uc?id=1QJXdin6maCowgCnfG0yKb5az5kLxDwpU&export=download', N'https://drive.google.com/file/d/1QJXdin6maCowgCnfG0yKb5az5kLxDwpU/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1QJXdin6maCowgCnfG0yKb5az5kLxDwpU', CAST(N'2024-05-05T07:09:13.897' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1qL7VdGmvTxO7ZJGfSjAudUfk8mjw5ZeF', N'student-8.jpg', N'image/jpeg', 152460, N'https://drive.google.com/uc?id=1qL7VdGmvTxO7ZJGfSjAudUfk8mjw5ZeF&export=download', N'https://drive.google.com/file/d/1qL7VdGmvTxO7ZJGfSjAudUfk8mjw5ZeF/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1qL7VdGmvTxO7ZJGfSjAudUfk8mjw5ZeF', CAST(N'2024-05-05T07:08:57.457' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1rFNZYv5egCA4gvMNyIE8PVwtppwuQ1t3', N'student-4.jpg', N'image/jpeg', 151835, N'https://drive.google.com/uc?id=1rFNZYv5egCA4gvMNyIE8PVwtppwuQ1t3&export=download', N'https://drive.google.com/file/d/1rFNZYv5egCA4gvMNyIE8PVwtppwuQ1t3/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1rFNZYv5egCA4gvMNyIE8PVwtppwuQ1t3', CAST(N'2024-05-05T07:08:22.913' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1RH6EHcpS-D9cyiq5sTVfy0VRSKdOEhER', N'teacher-1.jpg', N'image/jpeg', 160415, N'https://drive.google.com/uc?id=1RH6EHcpS-D9cyiq5sTVfy0VRSKdOEhER&export=download', N'https://drive.google.com/file/d/1RH6EHcpS-D9cyiq5sTVfy0VRSKdOEhER/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1RH6EHcpS-D9cyiq5sTVfy0VRSKdOEhER', CAST(N'2024-05-05T07:09:27.903' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1RlCmrr7URNDYpxGcJbwlEspXxOYNIsRZ', N'student-6.jpg', N'image/jpeg', 149004, N'https://drive.google.com/uc?id=1RlCmrr7URNDYpxGcJbwlEspXxOYNIsRZ&export=download', N'https://drive.google.com/file/d/1RlCmrr7URNDYpxGcJbwlEspXxOYNIsRZ/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1RlCmrr7URNDYpxGcJbwlEspXxOYNIsRZ', CAST(N'2024-05-05T07:08:41.283' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1Rr34cO4puqDz3L5KvmkDD7-kmDjfGm2l', N'student-5.jpg', N'image/jpeg', 153838, N'https://drive.google.com/uc?id=1Rr34cO4puqDz3L5KvmkDD7-kmDjfGm2l&export=download', N'https://drive.google.com/file/d/1Rr34cO4puqDz3L5KvmkDD7-kmDjfGm2l/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1Rr34cO4puqDz3L5KvmkDD7-kmDjfGm2l', CAST(N'2024-05-05T07:08:31.337' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1s2pAFC-YdhvCQdE8qtJx6GCPUQ_AL7Rb', N'Rawfin.jpg', N'image/jpeg', 760237, N'https://drive.google.com/uc?id=1s2pAFC-YdhvCQdE8qtJx6GCPUQ_AL7Rb&export=download', N'https://drive.google.com/file/d/1s2pAFC-YdhvCQdE8qtJx6GCPUQ_AL7Rb/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1s2pAFC-YdhvCQdE8qtJx6GCPUQ_AL7Rb', CAST(N'2024-05-05T07:07:40.983' AS DateTime), NULL)
GO
INSERT [core].[CloudFiles] ([CloudFileId], [Name], [ContentType], [Size], [WebContentLink], [WebViewLink], [DirectLink], [CreatedAt], [UserId]) VALUES (N'1ULRRQHUfbSKUbbWOn6k2v7vz5FIom7a-', N'student-9.jpg', N'image/jpeg', 152030, N'https://drive.google.com/uc?id=1ULRRQHUfbSKUbbWOn6k2v7vz5FIom7a-&export=download', N'https://drive.google.com/file/d/1ULRRQHUfbSKUbbWOn6k2v7vz5FIom7a-/view?usp=drivesdk', N'https://lh3.googleusercontent.com/d/1ULRRQHUfbSKUbbWOn6k2v7vz5FIom7a-', CAST(N'2024-05-05T07:09:05.490' AS DateTime), NULL)
GO
INSERT [dbo].[MigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240502131347_Init', N'8.0.3')
GO
SET IDENTITY_INSERT [enum].[Roles] ON 
GO
INSERT [enum].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Student')
GO
INSERT [enum].[Roles] ([RoleId], [RoleName]) VALUES (2, N'Teacher')
GO
INSERT [enum].[Roles] ([RoleId], [RoleName]) VALUES (3, N'Moderator')
GO
SET IDENTITY_INSERT [enum].[Roles] OFF
GO
SET IDENTITY_INSERT [post].[Answers] ON 
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (1, 0, N'Raspberry Pi OS (formerly known as Raspbian) is a Linux distribution specifically designed for the Raspberry Pi, based on Debian. It''s optimized for the Pi''s hardware, offering a lightweight desktop environment that''s well-suited for its low-powered platform. Raspberry Pi OS includes special programs and kernel modules for HAT and additional hardware support, which other operating systems like Ubuntu or Arch Linux might not have. While Ubuntu and Arch Linux are also Linux distributions, they are not specifically tailored for the Raspberry Pi and may require modifications to work with its unique hardware and software environment. Therefore, Raspberry Pi OS is often the preferred choice for users looking for a seamless experience with their Raspberry Pi devices.', 1, CAST(N'2024-04-05T07:10:00.000' AS DateTime), 2, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (2, 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit?', 1, CAST(N'2024-04-05T07:25:00.000' AS DateTime), 1, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (3, 2, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et.', 1, CAST(N'2024-04-05T07:34:00.000' AS DateTime), 4, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (4, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing.', 1, CAST(N'2024-04-05T07:46:00.000' AS DateTime), 2, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (5, 4, N'Lorem ipsum dolor sit amet.', 1, CAST(N'2024-04-05T07:59:00.000' AS DateTime), 5, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (6, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum.', 1, CAST(N'2024-05-05T07:33:37.740' AS DateTime), 3, 3)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (7, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et.', 1, CAST(N'2024-05-05T07:33:45.280' AS DateTime), 1, 3)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (8, 6, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed.', 1, CAST(N'2024-05-05T07:33:56.760' AS DateTime), 8, 3)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (9, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum.', 1, CAST(N'2024-05-05T07:34:01.593' AS DateTime), 2, 3)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (10, 8, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor.', 1, CAST(N'2024-05-05T07:34:09.837' AS DateTime), 10, 3)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (11, 7, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui.', 1, CAST(N'2024-05-05T07:34:15.740' AS DateTime), 3, 3)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (12, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum.', 1, CAST(N'2024-05-05T07:34:29.343' AS DateTime), 8, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (13, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor.', 1, CAST(N'2024-05-05T07:34:34.117' AS DateTime), 1, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (14, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui.', 1, CAST(N'2024-05-05T07:34:38.563' AS DateTime), 2, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (15, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui.', 1, CAST(N'2024-05-05T07:34:42.847' AS DateTime), 6, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (16, 14, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum vulputate.', 1, CAST(N'2024-05-05T07:34:49.477' AS DateTime), 7, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (17, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum vulputate diam, aliquam egestas nibh ante vel dui. Sed in tellus interdum eros vulputate placerat sed non enim. Pellentesque eget justo porttitor urna dictum fermentum sit amet sed mauris. Praesent molestie vestibulum erat ac rhoncus. Aenean nunc risus, accumsan nec ipsum et, convallis sollicitudin dui. Proin dictum quam a semper malesuada. Etiam porta sit amet risus quis porta. Nulla facilisi. Cras at interdum ante. Ut gravida pharetra ligula vitae malesuada. Sed eget libero et arcu tempor tincidunt in ac lectus. Maecenas vitae felis enim. In in tellus consequat, condimentum eros vitae, lacinia risus. Sed vehicula sem sed risus volutpat elementum. Nunc accumsan tempus nunc ac aliquet. Integer non ullamcorper eros, in rutrum velit. Proin cursus orci sit amet lobortis iaculis. Praesent condimentum eget felis ut laoreet. Aliquam sodales dolor id mi iaculis, non fermentum leo viverra. Aenean aliquet condimentum placerat. Aenean aliquet diam arcu. Curabitur ac ligula sem. Mauris tincidunt mauris at ligula tincidunt interdum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Phasellus sagittis, eros ut iaculis varius.', 1, CAST(N'2024-05-05T07:34:54.527' AS DateTime), 3, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (18, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor.', 1, CAST(N'2024-05-05T07:34:59.380' AS DateTime), 8, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (19, 17, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit.', 1, CAST(N'2024-03-25T04:35:15.057' AS DateTime), 11, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (20, 17, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum.', 1, CAST(N'2024-03-25T07:35:15.057' AS DateTime), 7, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (21, 17, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum.', 1, CAST(N'2024-04-01T07:35:41.770' AS DateTime), 2, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (22, 21, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui.', 1, CAST(N'2024-04-02T07:35:41.770' AS DateTime), 9, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (23, 12, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed.', 1, CAST(N'2024-04-03T07:35:41.770' AS DateTime), 11, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (24, 13, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum.', 1, CAST(N'2024-04-04T07:35:47.740' AS DateTime), 3, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (25, 24, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui.', 1, CAST(N'2024-04-05T07:35:59.187' AS DateTime), 9, 2)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (26, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum.', 1, CAST(N'2024-04-09T07:56:50.897' AS DateTime), 1, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (27, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor.', 1, CAST(N'2024-04-10T07:57:04.867' AS DateTime), 8, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (28, 0, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada, ligula ipsum vulputate diam, aliquam egestas nibh ante vel dui. Sed in tellus interdum eros vulputate placerat sed non enim. Pellentesque eget justo porttitor urna dictum fermentum sit amet sed mauris. Praesent molestie vestibulum erat ac rhoncus. Aenean nunc risus, accumsan nec ipsum et, convallis sollicitudin dui. Proin dictum quam a semper malesuada. Etiam porta sit amet risus quis porta. Nulla facilisi. Cras at interdum ante. Ut gravida pharetra ligula vitae malesuada.', 1, CAST(N'2024-04-15T07:57:11.650' AS DateTime), 9, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (29, 26, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed.', 1, CAST(N'2024-04-19T07:57:19.973' AS DateTime), 3, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (30, 26, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui.', 1, CAST(N'2024-04-22T07:57:36.650' AS DateTime), 11, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (31, 30, N'Lorem ipsum dolor sit amet.', 1, CAST(N'2024-04-29T07:57:45.513' AS DateTime), 6, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (32, 31, N'Lorem ipsum dolor.', 1, CAST(N'2024-05-01T07:57:52.010' AS DateTime), 7, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (33, 30, N'Lorem ipsum dolor.', 1, CAST(N'2024-05-03T07:57:58.710' AS DateTime), 2, 1)
GO
INSERT [post].[Answers] ([AnswerId], [ParentAnswerId], [Explanation], [IsNotified], [CreatedAt], [UserId], [QuestionId]) VALUES (34, 28, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit auctor dolor. Nulla viverra, nibh quis ultrices malesuada.', 1, CAST(N'2024-05-05T07:58:25.123' AS DateTime), 3, 1)
GO
SET IDENTITY_INSERT [post].[Answers] OFF
GO
SET IDENTITY_INSERT [post].[Questions] ON 
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (1, N'Raspberry Pi, Operating System, Raspbian', N'What is the difference between Raspbian and other operating systems available for Raspberry Pi, like Ubuntu or Arch Linux?', 1, CAST(N'2024-04-05T07:01:00.000' AS DateTime), NULL, 1, N'1oPg6rqgogFDLy3NbKMNngRhaXHlh8DDu')
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (2, N'C#, Java, Spring Boot, Developer Experience', N'As a C# developer comfortable with Microsoft ecosystem, is Spring Boot worth exploring even though it uses Java? When might switching make sense, if ever?', 1, CAST(N'2024-04-04T07:54:00.000' AS DateTime), NULL, 4, N'10QNwdTNdSf1VSckCwT6VL0_7JJohPrF8')
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (3, N'Deadlocks, Operating System', N'How does the use of timeouts help in preventing or resolving deadlocks?', 1, CAST(N'2024-03-25T08:43:00.000' AS DateTime), NULL, 1, NULL)
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (4, N'Computer Networks, TCP/IP, Network Layer Protocols', N'How do the transport layer and network layer protocols, such as TCP and IP, facilitate communication between processes in a network application?', 1, CAST(N'2024-03-21T05:13:00.000' AS DateTime), NULL, 1, NULL)
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (5, N'RDBMS, Transaction, ACID, Database Management', N'What is a transaction, and why is ACID compliance important in database management?', 1, CAST(N'2024-03-10T11:46:00.000' AS DateTime), NULL, 5, N'17uG0VRB01PsSpdihChMZPlk9ZI0geMTO')
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (6, N'HTTP, FTP, SMTP, Internet Communication', N'What''s the difference between a Deterministic Finite Automaton (DFA) and a Nondeterministic Finite Automaton (NFA)?', 1, CAST(N'2024-02-27T08:27:00.000' AS DateTime), NULL, 1, NULL)
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (7, N'V-model, Testing, SDLC', N'How does the V-model integrate testing activities into each phase of the development process?', 1, CAST(N'2024-02-16T06:16:00.000' AS DateTime), NULL, 5, NULL)
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (8, N'Arduino, I2C, SPI, Embedded Systems', N'Can Arduino boards communicate with each other or with other devices, and if so, how?', 1, CAST(N'2024-02-12T05:18:00.000' AS DateTime), NULL, 1, NULL)
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (9, N'Domain Name System, DNS, Computer Networks', N'How does the Domain Name System (DNS) work, and why is it important for navigating the internet?', 1, CAST(N'2024-01-25T05:25:00.000' AS DateTime), NULL, 4, N'192iT9x42YzDf3mAI6LWEUvSBVpe06gcw')
GO
INSERT [post].[Questions] ([QuestionId], [Topic], [Explanation], [IsNotified], [CreatedAt], [UpdatedAt], [UserId], [ImageFileId]) VALUES (10, N'Hibernate, HQL, Entity Framework, LINQ, SQL', N'How do Hibernate''s HQL (Hibernate Query Language) and Entity Framework''s LINQ (Language Integrated Query) compare in terms of syntax and functionality for executing database queries?', 1, CAST(N'2024-01-17T07:01:00.000' AS DateTime), NULL, 1, N'1BirBbhwoQj6TF0mtCkGNFCNWDnxOtW85')
GO
SET IDENTITY_INSERT [post].[Questions] OFF
GO
/****** Object:  Index [IX_UserDetails_UserId] ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserDetails_UserId] ON [auth].[UserDetails]
(
	[UserId] ASC
)
WHERE ([UserId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId] ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [auth].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Email] ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email] ON [auth].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Username] ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Username] ON [auth].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_ImageFileId] ******/
CREATE NONCLUSTERED INDEX [IX_Users_ImageFileId] ON [auth].[Users]
(
	[ImageFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CloudFiles_UserId] ******/
CREATE NONCLUSTERED INDEX [IX_CloudFiles_UserId] ON [core].[CloudFiles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Answers_QuestionId] ******/
CREATE NONCLUSTERED INDEX [IX_Answers_QuestionId] ON [post].[Answers]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Answers_UserId] ******/
CREATE NONCLUSTERED INDEX [IX_Answers_UserId] ON [post].[Answers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Questions_ImageFileId] ******/
CREATE NONCLUSTERED INDEX [IX_Questions_ImageFileId] ON [post].[Questions]
(
	[ImageFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_UserId] ******/
CREATE NONCLUSTERED INDEX [IX_Questions_UserId] ON [post].[Questions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [auth].[Otps] ADD  DEFAULT (dateadd(minute,(5),getutcdate())) FOR [ExpiresAt]
GO
ALTER TABLE [auth].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsVerified]
GO
ALTER TABLE [auth].[Users] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [core].[CloudFiles] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [post].[Answers] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [ParentAnswerId]
GO
ALTER TABLE [post].[Answers] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsNotified]
GO
ALTER TABLE [post].[Answers] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [post].[Questions] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsNotified]
GO
ALTER TABLE [post].[Questions] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [auth].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [auth].[Users] ([UserId])
ON DELETE SET NULL
GO
ALTER TABLE [auth].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_Users_UserId]
GO
ALTER TABLE [auth].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [enum].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [auth].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [auth].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [auth].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [auth].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [auth].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_CloudFiles_ImageFileId] FOREIGN KEY([ImageFileId])
REFERENCES [core].[CloudFiles] ([CloudFileId])
ON DELETE SET NULL
GO
ALTER TABLE [auth].[Users] CHECK CONSTRAINT [FK_Users_CloudFiles_ImageFileId]
GO
ALTER TABLE [core].[CloudFiles]  WITH CHECK ADD  CONSTRAINT [FK_CloudFiles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [auth].[Users] ([UserId])
ON DELETE SET NULL
GO
ALTER TABLE [core].[CloudFiles] CHECK CONSTRAINT [FK_CloudFiles_Users_UserId]
GO
ALTER TABLE [post].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [post].[Questions] ([QuestionId])
ON DELETE CASCADE
GO
ALTER TABLE [post].[Answers] CHECK CONSTRAINT [FK_Answers_Questions_QuestionId]
GO
ALTER TABLE [post].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Users] FOREIGN KEY([UserId])
REFERENCES [auth].[Users] ([UserId])
ON DELETE SET NULL
GO
ALTER TABLE [post].[Answers] CHECK CONSTRAINT [FK_Answers_Users]
GO
ALTER TABLE [post].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_CloudFiles_ImageFileId] FOREIGN KEY([ImageFileId])
REFERENCES [core].[CloudFiles] ([CloudFileId])
ON DELETE SET NULL
GO
ALTER TABLE [post].[Questions] CHECK CONSTRAINT [FK_Questions_CloudFiles_ImageFileId]
GO
ALTER TABLE [post].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [auth].[Users] ([UserId])
ON DELETE SET NULL
GO
ALTER TABLE [post].[Questions] CHECK CONSTRAINT [FK_Questions_Users_UserId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bytes' , @level0type=N'SCHEMA',@level0name=N'core', @level1type=N'TABLE',@level1name=N'CloudFiles', @level2type=N'COLUMN',@level2name=N'Size'
GO
USE [master]
GO
ALTER DATABASE [TopicTalks] SET  READ_WRITE 
GO
