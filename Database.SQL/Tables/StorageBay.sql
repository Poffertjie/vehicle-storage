CREATE TABLE [dbo].[StorageBay]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Number] INT NOT NULL,
	[StorageBayStatusId] INT NOT NULL,
	[CompanyId] INT NOT NULL,
	[MODIFIED_BY] UNIQUEIDENTIFIER NOT NULL,
	[ValidFrom] datetime2 GENERATED ALWAYS AS ROW START,
	[ValidTo] datetime2 GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
	CONSTRAINT [PK_StorageBay_Id] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_StorageBay_StorageBayStatus] FOREIGN KEY ([StorageBayStatusId]) REFERENCES [StorageBayStatus]([Id]), 
    CONSTRAINT [FK_StorageBay_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]) 
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[StorageBayHistory]));
