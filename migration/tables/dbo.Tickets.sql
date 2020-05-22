CREATE TABLE [dbo].[Tickets] (
    [ticket_id]                      INT         IDENTITY (1, 1) NOT NULL,
    [customer_full_name]             NCHAR (50)  NOT NULL,
    [customer_email]                 NCHAR (50)  NOT NULL,
    [customer_phone_number]          NCHAR (50)  NOT NULL,
    [ticket_product_image_path]      NCHAR (100) NULL,
    [ticket_estimated_delivery_date] DATETIME    NOT NULL,
    [ticket_estimated_cost]          INT         NOT NULL,
    [ticket_description]             NCHAR (200) NULL,
    [ticket_product_model_id]        INT         NOT NULL,
	[ticket_status] INT NOT NULL DEFAULT 0,
    PRIMARY KEY CLUSTERED ([ticket_id] ASC),
    FOREIGN KEY ([ticket_product_model_id]) REFERENCES [dbo].[ProductModels] ([product_model_id])
);

