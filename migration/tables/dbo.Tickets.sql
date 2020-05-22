CREATE TABLE [dbo].[Tickets] (
    [ticket_id]                      INT           IDENTITY (1, 1) NOT NULL,
    [customer_full_name]             VARCHAR (50)  NOT NULL,
    [customer_email]                 VARCHAR (50)  NOT NULL,
    [customer_phone_number]          VARCHAR (50)  NOT NULL,
    [ticket_product_image_path]      VARCHAR (100) NULL,
    [ticket_estimated_delivery_date] DATETIME      NOT NULL,
    [ticket_estimated_cost]          INT           NOT NULL,
    [ticket_description]             VARCHAR (200) NULL,
    [ticket_product_model_id]        INT           NOT NULL,
    [ticket_status]                  INT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ticket_id] ASC),
    FOREIGN KEY ([ticket_product_model_id]) REFERENCES [dbo].[ProductModels] ([product_model_id]),
	FOREIGN KEY ([ticket_status]) REFERENCES [dbo].[TicketStatuses] ([status_id])
);

