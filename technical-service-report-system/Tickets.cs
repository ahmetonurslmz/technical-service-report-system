using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace technical_service_report_system
{
    class Tickets
    {
        private SqlConnection con;
        ConnectionController connection;

        public Tickets()
        {
            connection = new ConnectionController();
            con = connection.connect();
        }

        public DataTable fetch()
        {
            SqlCommand cmd = new SqlCommand("PfetchTickets", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            DataTable data_table = new DataTable();
            sqlDa.Fill(data_table);
            return data_table;
        }

        public Boolean create(string customerFullName,
            string customerEmail,
            string customerPhoneNumber,
            string productImagePath,
            DateTime productEstimatedDate,
            int productEstimatedCost,
            int productModelId,
            string ticketDescription)
        {
            SqlCommand cmd = new SqlCommand("PcreateTicket", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerFullName", customerFullName);
            cmd.Parameters.AddWithValue("@CustomerEmail", customerEmail);
            cmd.Parameters.AddWithValue("@CustomerPhoneNumber", customerPhoneNumber);
            cmd.Parameters.AddWithValue("@TicketProductImagePath", productImagePath);
            cmd.Parameters.AddWithValue("@TicketEstimatedDeliveryDate", productEstimatedDate);
            cmd.Parameters.AddWithValue("@TicketEstimatedCost", productEstimatedCost);
            cmd.Parameters.AddWithValue("@TicketDescription", ticketDescription);
            cmd.Parameters.AddWithValue("@TicketProductModelId", productModelId);

            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            DataSet data_set = new DataSet();
            sqlDa.Fill(data_set);

            return true;
        }
    }
}
