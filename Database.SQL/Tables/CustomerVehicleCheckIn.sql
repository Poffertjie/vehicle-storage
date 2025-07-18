CREATE TABLE [dbo].[CustomerVehicleCheckIn]
(
    [Id] INT NOT NULL IDENTITY(1,1),
    [CustomerVehicleId] INT NOT NULL,
    [CustomerStorageContractId] INT NOT NULL,
    [Detailing] NVARCHAR(MAX) NOT NULL,
    [TyreMake] NVARCHAR(100) NOT NULL,
    [TyreYearModel] INT NOT NULL,
    [TyreTreadMeasure] decimal not null,
    [DeepWash] bit not null,
    [DiagnosticReportFile] nvarchar(max),
    [NumberPlateImage] nvarchar(max) not null,
    [ChassisNumberImage] nvarchar(max) not null,
    [FrontImage] nvarchar(max) not null,
    [LeftFrontImage] nvarchar(max) not null,
    [LeftFenderWheelImage] nvarchar(max) not null,
    [LeftDoorsImage] nvarchar(max) not null,
    [LeftRearFenderWheelImage] nvarchar(max) not null,
    [RearImage] nvarchar(max) not null,
    [RightFrontImage] nvarchar(max) not null,
    [RightFenderWheelImage] nvarchar(max) not null,
    [RightDoorsImage] nvarchar(max) not null,
    [RightRearFenderWheelImage] nvarchar(max) not null,
    [MODIFIED_BY] UNIQUEIDENTIFIER NOT NULL,
    [TimeStamp] DATETIME2 NOT NULL,
    [ValidFrom] datetime2 GENERATED ALWAYS AS ROW START,
    [ValidTo] datetime2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
    CONSTRAINT [PK_CustomerVehicleCheckIn_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerVehicleCheckIn_CustomerVehicleId] FOREIGN KEY ([CustomerVehicleId]) REFERENCES [CustomerVehicle]([Id]),
    CONSTRAINT [FK_CustomerVehicleCheckIn_CustomerStorageContract] FOREIGN KEY ([CustomerStorageContractId]) REFERENCES [CustomerStorageContact] ([Id]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[CustomerVehicleCheckInHistory]));