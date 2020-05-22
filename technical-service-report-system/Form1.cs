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
        DataGridViewRow Selected;
        int selectedRowIndex;

        public lblCustomerPhoneNumber()
        {
            InitializeComponent();
            pnlCreateTicket.Visible = false;
            pnlTickets.Visible = false;
            fetchDashboardData();
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
                    clearCreateTicketForm();
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

        public void clearCreateTicketForm()
        {
            txtCustomerFullName.Text = "";
            txtCustomerEmail.Text = "";
            txtCustomerPhoneNumber.Text = null;
            txtProductCost.Text = null;
            rTxtTicketDescription.Text = "";
            if (pctProductImage.Image != null)
            {
                pctProductImage.Image.Dispose();
                pctProductImage.Image = null;
            }
        }

        public void fetchDashboardData()
        {
            tickets = new Tickets();
            lblPendingCount.Text = tickets.getCountTicketsByStatus(1).ToString();
            lblInProgressCount.Text = tickets.getCountTicketsByStatus(2).ToString();
            lblDoneCount.Text = tickets.getCountTicketsByStatus(3).ToString();
            lblCancelledCount.Text = tickets.getCountTicketsByStatus(4).ToString();
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
            grpUpdateTicket.Visible = false;
            fetchTickets();
        }

        private void fetchTickets()
        {

            tickets = new Tickets();
            DataTable salesTable = tickets.fetch();
            binder.DataSource = salesTable;
            dgvTickets.DataSource = binder;

            dgvTickets.Columns[0].HeaderText = "ID";
            dgvTickets.Columns[1].HeaderText = "Full Name";
            dgvTickets.Columns[2].HeaderText = "Email";
            dgvTickets.Columns[3].HeaderText = "Phone Number";
            dgvTickets.Columns[4].Visible = false;
            dgvTickets.Columns[5].HeaderText = "Delivery Date";
            dgvTickets.Columns[6].HeaderText = "Cost";
            dgvTickets.Columns[7].HeaderText = "Description";
            dgvTickets.Columns[8].HeaderText = "P. Model";
            dgvTickets.Columns[9].HeaderText = "P. Brand";
            dgvTickets.Columns[10].HeaderText = "Status";
            dgvTickets.Columns[11].Visible = false; // Status ID
        }

        private void btnMenuCreateTicket_Click(object sender, EventArgs e)
        {
            pnlCreateTicket.Visible = true;
            pnlTickets.Visible = false;
            pnlDashboard.Visible = false;
            toolStripStatusLabel1.Text = "Create Ticket";
        }

        private void btnMenuTickets_Click(object sender, EventArgs e)
        {
            goTicketsPage();
            pnlDashboard.Visible = false;
            pnlCreateTicket.Visible = false;
            pnlTickets.Visible = true;
            toolStripStatusLabel1.Text = "Tickets";
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
                File.Copy(FileName, Path.Combine(@getProjectPath() + @"\Resources\img\", Path.GetFileName(FileName)), true);
                return Path.GetFileName(FileName);
            }
            return "";
        }

        private string getProjectPath()
        {
            return Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        }

        private void dgvTickets_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Console.WriteLine(e.RowIndex);
            grpUpdateTicket.Visible = true;
            DataGridViewRow selectedRow = dgvTickets.Rows[e.RowIndex];
            selectedRowIndex = e.RowIndex;
            fillTicketCard(selectedRow);
        }

        private void fillTicketCard(DataGridViewRow selectedRow)
        {
            lblUpdateFullName.Text = selectedRow.Cells[1].Value.ToString();
            lblUpdateEmail.Text = selectedRow.Cells[2].Value.ToString();
            lblUpdatePNumber.Text = selectedRow.Cells[3].Value.ToString();
            lblUpdateDeliveryDate.Text = selectedRow.Cells[5].Value.ToString();
            lblUpdateCost.Text = "$" + selectedRow.Cells[6].Value.ToString();
            if (selectedRow.Cells[7].Value.ToString().Length != 0)
            {
                grpDescription.Visible = true;
                lblUpdateDescription.Text = selectedRow.Cells[7].Value.ToString();
            }
            lblUpdateTicketStatus.Text = selectedRow.Cells[10].Value.ToString();

            int progressValue = Convert.ToInt32(selectedRow.Cells[11].Value.ToString());

            if (Convert.ToInt32(selectedRow.Cells[11].Value.ToString()) != 4)
            {
                progressTicket.Visible = true;
                progressTicket.Value = progressValue;
            } else
            {
                progressTicket.Visible = false;
            }


            if (progressValue == 1)
            {
                btnProceed.Text = "Proceed";
                btnProceed.Visible = true;
                btnCancel.Visible = true;
            }
            else if (progressValue == 2)
            {
                btnProceed.Visible = true;
                btnCancel.Visible = true;
                btnProceed.Text = "Done";
            }
            else if (progressValue == 3 || progressValue == 4)
            {
                btnProceed.Visible = false;
                btnCancel.Visible = false;
            }

            Selected = selectedRow;
            if (selectedRow.Cells[4].Value.ToString().Length != 0)
            {
                string path = @getProjectPath() + @"\Resources\img\" + selectedRow.Cells[4].Value.ToString();

                pctUpdateImage.Load(path);
            }
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            ChangeStatus(Convert.ToInt32(Selected.Cells[11].Value.ToString()) + 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ChangeStatus(4);
        }

        private void ChangeStatus(int status)
        {
            tickets = new Tickets();
            Boolean result = tickets.update(Convert.ToInt32(Selected.Cells[0].Value.ToString()), status);
            if (result)
            {
                fetchTickets();
                DataGridViewRow selectedRow = dgvTickets.Rows[selectedRowIndex];
                fillTicketCard(selectedRow);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tickets = new Tickets();
            Boolean result = tickets.delete(Convert.ToInt32(Selected.Cells[0].Value.ToString()));
            if (result)
            {
                fetchTickets();
                grpUpdateTicket.Visible = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMenuDashboard_Click_1(object sender, EventArgs e)
        {
            pnlCreateTicket.Visible = false;
            pnlTickets.Visible = false;
            pnlDashboard.Visible = true;
            toolStripStatusLabel1.Text = "Dashboard";
            fetchDashboardData();
        }
    }
}
