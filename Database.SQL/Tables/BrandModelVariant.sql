CREATE TABLE [dbo].[BrandModelVariant]
(
	[Id] INT NOT NULL  IDENTITY (1,1),
	[CompanyId] INT NOT NULL,
	[BrandModelId] INT  NOT NULL,
	[Name] NVARCHAR (20) NOT NULL,
	[MODIFIED_BY] UNIQUEIDENTIFIER NOT NULL, 
	[ValidFrom] datetime2 GENERATED ALWAYS AS ROW START,
	[ValidTo] datetime2 GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
    CONSTRAINT [PK_BrandModelVariant] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_BrandModelVariant_BrandModel] FOREIGN KEY ([BrandModelId]) REFERENCES [BrandModel]([Id]),
	CONSTRAINT [FK_BrandModelVariant_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[BrandModelVariantHistory]))