USE [DB_Prog1]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Question] [int] NOT NULL,
	[Correctness] [bit] NULL,
	[Contents] [varchar](max) NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups_Tests]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups_Tests](
	[ID_Group] [int] NOT NULL,
	[ID_Tests] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Groups_Tests] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Test] [int] NOT NULL,
	[Contents] [varchar](max) NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Results]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Test] [int] NULL,
	[Per_Complete] [float] NULL,
	[ID_User] [int] NOT NULL,
 CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Testing]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Testing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Result] [int] NOT NULL,
	[ID_Question] [int] NOT NULL,
 CONSTRAINT [PK_Testing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[LeadTime] [datetime] NULL,
	[NumAttempts] [int] NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[ID_Type] [int] NOT NULL,
	[Full_Name] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users_Groups]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users_Groups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_User] [int] NOT NULL,
	[ID_Group] [int] NOT NULL,
 CONSTRAINT [PK_Users_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users_Types]    Script Date: 14.12.2024 17:53:42 Иван ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Users_Types] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Answers] ON 

INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (1, 1, 1, N'менеджерскую')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (2, 1, 0, N'Коммуникативную')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (3, 1, 0, N'профессиональную')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (4, 1, 0, N'квалификационную')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (5, 2, 1, N'общение')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (6, 2, 0, N'восприятие')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (7, 2, 0, N'взаимодействие')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (8, 2, 0, N'идентификация')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (9, 3, 1, N'k1')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (10, 3, 0, N'k2')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (11, 3, 0, N'k3')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (12, 3, 0, N'k4')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (13, 4, 0, N'd1')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (14, 4, 1, N'd2')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (15, 4, 0, N'd3')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (16, 4, 0, N'd4')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (17, 5, 0, N's1')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (18, 5, 0, N's2')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (19, 5, 1, N's3')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (20, 5, 0, N's4')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (38, 10, 1, N'yes')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (39, 10, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (40, 10, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (41, 10, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (42, 11, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (43, 11, 1, N'yes')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (44, 11, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (45, 11, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (46, 12, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (47, 12, 1, N'yes')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (48, 12, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (49, 12, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (50, 13, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (51, 13, 1, N'yes')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (52, 13, 0, N'no')
INSERT [dbo].[Answers] ([ID], [ID_Question], [Correctness], [Contents]) VALUES (53, 13, 0, N'no')
SET IDENTITY_INSERT [dbo].[Answers] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([ID], [Name]) VALUES (2, N'1А')
INSERT [dbo].[Groups] ([ID], [Name]) VALUES (3, N'1b')
INSERT [dbo].[Groups] ([ID], [Name]) VALUES (4, N'1В')
INSERT [dbo].[Groups] ([ID], [Name]) VALUES (6, N'1C')
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups_Tests] ON 

INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (2, 1, 1)
INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (2, 2, 2)
INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (2, 3, 3)
INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (3, 1, 4)
INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (3, 2, 5)
INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (3, 3, 6)
INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (2, 8, 1006)
INSERT [dbo].[Groups_Tests] ([ID_Group], [ID_Tests], [ID]) VALUES (2, 9, 1007)
SET IDENTITY_INSERT [dbo].[Groups_Tests] OFF
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 

INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (1, 1, N'К видам компетентности не относят:')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (2, 1, N'Процесс установления и развития контактов среди людей – это:')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (3, 2, N'q1')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (4, 3, N'?')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (5, 3, N'?2')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (10, 8, N'1vop')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (11, 8, N'2vop')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (12, 9, N'vop1')
INSERT [dbo].[Questions] ([ID], [ID_Test], [Contents]) VALUES (13, 9, N'vop2')
SET IDENTITY_INSERT [dbo].[Questions] OFF
GO
SET IDENTITY_INSERT [dbo].[Results] ON 

INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (4, 1, 0, 4)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (5, 2, 0, 4)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (6, 3, 0, 4)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (8, 1, 1, 4)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (9, 1, 0, 4)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (10, 2, 1, 4)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (11, 3, 0, 4)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (12, 1, 0.5, 3)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (13, 9, 1, 3)
INSERT [dbo].[Results] ([ID], [ID_Test], [Per_Complete], [ID_User]) VALUES (14, 8, 1, 3)
SET IDENTITY_INSERT [dbo].[Results] OFF
GO
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([ID], [Name], [LeadTime], [NumAttempts]) VALUES (1, N'Тест по психологии общения', NULL, NULL)
INSERT [dbo].[Tests] ([ID], [Name], [LeadTime], [NumAttempts]) VALUES (2, N't1', NULL, NULL)
INSERT [dbo].[Tests] ([ID], [Name], [LeadTime], [NumAttempts]) VALUES (3, N'bff', NULL, NULL)
INSERT [dbo].[Tests] ([ID], [Name], [LeadTime], [NumAttempts]) VALUES (8, N'Test for test', NULL, NULL)
INSERT [dbo].[Tests] ([ID], [Name], [LeadTime], [NumAttempts]) VALUES (9, N'Тест для проверки', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Tests] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (1, N'admin', N'111', 1, N'Иванов И. А.')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (2, N'teacher1', N'222', 2, N'Ковальчук И. Д.')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (3, N'student1', N'333', 3, N'Покрас Н. А.')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (4, N'student2', N'444', 3, N'Кондрашов Г. А.')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (5, N'teacher2', N'555', 2, N'Юсупова М. Д. ')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (6, N'student3', N'676', 3, N'Вася Пупкин')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (7, N'student4', N'767', 3, N'Денис Гоцман')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (8, N'teacherx', N'666', 2, N'Кашапова А. С.')
INSERT [dbo].[Users] ([ID], [Login], [Password], [ID_Type], [Full_Name]) VALUES (9, N'studentx', N'343', 3, N'Панюшкин А. А.')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Users_Groups] ON 

INSERT [dbo].[Users_Groups] ([ID], [ID_User], [ID_Group]) VALUES (2, 2, 2)
INSERT [dbo].[Users_Groups] ([ID], [ID_User], [ID_Group]) VALUES (3, 3, 2)
INSERT [dbo].[Users_Groups] ([ID], [ID_User], [ID_Group]) VALUES (4, 4, 2)
INSERT [dbo].[Users_Groups] ([ID], [ID_User], [ID_Group]) VALUES (5, 6, 3)
INSERT [dbo].[Users_Groups] ([ID], [ID_User], [ID_Group]) VALUES (6, 7, 3)
INSERT [dbo].[Users_Groups] ([ID], [ID_User], [ID_Group]) VALUES (8, 8, 3)
INSERT [dbo].[Users_Groups] ([ID], [ID_User], [ID_Group]) VALUES (9, 8, 3)
SET IDENTITY_INSERT [dbo].[Users_Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Users_Types] ON 

INSERT [dbo].[Users_Types] ([ID], [Name]) VALUES (1, N'Админ')
INSERT [dbo].[Users_Types] ([ID], [Name]) VALUES (2, N'Учитель')
INSERT [dbo].[Users_Types] ([ID], [Name]) VALUES (3, N'Ученик')
SET IDENTITY_INSERT [dbo].[Users_Types] OFF
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Questions] FOREIGN KEY([ID_Question])
REFERENCES [dbo].[Questions] ([ID])
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Questions]
GO
ALTER TABLE [dbo].[Groups_Tests]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Tests_Groups] FOREIGN KEY([ID_Group])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[Groups_Tests] CHECK CONSTRAINT [FK_Groups_Tests_Groups]
GO
ALTER TABLE [dbo].[Groups_Tests]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Tests_Tests] FOREIGN KEY([ID_Tests])
REFERENCES [dbo].[Tests] ([ID])
GO
ALTER TABLE [dbo].[Groups_Tests] CHECK CONSTRAINT [FK_Groups_Tests_Tests]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Tests] FOREIGN KEY([ID_Test])
REFERENCES [dbo].[Tests] ([ID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Tests]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Tests] FOREIGN KEY([ID_Test])
REFERENCES [dbo].[Tests] ([ID])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Tests]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Users] FOREIGN KEY([ID_User])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Users]
GO
ALTER TABLE [dbo].[Testing]  WITH CHECK ADD  CONSTRAINT [FK_Testing_Questions] FOREIGN KEY([ID_Question])
REFERENCES [dbo].[Questions] ([ID])
GO
ALTER TABLE [dbo].[Testing] CHECK CONSTRAINT [FK_Testing_Questions]
GO
ALTER TABLE [dbo].[Testing]  WITH CHECK ADD  CONSTRAINT [FK_Testing_Results] FOREIGN KEY([ID_Result])
REFERENCES [dbo].[Results] ([ID])
GO
ALTER TABLE [dbo].[Testing] CHECK CONSTRAINT [FK_Testing_Results]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users_Types] FOREIGN KEY([ID_Type])
REFERENCES [dbo].[Users_Types] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users_Types]
GO
ALTER TABLE [dbo].[Users_Groups]  WITH CHECK ADD  CONSTRAINT [FK_Users_Groups_Groups] FOREIGN KEY([ID_Group])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[Users_Groups] CHECK CONSTRAINT [FK_Users_Groups_Groups]
GO
ALTER TABLE [dbo].[Users_Groups]  WITH CHECK ADD  CONSTRAINT [FK_Users_Groups_Users] FOREIGN KEY([ID_User])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Users_Groups] CHECK CONSTRAINT [FK_Users_Groups_Users]
GO
