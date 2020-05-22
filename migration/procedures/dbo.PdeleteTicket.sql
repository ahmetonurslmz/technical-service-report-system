CREATE PROCEDURE [dbo].[PdeleteTicket]
	@TicketId int
AS
	DELETE [dbo].[Tickets] WHERE ticket_id=@TicketId
RETURN 0