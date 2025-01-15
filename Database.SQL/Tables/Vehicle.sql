CREATE TABLE [dbo].[Vehicle]
(
	[Id] INT NOT NULL  IDENTITY (1,1),
	[CompanyId] INT NOT NULL,
    [ShortDescription] NVARCHAR(200) NOT NULL,
	[BrandId] INT NOT NULL,
	[BrandModelId] INT NOT NULL,
	[BrandModelVariantId] INT NULL,
	[FuelTypeId] INT NOT NULL,
	[MODIFIED_BY] UNIQUEIDENTIFIER NOT NULL, 
	[ValidFrom] datetime2 GENERATED ALWAYS AS ROW START,
	[ValidTo] datetime2 GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
	CONSTRAINT [PK_Vehicle_Id] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Vehicle_Brands] FOREIGN KEY ([BrandId]) REFERENCES [Brand]([Id]), 
    CONSTRAINT [FK_Vehicle_BrandModel] FOREIGN KEY ([BrandModelId]) REFERENCES [BrandModel]([Id]), 
    CONSTRAINT [FK_Vehicle_BrandModelVariant] FOREIGN KEY ([BrandModelVariantId]) REFERENCES [BrandModelVariant]([Id]), 
    CONSTRAINT [FK_Vehicle_FuelType] FOREIGN KEY ([FuelTypeId]) REFERENCES [FuelType]([Id]),
	CONSTRAINT [FK_Vehicle_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE =[dbo].[VehicleHistory]));