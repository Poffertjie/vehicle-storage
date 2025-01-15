CREATE TABLE [dbo].[CustomerVehicleDailyChecks]
(
    [Id]                  INT              NOT NULL IDENTITY (1,1),
    [CustomerVehicleId]   INT              NOT NULL,
    [CustomerStorageContractId] INT NOT NULL,
    [BatteryChargeStatus] nvarchar(200)    null,
    [TyreCheck]           nvarchar(200)    null,
    [OilCheck]            nvarchar(200)    null,
    [WaterCheck]          nvarchar(200)    null,
    [SpeedometerImage]          nvarchar(max)    null,
    [Completed]           bit              not null,
    [Date]                datetime         not null,
    [MODIFIED_BY]         UNIQUEIDENTIFIER NOT NULL,
    [ValidFrom]           datetime2 GENERATED ALWAYS AS ROW START,
    [ValidTo]             datetime2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
    CONSTRAINT [PK_CustomerVehicleDailyChecks_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerVehicleDailyChecks_CustomerVehicle] FOREIGN KEY ([CustomerVehicleId]) REFERENCES [CustomerVehicle] ([Id]),
    CONSTRAINT [FK_CustomerVehicleDailyChecks_CustomerStorageContract] FOREIGN KEY ([CustomerStorageContractId]) REFERENCES [CustomerStorageContact] ([Id]),
)
    WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[CustomerVehicleDailyChecksHistory]));