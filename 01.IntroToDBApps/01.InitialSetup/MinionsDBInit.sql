USE [MinionsDB]
GO
/****** Object:  Table [dbo].[Minions]    Script Date: 27.02.2017 г. 21:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Minions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[Age] [int] NULL,
	[TownId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Towns]    Script Date: 27.02.2017 г. 21:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Towns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TownName] [varchar](35) NULL,
	[CountryName] [varchar](35) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Villians]    Script Date: 27.02.2017 г. 21:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Villians](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[EvilnessFactor] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VilliansMinions]    Script Date: 27.02.2017 г. 21:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VilliansMinions](
	[VillianId] [int] NOT NULL,
	[MinionId] [int] NOT NULL,
 CONSTRAINT [PK_VilliansMinions] PRIMARY KEY CLUSTERED 
(
	[VillianId] ASC,
	[MinionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Minions] ON 

INSERT [dbo].[Minions] ([Id], [Name], [Age], [TownId]) VALUES (1, N'Kevin', 11, 1)
INSERT [dbo].[Minions] ([Id], [Name], [Age], [TownId]) VALUES (2, N'Bob', 12, 2)
INSERT [dbo].[Minions] ([Id], [Name], [Age], [TownId]) VALUES (3, N'Stewart', 5, 3)
INSERT [dbo].[Minions] ([Id], [Name], [Age], [TownId]) VALUES (4, N'Steve', 3, 5)
INSERT [dbo].[Minions] ([Id], [Name], [Age], [TownId]) VALUES (5, N'Charles', 1, 4)
SET IDENTITY_INSERT [dbo].[Minions] OFF
SET IDENTITY_INSERT [dbo].[Towns] ON 

INSERT [dbo].[Towns] ([Id], [TownName], [CountryName]) VALUES (1, N'Sofia', N'Bulgaria')
INSERT [dbo].[Towns] ([Id], [TownName], [CountryName]) VALUES (2, N'Plovdiv', N'Bulgaria')
INSERT [dbo].[Towns] ([Id], [TownName], [CountryName]) VALUES (3, N'Berlin', N'Germany')
INSERT [dbo].[Towns] ([Id], [TownName], [CountryName]) VALUES (4, N'Paris', N'France')
INSERT [dbo].[Towns] ([Id], [TownName], [CountryName]) VALUES (5, N'Liverpool', N'England')
SET IDENTITY_INSERT [dbo].[Towns] OFF
SET IDENTITY_INSERT [dbo].[Villians] ON 

INSERT [dbo].[Villians] ([Id], [Name], [EvilnessFactor]) VALUES (1, N'Gosho', N'bad')
INSERT [dbo].[Villians] ([Id], [Name], [EvilnessFactor]) VALUES (2, N'Tosho', N'good')
INSERT [dbo].[Villians] ([Id], [Name], [EvilnessFactor]) VALUES (3, N'Misho', N'evil')
INSERT [dbo].[Villians] ([Id], [Name], [EvilnessFactor]) VALUES (4, N'Gogo', N'super evil')
INSERT [dbo].[Villians] ([Id], [Name], [EvilnessFactor]) VALUES (5, N'Tisho', N'bad')
SET IDENTITY_INSERT [dbo].[Villians] OFF
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (1, 1)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (1, 2)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (1, 3)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (1, 4)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (1, 5)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (2, 2)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (3, 1)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (3, 2)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (3, 3)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (3, 4)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (3, 5)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (4, 4)
INSERT [dbo].[VilliansMinions] ([VillianId], [MinionId]) VALUES (5, 5)
ALTER TABLE [dbo].[Minions]  WITH CHECK ADD FOREIGN KEY([TownId])
REFERENCES [dbo].[Towns] ([Id])
GO
ALTER TABLE [dbo].[VilliansMinions]  WITH CHECK ADD FOREIGN KEY([MinionId])
REFERENCES [dbo].[Minions] ([Id])
GO
ALTER TABLE [dbo].[VilliansMinions]  WITH CHECK ADD FOREIGN KEY([VillianId])
REFERENCES [dbo].[Villians] ([Id])
GO
ALTER TABLE [dbo].[Villians]  WITH CHECK ADD CHECK  (([EvilnessFactor]='super evil' OR [EvilnessFactor]='bad' OR [EvilnessFactor]='evil' OR [EvilnessFactor]='good'))
GO
