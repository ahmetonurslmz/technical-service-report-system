CREATE TABLE [dbo].[ProductBrands] (
    [product_brand_id]   INT        IDENTITY (1, 1) NOT NULL,
    [product_brand_name] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([product_brand_id] ASC)
);

