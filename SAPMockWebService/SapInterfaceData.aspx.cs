using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;

namespace SAPMockWebService
{
    public partial class SapInterfaceData : System.Web.UI.Page
    {
        CloudStorageAccount cloudStorageAccount;
        CloudTableClient cloudTableClient;
        List<ZMTA_SALES> tpsorders1 = null;
        List<ZMTA_SALES> tpsorders2 = null;
        List<string> stsrList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtSearch.Text = string.Empty;
                BindData();
            }
        }


        private void BindData()
        {
            tpsorders1 = new List<ZMTA_SALES>();

            tpsorders1 = GetAllOrders();

            if (tpsorders1.Count > 0)
            {
                transactionList.DataSource = tpsorders1;

                tpsorders1.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp));
                tpsorders1.Reverse();

                transactionList.DataBind();

                decimal sum = 0;

                for (int i = 0; i < tpsorders1.Count; i++)
                {
                    if (tpsorders1[i].GROSSAMOUNT != "" && tpsorders1[i].GROSSAMOUNT != null)
                    {
                        sum += decimal.Parse(tpsorders1[i].GROSSAMOUNT);
                    }
                }
                lblamounttext.Visible = true;
                lblamounttext.Text = "Total payment done till now : ";
                lblamount.Visible = true;
                lblamount.Text = "£ " + sum.ToString("0.00");
            }
        }

        private void showData(string orderid)
        {
            tpsorders2 = new List<ZMTA_SALES>();

            if (orderid != null)
            {
                lblOrder.Text = "Order Id : ";
                lblOrderId.Text = orderid.ToString();
                lblOrder.Visible = true;
                lblOrderId.Visible = true;

                tpsorders2 = GetOrderById(orderid);

                if (tpsorders2.Count > 0)
                {
                    GridView1.DataSource = tpsorders2;
                    GridView1.DataBind();

                    GridView2.DataSource = tpsorders2[0].PRODUCTS;
                    GridView2.DataBind();
                }
                else
                {
                    lblMessage.Text = "Invalid Order Id";
                    lblMessage.Visible = true;
                    lblOrder.Visible = false;
                    lblOrderId.Visible = false;
                    lblamounttext.Visible = false;
                    lblamount.Visible = false;

                    lblMessage.ForeColor = Color.Red;
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Orderid cannot be NULL";
            }
        }

        private List<ZMTA_SALES> GetOrderById(string orderid)
        {
            List<ZMTA_SALES> orders = null;

            InitializeCloudClient();

            // Retrieve cloud table by tablename.
            CloudTable table = cloudTableClient.GetTableReference("sapdatatable");

            // Create table Query
            TableQuery<ZMTA_SALES> query = new TableQuery<ZMTA_SALES>();
            string partitionFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, orderid);
            TableContinuationToken continuationToken = null;

            var page = table.ExecuteQuerySegmented(query.Where(partitionFilter), continuationToken);

            if (page.Results != null)
            {
                orders = new List<ZMTA_SALES>();
                orders.AddRange(page.Results);
            }
            return orders;
        }

        private List<ZMTA_SALES> GetAllOrders()
        {
            List<ZMTA_SALES> orders = null;

            InitializeCloudClient();

            // Retrieve cloud table by tablename.
            CloudTable table = cloudTableClient.GetTableReference("sapdatatable");

            TableQuery<ZMTA_SALES> query = new TableQuery<ZMTA_SALES>();

            if (table != null)
            {
                orders = new List<ZMTA_SALES>();
                foreach (ZMTA_SALES entity in table.ExecuteQuery(query))
                {
                    orders.Add(entity);
                }
            }
            return orders;
        }

        private void InitializeCloudClient()
        {
            if (cloudStorageAccount == null)
            {
                var connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageAccountConnectionString"];

                // Retrieve storage account from connection string.
                cloudStorageAccount = CloudStorageAccount.Parse(connectionString);

                // Create table client.
                cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
        }
        protected void transactionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;

            if (transactionList.SelectedRow != null)
            {
                int index = transactionList.SelectedRow.RowIndex;
                string orderid = transactionList.SelectedRow.Cells[1].Text;

                showData(orderid);
            }
        }
        protected void transactionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            transactionList.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string orderid = txtSearch.Text.ToString();

            if (!string.IsNullOrEmpty(orderid))
            {
                showData(orderid);
            }
        }
    }
}