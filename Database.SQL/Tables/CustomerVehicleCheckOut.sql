CREATE TABLE [dbo].[CustomerVehicleCheckOut]
(
    [Id]                        INT              NOT NULL IDENTITY (1,1),
    [CustomerVehicleId]         INT              NOT NULL,
    [CustomerStorageContractId] INT              NOT NULL,
    [ReturnAllBelongings]       bit              NOT NULL,
    [ReturnNumberPlates]        bit              NOT NULL,
    [CheckOutDate]              datetime         NOT NULL,
    [PlannedCheckInDate]        datetime         NOT NULL,
    [CheckInDate]               datetime         NULL,
    [DeliveryMethodId]          int              NOT NULL,
    [ErrorCodeImage]            nvarchar(max)    null,
    [Address]                   nvarchar(450)    null,
    [TimeStamp] DATETIME2 NOT NULL,
    [MODIFIED_BY]               UNIQUEIDENTIFIER NOT NULL,
    [ValidFrom]                 datetime2 GENERATED ALWAYS AS ROW START,
    [ValidTo]                   datetime2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
    CONSTRAINT [PK_CustomerVehicleCheckOut_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerVehicleCheckOut_CustomerVehicle] FOREIGN KEY ([CustomerVehicleId]) REFERENCES [CustomerVehicle] ([Id]),
    CONSTRAINT [FK_CustomerVehicleCheckOut_DeliveryMethod] FOREIGN KEY ([DeliveryMethodId]) REFERENCES [DeliveryMethod] ([Id]),
    CONSTRAINT [FK_CustomerVehicleCheckout_CustomerStorageContract] FOREIGN KEY ([CustomerStorageContractId]) REFERENCES [CustomerStorageContact] ([Id]),
)
    WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[CustomerVehicleCheckOutHistory]));