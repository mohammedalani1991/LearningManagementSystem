CREATE TABLE [dbo].[SenangPay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationUserId] [nvarchar](450) NULL,
	[UserName] [nvarchar](450) NULL,
	[Country] [nvarchar](450) NULL,
	[Email] [nvarchar](450) NULL,
	[ProcessDate] [datetime2](7) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Details] [nvarchar](450) NULL,
	[Status] int NOT NULL,
	[TransactionId] [nvarchar](max) NULL,
	[Msg] [nvarchar](max) NULL,
	[SenangPayPaymentType] [int] NOT NULL,
 CONSTRAINT [PK_SenangPay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[SenangPay]  WITH CHECK ADD  CONSTRAINT [FK_SenangPay_AspNetUsers_ApplicationUserId] FOREIGN KEY([ApplicationUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])


ALTER TABLE [dbo].[SenangPay] CHECK CONSTRAINT [FK_SenangPay_AspNetUsers_ApplicationUserId]

