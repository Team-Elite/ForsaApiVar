using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ForsaWebAPI.Helper
{
    public static class HelperClass
    {
        public static string LoginURL = ConfigurationManager.AppSettings["LoginUrl"].ToString();
        public static string RegistrationEmailSubject = ConfigurationManager.AppSettings["SubjectOfRegistrationEmail"].ToString();
        public static string ForgotPasswordEmailSubject = ConfigurationManager.AppSettings["SubjectOfForgotPasswordEmail"].ToString();
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
    }
}