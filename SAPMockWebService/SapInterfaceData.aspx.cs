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

        protected void Page_Load(object sender, EventArgs e)
        {
            tpsorders1 = new List<ZMTA_SALES>();
            tpsorders2 = new List<ZMTA_SALES>();

            string orderid = string.Empty;

            if (Request.QueryString["orderid"] != null)
            {
                orderid = Request.QueryString["orderid"].ToString();

                lblOrder.Text = "Order Id : ";
                lblOrderId.Text = orderid.ToString();
                lblOrder.Visible = true;
                lblOrderId.Visible = true;

                tpsorders1 = GetAllOrders();

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

                decimal sum = 0;
                if (tpsorders1.Count > 0)
                {
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
                    lblamount.Text = sum.ToString("0.00");
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Provide orderid in querystring to see the data";
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
    }
}