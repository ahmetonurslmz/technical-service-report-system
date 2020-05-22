CREATE TABLE [dbo].[TicketStatuses] (
    [status_id]   INT          IDENTITY (1, 1) NOT NULL,
    [status_name] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([status_id] ASC)
);

