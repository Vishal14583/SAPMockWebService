using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SAPMockWebService
{
    public partial class SapInterfaceData : System.Web.UI.Page
    {
        CloudStorageAccount cloudStorageAccount;
        CloudTableClient cloudTableClient;
        List<ZMTA_SALES> zMTA_SALEs = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            zMTA_SALEs = new List<ZMTA_SALES>();

            InitializeCloudClient();

            // Retrieve cloud table by tablename.
            CloudTable table = cloudTableClient.GetTableReference("sapdatatable");

            TableQuery<ZMTA_SALES> query = new TableQuery<ZMTA_SALES>();

            if (table != null)
            {
                foreach (ZMTA_SALES entity in table.ExecuteQuery(query))
                {
                    zMTA_SALEs.Add(entity);
                }
            }

            GridView1.DataSource = zMTA_SALEs;
            GridView1.DataBind();

            decimal sum = 0;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                sum += decimal.Parse(GridView1.Rows[i].Cells[14].Text);
                Label lb = Page.FindControl("totalamount") as Label;
                totalamount.Text = "Total Gross Amount : " + sum.ToString("0.00");
            }

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

        //protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        //{
        //    GridView SC = e.Row.FindControl("GridView2") as GridView;
        //    SC.DataSource = zMTA_PRODUCTs;
        //    SC.DataBind();
        //}
    }
}