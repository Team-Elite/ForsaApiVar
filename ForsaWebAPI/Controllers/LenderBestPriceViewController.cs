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
    public class LenderBestPriceViewController : ApiController
    {
        [HttpPost]

        public IHttpActionResult GetRatesByTimePeriod(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel objRate = JsonConvert.DeserializeObject<UserModel>(data);
            // int id = int.Parse(new JwtTokenManager().DecodeToken(requestModel.Data));
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", objRate.UserId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_BestPriceView_GetRatesByTimePeriod", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            //  return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }
        [HttpPost]
        public IHttpActionResult GetBanksByTimePeriod(ApiRequestModel requestModel)
        {   
              var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel objRate = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserId", objRate.UserId);
            param[1] = new SqlParameter("@TimePeriodId", objRate.TimePeriod);
            param[2] = new SqlParameter("@PageNumber", requestModel.PageNumber);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_BestPriceView_GetBanksByTimePeriod", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            // return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult SaveSendRequest(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LenderSendRequestModel sendRequestModel = JsonConvert.DeserializeObject<LenderSendRequestModel>(data);
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@LenderId", sendRequestModel.LenderId);
            param[1] = new SqlParameter("@BorrowerId", sendRequestModel.BorrowerId);
            param[2] = new SqlParameter("@Amount", sendRequestModel.Amount);
            param[3] = new SqlParameter("@StartDate", sendRequestModel.StartDate);
            param[4] = new SqlParameter("@EndDate", sendRequestModel.EndDate);
            param[5] = new SqlParameter("@NoOfDays", sendRequestModel.NoOfDays);
            param[6] = new SqlParameter("@InterestConvention", sendRequestModel.InterestConvention);
            param[7] = new SqlParameter("@Payments", sendRequestModel.Payments);
            param[8] = new SqlParameter("@IsRequestAccepted", sendRequestModel.IsRequestAccepted );

            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_Lender_SaveSendRequest", System.Data.CommandType.StoredProcedure, param);

            // Sending Email.
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\LenderSendRequest.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[FirstName]", sendRequestModel.LenderName);
            bodyOfMail = bodyOfMail.Replace("[Borrower]", sendRequestModel.BorrowerName);
            bodyOfMail = bodyOfMail.Replace("[Amount]", sendRequestModel.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", sendRequestModel.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", sendRequestModel.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", sendRequestModel.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", sendRequestModel.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", sendRequestModel.PaymentsName);
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(sendRequestModel.LenderEmailId, HelperClass.LenderSendRequestSubject, bodyOfMail);

            return Json(new { IsSuccess = true });
        }

        public IHttpActionResult GetRatesByTimePeriodK()
        {
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_BestPriceView_GetRatesByTimePeriodK", System.Data.CommandType.StoredProcedure);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            // return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
        }

        public IHttpActionResult GetBanksByTimePeriodK(ApiRequestModel requestModel, int PageNumber)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel objRate = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TimePeriodId", objRate.TimePeriod);
            param[1] = new SqlParameter("@PageNumber", PageNumber);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_BestPriceView_GetBanksByTimePeriodK", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            // return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
        }
    }
}
