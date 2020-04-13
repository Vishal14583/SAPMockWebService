using System;
using System.IO;
using System.Net;
using System.Xml;

namespace SAPMockWebServiceClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            //creating object of program class to access methods  
            Program obj = new Program();

            //Calling InvokeService method  
            obj.InvokeService();
        }

        public void InvokeService()
        {
            //Calling CreateSOAPWebRequest method  
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            
            //SOAP Body Request  
            SOAPReqBody.Load(@"C:\Users\Vishal14583\source\repos\SAPMockWebService\SAPMockWebServiceClient\SAPRequest.xml");



            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }
            //Geting response from request  
            using (WebResponse Serviceres = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                {
                    //reading stream  
                    var ServiceResult = rd.ReadToEnd();
                    //writting stream result on console  
                    Console.WriteLine(ServiceResult);
                    Console.ReadLine();
                }
            }
        }

        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request  
            //HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://localhost:50991/z_mta_interface.asmx");
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://sapmockwebservice.azurewebsites.net/z_mta_interface.asmx");


            //SOAPAction  
            Req.Headers.Add(@"SOAPAction:urn:sap-com:document:sap:rfc:functions/Z_MTA_INTERFACE");
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";
            //return HttpWebRequest  
            return Req;
        }
    }
}
