CREATE PROCEDURE [dbo].[PcreateTicket]
	@CustomerFullName nvarchar(50),
	@CustomerEmail nvarchar(50),
	@CustomerPhoneNumber nvarchar(50),
	@TicketProductImagePath nvarchar(100),
	@TicketEstimatedDeliveryDate datetime,
	@TicketEstimatedCost int,
	@TicketDescription nvarchar(200),
	@TicketProductModelId int
AS
	INSERT INTO [dbo].[Tickets] (customer_full_name, customer_email, customer_phone_number, ticket_product_image_path, ticket_estimated_delivery_date, ticket_estimated_cost, ticket_description, ticket_product_model_id)
	VALUES (@CustomerFullName, @CustomerEmail, @CustomerPhoneNumber, @TicketProductImagePath, @TicketEstimatedDeliveryDate, @TicketEstimatedCost, @TicketDescription, @TicketProductModelId)
RETURN 0
