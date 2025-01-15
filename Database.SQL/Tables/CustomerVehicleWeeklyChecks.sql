CREATE TABLE [dbo].[CustomerVehicleWeeklyChecks]
(
    [Id]                     INT              NOT NULL IDENTITY (1,1),
    [CustomerVehicleId]      INT              NOT NULL,
    [CustomerStorageContractId] INT NOT NULL,
    [BatteryChargeStatus]    nvarchar(max)    null,
    [BatteryStatus]          nvarchar(max)    null,
    [TirePressureFrontRight] nvarchar(max)    null,
    [TirePressureRearRight]  nvarchar(max)    null,
    [TirePressureRearLeft]   nvarchar(max)    null,
    [TirePressureFrontLeft]  nvarchar(max)    null,
    [Completed]              bit              not null,
    [Date]                   datetime         not null,
    [MODIFIED_BY]            UNIQUEIDENTIFIER NOT NULL,
    [ValidFrom]              datetime2 GENERATED ALWAYS AS ROW START,
    [ValidTo]                datetime2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
    CONSTRAINT [PK_CustomerVehicleWeeklyChecks_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerVehicleWeeklyChecks_CustomerVehicle] FOREIGN KEY ([CustomerVehicleId]) REFERENCES [CustomerVehicle] ([Id]),
    CONSTRAINT [FK_CustomerVehicleWeeklyChecks_CustomerStorageContract] FOREIGN KEY ([CustomerStorageContractId]) REFERENCES [CustomerStorageContact] ([Id]),
)
    WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[CustomerVehicleWeeklyChecksHistory]));