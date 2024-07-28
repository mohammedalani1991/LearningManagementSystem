CREATE TABLE [dbo].[InvoicesPay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactID] [int] NOT NULL,
	[EnrollTeacherCourseId] [int] NOT NULL,
	[ProcessStatus] [int] NOT NULL,
	[ProcessDate] [datetime] NULL,
	[ReceiptNo] [nvarchar](500) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[AttachmentUrl] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_InvoicesPay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[InvoicesPay]  WITH CHECK ADD  CONSTRAINT [FK_InvoicesPay_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([Id])


ALTER TABLE [dbo].[InvoicesPay] CHECK CONSTRAINT [FK_InvoicesPay_Contact]


ALTER TABLE [dbo].[InvoicesPay]  WITH CHECK ADD  CONSTRAINT [FK_InvoicesPay_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])


ALTER TABLE [dbo].[InvoicesPay] CHECK CONSTRAINT [FK_InvoicesPay_EnrollTeacherCourse]

