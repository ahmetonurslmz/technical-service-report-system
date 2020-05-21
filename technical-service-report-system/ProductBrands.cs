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
    class ProductBrands
    {
        private SqlConnection con;
        ConnectionController connection;

        public ProductBrands()
        {
            connection = new ConnectionController();
            con = connection.connect();
        }

        public DataSet fetch()
        {
            DataSet data_set = new DataSet();
            SqlCommand cmd = new SqlCommand("PfetchProductBrands", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            sqlDa.Fill(data_set, "productBrandsTable");
            return data_set;
        }
    }
}
