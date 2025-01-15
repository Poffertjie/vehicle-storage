CREATE TABLE [dbo].[StorageBayStatus]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[CompanyId] INT NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_StorageBayStatus_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StorageBayStatus_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 

)
