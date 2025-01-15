CREATE TABLE [dbo].[User]
(
	[Id] NVARCHAR (450) NOT NULL,
	[FullName] NVARCHAR(100) NOT NULL,
	[Email] NVARCHAR (100) NOT NULL,
	[EmailConfirmed] BIT DEFAULT ((0)) NOT NULL,
	[ResetPasswordToken] NVARCHAR(450) NULL,
	[PasswordHash] NVARCHAR(MAX) NULL,
	[AccountLocked] BIT DEFAULT((0)) NOT NULL,
	[SecurityStamp] VARCHAR(40)  NOT NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [SystemAdmin] BIT NOT NULL DEFAULT 0,
	[CompanyId] INT NULL,
	[ValidFrom] datetime2 GENERATED ALWAYS AS ROW START,
	[ValidTo] datetime2 GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo),
	CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_User_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]) 
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[UserHistory]));
