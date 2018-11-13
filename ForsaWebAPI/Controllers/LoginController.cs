using ForsaWebAPI.Helper;
using ForsaWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ForsaWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        private ForsaEntities db = new ForsaEntities();
     

        [HttpPost]
        public IHttpActionResult ValidateUser(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LoginModel login = JsonConvert.DeserializeObject<LoginModel>(data);

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userName", login.UserName);
            param[1] = new SqlParameter("@password", login.UserPassword);
            param[2] = new SqlParameter("@emailaddress", login.UserEmailId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_ValidateUser", System.Data.CommandType.StoredProcedure,param);
            if (dt == null || dt.Rows.Count==0) 
            {
                return Json(new { IsSuccess = false });
            }
            return Json(new { IsSuccess = true, data  = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
        }
       
        [HttpPost]
        public IHttpActionResult ForgotPassword(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LoginModel loginModel = JsonConvert.DeserializeObject<LoginModel>(data);
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@emailaddress", loginModel.ForgotPasswordEmailId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_VerifyEmailAddress", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false, data = "Email id is wrong." });
            }
            else
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\ForgotPassword.html";
                var bodyOfMail = "";
                using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
                {
                    bodyOfMail = reader.ReadToEnd();
                }

                bodyOfMail = bodyOfMail.Replace("[FirstName]", dt.Rows[0]["FirstName"].ToString());
                bodyOfMail = bodyOfMail.Replace("[Password]", dt.Rows[0]["Password"].ToString());
                // Sending Email
                EmailHelper objHelper = new EmailHelper();
                objHelper.SendEMail(loginModel.ForgotPasswordEmailId, HelperClass.ForgotPasswordEmailSubject, bodyOfMail);
            }

            //  return Json(new { IsSuccess = true, data = "Password sent.", Token = JsonConvert.SerializeObject(param) });
            return Json(new { IsSuccess = true, data = "Password sent."});

        }
    }
}
