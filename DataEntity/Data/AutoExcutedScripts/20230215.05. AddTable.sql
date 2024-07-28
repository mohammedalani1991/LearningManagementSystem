ALTER TABLE  [dbo].[SenangPay] add  [CurrencyRate]  [decimal](18, 8)   NULL;
ALTER TABLE  [dbo].[SenangPay] add  [CustomerCurrencyCode]  nvarchar(250)   NULL;

ALTER TABLE  [dbo].[InvoicesPay] add  [CurrencyRate]  [decimal](18, 8)   NULL;
ALTER TABLE  [dbo].[InvoicesPay] add  [CustomerCurrencyCode]  nvarchar(250)   NULL;