CREATE TABLE [dbo].[CustomerVehicleStatus]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[CompanyId] INT NOT NULL,
	[Status] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_CustomerVehicleStatus_Id] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_CustomerVehicleStatus_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
)
