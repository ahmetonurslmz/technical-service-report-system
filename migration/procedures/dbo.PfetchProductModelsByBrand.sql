CREATE PROCEDURE [dbo].[PfetchProductModelsByBrand]
	@productBrandId INT
AS
	SELECT * FROM [dbo].[ProductModels] where product_brand_id = @productBrandId
RETURN 0