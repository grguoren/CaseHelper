USE [CaseHelper]
GO
/****** Object:  Table [dbo].[tb_UserInfo]    Script Date: 12/25/2017 10:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Pwd] [varchar](50) NULL,
	[NickName] [varchar](20) NULL,
	[UpdateTime] [datetime] NULL,
	[CreateTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ImgPath] [varchar](150) NULL,
	[TeamMember] [varchar](500) NULL,
	[TeamOwer] [varchar](500) NULL,
	[TeamAim] [varchar](500) NULL,
	[Telephone] [varchar](20) NULL,
 CONSTRAINT [PK_tb_UserInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tb_UserInfo] ON
INSERT [dbo].[tb_UserInfo] ([ID], [UserName], [Pwd], [NickName], [UpdateTime], [CreateTime], [Status], [ImgPath], [TeamMember], [TeamOwer], [TeamAim], [Telephone]) VALUES (1, N'test', N'9A819E0927F15263', N'测试人员', NULL, CAST(0x0000A81E00000000 AS DateTime), 1, N'', N'', N'', N'', N'0731-85798384')
INSERT [dbo].[tb_UserInfo] ([ID], [UserName], [Pwd], [NickName], [UpdateTime], [CreateTime], [Status], [ImgPath], [TeamMember], [TeamOwer], [TeamAim], [Telephone]) VALUES (3, N'admin', N'123456', N'最高权限', NULL, CAST(0x0000A81E00000000 AS DateTime), 1, N'', N'', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[tb_UserInfo] OFF
/****** Object:  Table [dbo].[tb_CaseQuestBank]    Script Date: 12/25/2017 10:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CaseQuestBank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaperId] [int] NOT NULL,
	[TypeId] [int] NULL,
	[Problem] [varchar](500) NULL,
	[ItemList] [varchar](2000) NULL,
	[AddId] [int] NULL,
	[AddName] [varchar](50) NULL,
	[AddTime] [datetime] NULL,
 CONSTRAINT [PK_tb_CaseQuestBank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_CasePaper]    Script Date: 12/25/2017 10:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CasePaper](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [varchar](150) NULL,
	[AddTime] [datetime] NULL,
	[Remark] [varchar](450) NULL,
	[Status] [int] NULL,
	[CaseNo] [int] NOT NULL,
 CONSTRAINT [PK_tb_CasePaper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tb_CasePaper] ON
INSERT [dbo].[tb_CasePaper] ([Id], [UserId], [Title], [AddTime], [Remark], [Status], [CaseNo]) VALUES (1, 3, N'dfgdsfg', CAST(0x0000A82100BB8089 AS DateTime), N'sgsdfg', 0, 0)
SET IDENTITY_INSERT [dbo].[tb_CasePaper] OFF
/****** Object:  Table [dbo].[tb_CaseNo]    Script Date: 12/25/2017 10:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CaseNo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[No] [varchar](50) NULL,
 CONSTRAINT [PK_tb_CaseNo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_CaseAnswer]    Script Date: 12/25/2017 10:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CaseAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pid] [varchar](50) NULL,
	[Qid] [int] NULL,
	[AnswerList] [varchar](2000) NULL,
	[AddTime] [datetime] NULL,
	[AnswerNo] [int] NULL,
	[Status] [int] NULL,
	[ExportDate] [datetime] NULL,
 CONSTRAINT [PK_tb_CaseType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回答表Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_CaseAnswer', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问卷ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_CaseAnswer', @level2type=N'COLUMN',@level2name=N'Pid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_CaseAnswer', @level2type=N'COLUMN',@level2name=N'Qid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回答项' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_CaseAnswer', @level2type=N'COLUMN',@level2name=N'AnswerList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回答时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_CaseAnswer', @level2type=N'COLUMN',@level2name=N'AddTime'
GO
/****** Object:  Table [dbo].[tb_CaseAnsExport]    Script Date: 12/25/2017 10:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CaseAnsExport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NULL,
	[IdList] [varchar](800) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AddTime] [datetime] NULL,
 CONSTRAINT [PK_tb_CaseAnsExport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
