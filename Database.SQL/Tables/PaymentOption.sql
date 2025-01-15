CREATE TABLE [dbo].[PaymentOption]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(100) NOT NULL,
	[CompanyId] INT NOT NULL,
	CONSTRAINT [PK_PaymentOption_Id] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_PaymentOption_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
)
