CREATE TABLE [dbo].[AdditionalContactPerson]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[CompanyId] INT NOT NULL,
	[CustomerId] INT NOT NULL,
	[FullName] NVARCHAR(50) NOT NULL,
	[IdentificationNumber] NVARCHAR(30) NOT NULL,
	[ContactNumber] NVARCHAR(50) NOT NULL,
	[Relationship] NVARCHAR(50) NOT NULL,
	[MODIFIED_BY] UNIQUEIDENTIFIER NOT NULL,
	[ValidFrom] datetime2 GENERATED ALWAYS AS ROW START,
	[ValidTo] datetime2 GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
	CONSTRAINT [PK_AdditionalContactPerson_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdditionalContactPerson_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id]),
	CONSTRAINT [FK_AdditionalContactPerson_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[AdditionalContactPersonHistory]));