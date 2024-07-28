  delete from SenangPay
  
  alter table SenangPay add [CreatedOn] [datetime] NOT NULL
  alter table SenangPay add [CreatedBy] [nvarchar](256) NOT NULL
  alter table SenangPay add [DeletedOn] [datetime] NULL

