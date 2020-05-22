CREATE PROCEDURE [dbo].[PfetchTicketsByStatus]
	@TicketStatus int
AS
	SELECT ticket_id, status_name, ticket_status 
	FROM [dbo].[Tickets]
	INNER JOIN [dbo].[TicketStatuses]
	ON [dbo].[Tickets].ticket_status = [dbo].[TicketStatuses].status_id
	WHERE ticket_status = @TicketStatus
RETURN 0