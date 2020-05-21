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
    class ProductModels
    {
        private SqlConnection con;
        ConnectionController connection;

        public ProductModels()
        {
            connection = new ConnectionController();
            con = connection.connect();
        }

        public DataSet fetchByBrand(int brand_id)
        {
            DataSet data_set = new DataSet();
            SqlCommand cmd = new SqlCommand("PfetchProductModelsByBrand", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productBrandId", brand_id);
            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            sqlDa.Fill(data_set, "productModelsTable");
            return data_set;
        }
    }
}
