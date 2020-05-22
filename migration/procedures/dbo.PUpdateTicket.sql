CREATE PROCEDURE [dbo].[PupdateTicket]
	@TicketId int,
	@TicketStatus int
AS
	UPDATE [dbo].[Tickets] SET ticket_status=@TicketStatus
	WHERE ticket_id=@TicketId
RETURN 0