using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System.Web.Services;
using System.Xml.Serialization;

namespace SAPMockWebService
{
    /// <summary>
    /// Summary description for SAPMockWebService
    /// </summary>
    [WebService(Namespace = "urn:sap-com:document:sap:rfc:functions")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SAPMockWebService : Z_MTA_INTERFACE
    {
        [WebMethod]
        [return: XmlElement("Z_MTA_INTERFACEResponse", Namespace = "urn:sap-com:document:sap:rfc:functions")]
        public Z_MTA_INTERFACEResponse Z_MTA_INTERFACE([XmlElement("Z_MTA_INTERFACE", Namespace = "urn:sap-com:document:sap:rfc:functions")] Z_MTA_INTERFACE Z_MTA_INTERFACE)
        {
            Z_MTA_INTERFACEResponse z_MTA_INTERFACEResponse = new Z_MTA_INTERFACEResponse();

            //******************************************Option 1 - Stored in Queue*************************************************************

            //string message = JsonConvert.SerializeObject(Z_MTA_INTERFACE.IM_ORDER);
            //// Retrieve storage account from connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storageaccountvj;AccountKey=2hii7+8ubhWvsA/iTemvRh1aEP/NxtdqAspbAPmPatFK8GNcMtIPNWkyUKUbPVdBV991odApMZ+uEeeyY1AYsw==;EndpointSuffix=core.windows.net");

            //// Create the queue client.
            //CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            //// Retrieve a reference to a queue.
            //CloudQueue queue = queueClient.GetQueueReference("sap-data-queue");

            //// Create the queue if it doesn't already exist.
            //queue.CreateIfNotExists();

            //// Create a message and add it to the queue.
            //CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(message);

            //queue.AddMessageAsync(cloudQueueMessage);

            //******************************************Option 1 - Stored in Queue*************************************************************




            //******************************************Option 2 - Stored in Azure Table*************************************************************
            TableOperation tableOperation = null;
            TableResult tableResult = null;

            var connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageAccountConnectionString"];

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);

            // Create table client.
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();

            // Retrieve cloud table by tablename.
            CloudTable table = cloudTableClient.GetTableReference("sapdatatable");

            //create table if does not exists.
            table.CreateIfNotExistsAsync();

            //add PartitionKey & RowKey
            Z_MTA_INTERFACE.IM_ORDER.RowKey = Z_MTA_INTERFACE.IM_ORDER.UNIQUEID;
            Z_MTA_INTERFACE.IM_ORDER.PartitionKey = Z_MTA_INTERFACE.IM_ORDER.UNIQUEID;


            tableOperation = TableOperation.Insert(Z_MTA_INTERFACE.IM_ORDER);
            tableResult = table.Execute(tableOperation);

            //******************************************Option 2 - Stored in Azure Table*************************************************************

            z_MTA_INTERFACEResponse.EX_MESSAGE = "Thank you for your order - GUID uniqueid " + Z_MTA_INTERFACE.IM_ORDER.UNIQUEID + " is being processed.";
            z_MTA_INTERFACEResponse.EX_STATUS = "Success";

            return z_MTA_INTERFACEResponse;
        }
    }
}
