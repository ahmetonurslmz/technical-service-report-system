using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace technical_service_report_system
{
    public partial class lblCustomerPhoneNumber : Form
    {
        ProductBrands productBrands;
        ProductModels productModels;
        Tickets tickets;
        BindingSource binder = new BindingSource();
        string FileName = "";

        public lblCustomerPhoneNumber()
        {
            InitializeComponent();
            pnlCreateTicket.Visible = false;
            pnlTickets.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.fetchBrands();
        }

        private void btnCreateTicket_Click(object sender, EventArgs e)
        {
            string customerFullName;
            string customerEmail;
            string customerPhoneNumber;
            string ticketDescription = "";

            int productBrandId;
            int productModelId;
            int productEstimatedCost;
            DateTime productEstimatedDate;


            try
            {
                // customer full name
                if (txtCustomerFullName.TextLength != 0)
                {
                    customerFullName = txtCustomerFullName.Text;
                }
                else
                {
                    throw new Exception("You must enter customer full name.");
                }
                // customer email
                if (txtCustomerEmail.TextLength != 0)
                {
                    customerEmail = txtCustomerEmail.Text;
                }
                else
                {
                    throw new Exception("You must enter customer email.");
                }
                // customer phone number
                if (txtCustomerPhoneNumber.Text.ToString().Length != 0)
                {
                    if (txtCustomerPhoneNumber.MaskCompleted)
                    {
                        customerPhoneNumber = txtCustomerPhoneNumber.Text.ToString();
                    }
                    else
                    {
                        throw new Exception("Invalid Phone Number");
                    }
                }
                else
                {
                    throw new Exception("You must enter phone number.");
                }
                // product brand
                if (cmbProductBrand.SelectedIndex > -1)
                {
                    productBrandId = ((KeyValuePair<int, string>)cmbProductBrand.SelectedItem).Key;
                }
                else
                {
                    throw new Exception("You must select product brand.");
                }
                // product model
                if (cmbProductModel.SelectedIndex > -1)
                {
                    productModelId = ((KeyValuePair<int, string>)cmbProductModel.SelectedItem).Key;
                }
                else
                {
                    throw new Exception("You must select product brand.");
                }
                // product estimated cost
                if (txtProductCost.Text.ToString().Length != 0)
                {
                    productEstimatedCost = Convert.ToInt32(txtProductCost.Text.ToString());
                }
                else
                {
                    throw new Exception("You must enter estimated product cost.");
                }
                // product estimated delivery date
                if (dateTimeProductDate.Value.ToString().Length != 0)
                {
                    productEstimatedDate = dateTimeProductDate.Value;
                }
                else
                {
                    throw new Exception("You must enter estimated product date.");
                }
                // ticket description
                if (rTxtTicketDescription.Text.ToString().Length != 0)
                {
                    ticketDescription = rTxtTicketDescription.Text.ToString();
                }
                string imageName = "";
                if (FileName.Length != 0)
                {
                    imageName = SaveImage();
                }
                tickets = new Tickets();
                Boolean result = tickets.create(customerFullName, customerEmail, customerPhoneNumber, imageName, productEstimatedDate, productEstimatedCost, productModelId, ticketDescription);
                if (result)
                {
                    goTicketsPage();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString(), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void fetchBrands()
        {
            productBrands = new ProductBrands();
            DataSet data_set = productBrands.fetch();
            int count = data_set.Tables[0].Rows.Count;
            Dictionary<int, string> comboSource = new Dictionary<int, string>();

            for (int i = 0; i < count; i++)
            {
                DataRow table = data_set.Tables[0].Rows[i];
                comboSource.Add(Convert.ToInt32(table.ItemArray.GetValue(0).ToString()), table.ItemArray.GetValue(1).ToString());
            }

            cmbProductBrand.DataSource = new BindingSource(comboSource, null);
            cmbProductBrand.DisplayMember = "Value";
            cmbProductBrand.ValueMember = "Key";

            DataRow first = data_set.Tables[0].Rows[0];
            fetchModels(Convert.ToInt32(first.ItemArray.GetValue(0).ToString()));
        }


        public void fetchModels(int product_brand_id)
        {
            productModels = new ProductModels();
            DataSet product_data_set = productModels.fetchByBrand(product_brand_id);
            int count = product_data_set.Tables[0].Rows.Count;
            Dictionary<int, string> comboSource = new Dictionary<int, string>();

            for (int i = 0; i < count; i++)
            {
                DataRow table = product_data_set.Tables[0].Rows[i];
                comboSource.Add(Convert.ToInt32(table.ItemArray.GetValue(0).ToString()), table.ItemArray.GetValue(1).ToString());
            }

            cmbProductModel.DataSource = new BindingSource(comboSource, null);
            cmbProductModel.DisplayMember = "Value";
            cmbProductModel.ValueMember = "Key";
        }

        private void cmbProductBrand_SelectedValueChanged(object sender, EventArgs e)
        {
            fetchModels(((KeyValuePair<int, string>)cmbProductBrand.SelectedItem).Key);
        }


        private void goTicketsPage()
        {
            pnlCreateTicket.Visible = false;
            pnlTickets.Visible = true;
            fetchTickets();
        }

        private void fetchTickets()
        {
            tickets = new Tickets();
            DataTable salesTable = tickets.fetch();
            binder.DataSource = salesTable;
            dgvTickets.DataSource = binder;
        }

        private void btnMenuCreateTicket_Click(object sender, EventArgs e)
        {
            pnlCreateTicket.Visible = true;
            pnlTickets.Visible = false;
        }

        private void btnMenuTickets_Click(object sender, EventArgs e)
        {
            goTicketsPage();
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select an Image";
            dlg.Filter = "First Selection |*.jpg| Second Selection |*.png| All Files |*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pctProductImage.Image = new Bitmap(dlg.OpenFile());
                    FileName = dlg.FileName;
                }
                catch (Exception)
                {
                    MessageBox.Show("You have to select image", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            dlg.Dispose();
        }

        private string SaveImage()
        {
            if (FileName.Length != 0)
            {
                string projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                File.Copy(FileName, Path.Combine(@projectPath + @"\Resources\img\", Path.GetFileName(FileName)), true);
                return Path.GetFileName(FileName);
            }
            return "";
        }
    }
}
