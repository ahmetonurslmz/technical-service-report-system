CREATE TABLE [dbo].[ProductModels] (
    [product_model_id]   INT          IDENTITY (1, 1) NOT NULL,
    [product_model_name] VARCHAR (20) NOT NULL,
	[product_brand_id]      INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([product_model_id] ASC),
	FOREIGN KEY ([product_brand_id]) REFERENCES [dbo].[ProductBrands] ([product_brand_id])
);