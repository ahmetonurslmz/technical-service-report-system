CREATE TABLE [dbo].[Tickets]
(
	[ticket_id]        INT        IDENTITY (1, 1) NOT NULL,
    [customer_full_name] NCHAR (50) NOT NULL,
	[customer_email] NCHAR (50) NOT NULL,
	[customer_phone_number] NCHAR (50) NOT NULL,
	[ticket_product_image_path] NCHAR (100),
    [ticket_estimated_delivery_date]      DATETIME        NOT NULL,
    [ticket_estimated_cost]  INT    NOT NULL,
	[ticket_description] NCHAR (200),
	[ticket_product_model_id]  INT    NOT NULL,
	[ticket_product_brand_id]  INT    NOT NULL,
    PRIMARY KEY CLUSTERED ([ticket_id] ASC),
    FOREIGN KEY ([ticket_product_model_id]) REFERENCES [dbo].[ProductModels] ([product_model_id]),
    FOREIGN KEY ([ticket_product_brand_id]) REFERENCES [dbo].[ProductBrands] ([product_brand_id]),
)
