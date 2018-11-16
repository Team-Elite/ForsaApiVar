using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ForsaWebAPI.Models;
using Newtonsoft.Json;

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

        public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

        internal static dynamic UploadDocument(dynamic commercialRegisterExtract, EnumClass.UploadDocumentType documentType, string FilePath)
        {
            HttpPostedFile httpPostedFile = commercialRegisterExtract as HttpPostedFile;
            // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            FilePath = String.Format(@"{0}\{1}", FilePath, documentType.ToString());
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            var FileName = string.Format("{0}/{1}", FilePath, httpPostedFile.FileName);
            httpPostedFile.SaveAs(FileName);
            return FileName;
        }
    }
}