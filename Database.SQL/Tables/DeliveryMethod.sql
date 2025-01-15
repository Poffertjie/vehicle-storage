CREATE TABLE [dbo].[DeliveryMethod]
(
    [Id]   int          NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(20) NOT NULL,
    CONSTRAINT [PK_DeliveryMethod_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
)
