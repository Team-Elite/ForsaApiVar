using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using ForsaWebAPI.Models;

namespace ForsaWebAPI.Helper
{
    public static class HelperClass
    {
        public static string LoginURL = ConfigurationManager.AppSettings["LoginUrl"].ToString();
        public static string RegistrationEmailSubject = ConfigurationManager.AppSettings["SubjectOfRegistrationEmail"].ToString();
        public static string ForgotPasswordEmailSubject = ConfigurationManager.AppSettings["SubjectOfForgotPasswordEmail"].ToString();
        public static string PasswordUpdatedEmailSubject = ConfigurationManager.AppSettings["SubjectOfPasswordUpdatedEmail"].ToString();

        

        public static string LenderSendRequestSubject = ConfigurationManager.AppSettings["SubjectOfLenderSendRequest"].ToString();
        public static string LenderSendRequestAccepted = ConfigurationManager.AppSettings["SubjectOfLenderSendRequestAccepted"].ToString();
        public static string LenderSendRequestRejected = ConfigurationManager.AppSettings["SubjectOfLenderSendRequestRejected"].ToString();

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ForsaDBConnection"].ConnectionString;
        public static void WriteMessage(Exception ex)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\Error.txt";
            using(StreamWriter write = new StreamWriter(path, true))
            {
                write.Write("Exception at " + DateTime.Now + "--------Message" + ex.Message);
                write.NewLine="----------------------------";
                if(ex.InnerException !=null)
                write.Write("" + "-------- Inner exception " + ex.InnerException);
                write.NewLine = "----------------------------";
                write.Write("" + "-------- Stack trace " + ex.StackTrace);
            }
        }
        public static XmlDocument JsonToXML(string json)
        {
            XmlDocument doc = new XmlDocument();

            using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max))
            {
                XElement xml = XElement.Load(reader);
                doc.LoadXml(xml.ToString());
            }

            return doc;
        }
        internal static dynamic UploadDocument(HttpPostedFile hpf, EnumClass.UploadDocumentType idendityCard, string filePath)
        {
            //  HttpPostedFile hpf = idendityCard ;
            string exttension = System.IO.Path.GetExtension(hpf.FileName);
           // var filename = string.Format("{0}/{1}",hpf.FileName,exttension);
            if (!Directory.Exists(filePath))Directory.CreateDirectory(filePath);
            hpf.SaveAs(string.Format(@"{0}\{1}", filePath, hpf.FileName));
            return hpf.FileName;

        }

        //public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        //    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> childRow;
        //    foreach (DataRow row in table.Rows)
        //    {
        //        childRow = new Dictionary<string, object>();
        //        foreach (DataColumn col in table.Columns)
        //        {
        //            childRow.Add(col.ColumnName, row[col]);
        //        }
        //        parentRow.Add(childRow);
        //    }
        //    return jsSerializer.Serialize(parentRow);
        //}
    }
}