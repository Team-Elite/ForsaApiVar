using AutoMapper;
using ForsaWebAPI.Helper;
using ForsaWebAPI.Models;
using ForsaWebAPI.Persistance.Data;
using ForsaWebAPI.Persistance;
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
    public class LenderDashboardController : ApiController
    {
        private readonly ForsaEntities _context;
          



        [HttpPost]
        public IHttpActionResult GetMaturityList(ApiRequestModel requestModel)
        {
            var user = JsonConvert.DeserializeObject<UserModel>((new JwtTokenManager().DecodeToken(requestModel.Data)));
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@LenderId", user.UserId);
            param[1] = new SqlParameter("@History", requestModel.ShowAll);
            param[2] = new SqlParameter("@Orderby", requestModel.orderBy);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetMaturityList", System.Data.CommandType.StoredProcedure, param);
            if (dt == null )
            {
                //return NotFound();
                return Json(new { IsSuccess = false });
            }

            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
        }


        [HttpPost]
        public IHttpActionResult GetAllBanksWithInterestRateHorizontaly(ApiRequestModel requestModel)
        {
            int id = int.Parse(new JwtTokenManager().DecodeToken(requestModel.Data));
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", id);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetAllBanksWithInterestRateHorizontaly", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            // return Json(new { IsSuccess = true, data = dt });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult GetAllBanksWithInterestRateHorizontalyOrderByColumnName(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel objRate = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", objRate.UserId);
            param[1] = new SqlParameter("@OrderBy", requestModel.orderBy + " desc");
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetAllBanksWithInterestRateHorizontaly", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            //  return Json(new { IsSuccess = true, data = dt });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult DeselectBank(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel objRate = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserId", objRate.UserId);
            param[1] = new SqlParameter("@BankId", objRate.bankId);
            param[2] = new SqlParameter("@IsSelected", objRate.IsSelected);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_DeselectBank ", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });

        }

        [HttpPost]
        public IHttpActionResult GetAllBanksWithStatusIsDeselected(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", user.UserId);
            param[1] = new SqlParameter("@PageNumber", requestModel.PageNumber);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_Lender_GetAllBanksWithStatusIsDeselected", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            // return Json(new { IsSuccess = true, data = dt });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult GetAllBanksWithInterestRateHorizontalyWhichAreNotDeSelected(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId",user.UserId);
            param[1] = new SqlParameter("@orderby", requestModel.orderBy);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetAllBanksWithInterestRateHorizontalyWhichAreNotDeSelected", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
            {
                return Json(new { IsSuccess = false });
            }
            if (dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = true, IfDataFound = false });
            }
            // return Json(new { IsSuccess = true, IfDataFound = true, data = dt });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult GetPagesForLenderSettingStartPage(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel objRate = JsonConvert.DeserializeObject<UserModel>(data);

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", objRate.UserId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetPagesForLenderSettingStartPage", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
            {
                return Json(new { IsSuccess = false });
            }
            if (dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = true, IfDataFound = false });
            }
            //  return Json(new { IsSuccess = true, IfDataFound = true, data = dt });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult LenderSaveStartPage(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LenderStartPageModel objRate = JsonConvert.DeserializeObject<LenderStartPageModel>(data);

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", objRate.UserId);
            param[1] = new SqlParameter("@PageId", objRate.PageId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSaveStartPage", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
        }

        [HttpPost]
        public IHttpActionResult GetLenderSendRequestPendingLendedRequestByLenderId(ApiRequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@LenderId", user.UserId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetLenderSendRequestPendingLendedRequestByLenderId", System.Data.CommandType.StoredProcedure, param);
            //if (dt == null || dt.Rows.Count == 0)
            //    return Json(new { IsSuccess = false, IfDataFound = false });
            //  return Json(new { IsSuccess = true, IfDataFound = true, data = dt });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult AcceptLendedRequest(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LenderSendRequestModel objRate = JsonConvert.DeserializeObject<LenderSendRequestModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IsAccepted", objRate.IsAccepted);
            param[1] = new SqlParameter("@RequestId", objRate.RequestId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSendRequest_AcceptLendedRequest", System.Data.CommandType.StoredProcedure, param);

            #region Sending Mail to Lender
            // Sending Email to Lender.
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\LenderSendRequest.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[User]", "Lender: <b>" + objRate.LenderName + "</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", objRate.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", objRate.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", objRate.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", objRate.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", objRate.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", objRate.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", objRate.RateOfInterest.ToString());
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(objRate.LenderEmailId, HelperClass.LenderSendRequestAccepted, bodyOfMail);
            #endregion

            #region Sending Mail to Borrower

            // Sending email to borrower
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }
            bodyOfMail = bodyOfMail.Replace("[User]", "Borrower: <b>" + objRate.BorrowerName + "</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", objRate.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", objRate.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", objRate.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", objRate.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", objRate.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", objRate.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", objRate.RateOfInterest.ToString());

            objHelper.SendEMail(objRate.BorrowerEmailId, HelperClass.LenderSendRequestAccepted, bodyOfMail);
            #endregion

            return Json(new { IsSuccess = true });
        }

        [HttpPost]
        public IHttpActionResult RejectLendedRequest(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LenderSendRequestModel objRate = JsonConvert.DeserializeObject<LenderSendRequestModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IsRejected", objRate.IsRejected);
            param[1] = new SqlParameter("@RequestId", objRate.RequestId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSendRequest_RejectLendedRequest", System.Data.CommandType.StoredProcedure, param);

            #region Sending Mail to Lender
            // Sending Email to Lender.
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\LenderSendRequest.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[User]", "Lender: <b>" + objRate.LenderName + "</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", objRate.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", objRate.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", objRate.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", objRate.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", objRate.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", objRate.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", objRate.RateOfInterest.ToString());
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(objRate.LenderEmailId, HelperClass.LenderSendRequestRejected, bodyOfMail);
            #endregion

            #region Sending Mail to Borrower

            // Sending email to borrower
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }
            bodyOfMail = bodyOfMail.Replace("[User]", "Borrower: <b>" + objRate.BorrowerName + "</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", objRate.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", objRate.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", objRate.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", objRate.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", objRate.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", objRate.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", objRate.RateOfInterest.ToString());

            objHelper.SendEMail(objRate.BorrowerEmailId, HelperClass.LenderSendRequestRejected, bodyOfMail);
            #endregion
            return Json(new { IsSuccess = true });
        }

        [HttpPost]
        public IHttpActionResult SaveForsaMessage(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LenderSendRequestModel model = JsonConvert.DeserializeObject<LenderSendRequestModel>(data);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MessageForForsa", model.MessageForForsa);
            param[1] = new SqlParameter("@IsMessageSentToForsa", model.IsMessageSentToForsa);
            param[2] = new SqlParameter("@RequestId", model.RequestId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSendRequest_SaveForsaMEssage", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
        }

        [HttpGet]
        public IHttpActionResult GetAllBanksWithInterestRateHorizontalyForKontactUser(string orderBy)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@OrderBy", orderBy);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetAllBanksWithInterestRateHorizontalyForKontactUser", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
        }
    }
}
