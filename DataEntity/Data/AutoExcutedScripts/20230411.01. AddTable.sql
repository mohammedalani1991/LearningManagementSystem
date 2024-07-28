
CREATE TABLE [dbo].[SuperAdmin](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Show] [bit] NOT NULL,
    [Name] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_SuperAdmin] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


INSERT [dbo].[SuperAdmin] ( [Name], [Show]) VALUES ( N'PracticalExams', 1)
INSERT [dbo].[SuperAdmin] ( [Name], [Show]) VALUES ( N'Quizzes', 1)
INSERT [dbo].[SuperAdmin] ( [Name], [Show]) VALUES ( N'Packages', 1)
