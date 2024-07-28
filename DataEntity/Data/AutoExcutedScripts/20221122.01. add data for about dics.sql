delete from [dbo].[AboutDicTranslation]

delete from [dbo].[AboutDic]

SET IDENTITY_INSERT [dbo].[AboutDic] ON 

INSERT [dbo].[AboutDic] ([Id], [GroupName], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [SortOrder]) VALUES (1, N'HomePageContent', CAST(N'2021-08-09T11:14:51.627' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Al-Safa Academy Traning Center', N'<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry Lorem Ipsum is simply dummy text of the printing and typesetting industry Lorem Ipsum is simply dummy text of the printing and typesetting industry Lorem Ipsum is simply dummy text of the printing and typesetting industry Lorem Ipsum is simply dummy text of the printing and typesetting industry</p>', NULL)
INSERT [dbo].[AboutDic] ([Id], [GroupName], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [SortOrder]) VALUES (2, N'AboutUs', CAST(N'2022-11-22T16:03:23.137' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'About us', N'', NULL)
INSERT [dbo].[AboutDic] ([Id], [GroupName], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [SortOrder]) VALUES (3, N'TermsAndConditions', CAST(N'2022-11-22T17:42:04.613' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Terms And Conditions', N'<p>TermsAndConditions</p>', NULL)
INSERT [dbo].[AboutDic] ([Id], [GroupName], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [SortOrder]) VALUES (4, N'PrivacyPolicy', CAST(N'2022-11-22T17:42:23.560' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Privacy Policy', N'<p>PrivacyPolicy</p>', NULL)
INSERT [dbo].[AboutDic] ([Id], [GroupName], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [Name], [Value], [SortOrder]) VALUES (5, N'FooterContent', CAST(N'2022-11-22T17:42:40.703' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, N'Footer Content', N'<p>Footer Content</p>', NULL)
SET IDENTITY_INSERT [dbo].[AboutDic] OFF

SET IDENTITY_INSERT [dbo].[AboutDicTranslation] ON 

INSERT [dbo].[AboutDicTranslation] ([Id], [AboutDicId], [LanguageId], [IsDefault], [Name], [Value]) VALUES (1, 2, 7, 0, N'من نحن', N'')
INSERT [dbo].[AboutDicTranslation] ([Id], [AboutDicId], [LanguageId], [IsDefault], [Name], [Value]) VALUES (3, 1, 7, 0, N'مركز تدريب أكاديمية الصفا', N'<p style="direction: rtl;">لوريم إيبسوم هو ببساطة نص شكلي في صناعة الطباعة والتنضيد لوريم إيبسوم هو ببساطة نص وهمي لصناعة الطباعة والتنضيد. لوريم إيبسوم هو ببساطة نص شكلي لصناعة الطباعة والتنضيد. هو مجرد نص وهمي لصناعة الطباعة والتنضيد</p>')
SET IDENTITY_INSERT [dbo].[AboutDicTranslation] OFF

