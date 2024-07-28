
CREATE TABLE [dbo].[SuperAdminSettings](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [NameEnglish] [nvarchar](512) NULL,
    [NameArabic] [nvarchar](512) NULL,
    [ImageUrl] [nvarchar](512) NULL,
 CONSTRAINT [PK_SuperAdminSettings] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


INSERT [dbo].[SuperAdminSettings] ( [NameEnglish], [NameArabic] ,[ImageUrl]) VALUES ( N'Al-Safa Academy', N'أكاديمية الصفا' ,N'')
