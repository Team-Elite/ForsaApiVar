using ForsaWebAPI.Helper;
using ForsaWebAPI.Models;
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
        [HttpGet]
        public IHttpActionResult GetAllBanksWithInterestRateHorizontaly(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetAllBanksWithInterestRateHorizontaly", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpGet]
        public IHttpActionResult GetAllBanksWithInterestRateHorizontalyOrderByColumnName(int userId, string orderBy)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", userId);
            param[1] = new SqlParameter("@OrderBy", orderBy+ " desc");
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetAllBanksWithInterestRateHorizontaly", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpGet]
        public IHttpActionResult DeselectBank(int userId, int bankId, Boolean IsSelected)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserId", userId);
            param[1] = new SqlParameter("@BankId", bankId);
            param[2] = new SqlParameter("@IsSelected", IsSelected);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_DeselectBank ", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
        }

        [HttpGet]
        public IHttpActionResult GetAllBanksWithStatusIsDeselected(int userId, int PageNumber)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", userId);
            param[1] = new SqlParameter("@PageNumber", PageNumber);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_Lender_GetAllBanksWithStatusIsDeselected", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpGet]
        public IHttpActionResult GetAllBanksWithInterestRateHorizontalyWhichAreNotDeSelected(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetAllBanksWithInterestRateHorizontalyWhichAreNotDeSelected", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
            {
                return Json(new { IsSuccess = false });
            }
            if (dt.Rows.Count == 0){
                return Json(new { IsSuccess = true,IfDataFound=false});
            }
            return Json(new { IsSuccess = true, IfDataFound = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpGet]
        public IHttpActionResult GetPagesForLenderSettingStartPage(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetPagesForLenderSettingStartPage", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
            {
                return Json(new { IsSuccess = false });
            }
            if (dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = true, IfDataFound = false });
            }
            return Json(new { IsSuccess = true, IfDataFound = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpGet]
        public IHttpActionResult LenderSaveStartPage(int userId, int pageId)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", userId);
            param[1] = new SqlParameter("@PageId", pageId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSaveStartPage", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true});
        }

        [HttpGet]
        public IHttpActionResult GetLenderSendRequestPendingLendedRequestByLenderId(int lenderId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@LenderId", lenderId);
            var dt =SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetLenderSendRequestPendingLendedRequestByLenderId", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
                return Json(new { IsSuccess = false });
            if (dt.Rows.Count == 0)
                return Json(new { IsSuccess = false, IfDataFound = false });
            return Json(new { IsSuccess = true, IfDataFound = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpPost]
        public IHttpActionResult AcceptLendedRequest(LenderSendRequestModel sendRequestModel)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IsAccepted", sendRequestModel.IsAccepted);
            param[1] = new SqlParameter("@RequestId", sendRequestModel.RequestId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSendRequest_AcceptLendedRequest", System.Data.CommandType.StoredProcedure, param);

            #region Sending Mail to Lender
            // Sending Email to Lender.
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\LenderSendRequestAccepted.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[User]", "Lender: <b>"+sendRequestModel.LenderName+"</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", sendRequestModel.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", sendRequestModel.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", sendRequestModel.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", sendRequestModel.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", sendRequestModel.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", sendRequestModel.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", sendRequestModel.RateOfInterest.ToString());
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(sendRequestModel.LenderEmailId, HelperClass.LenderSendRequestAccepted, bodyOfMail);
            #endregion

            #region Sending Mail to Borrower

            // Sending email to borrower
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }
            bodyOfMail = bodyOfMail.Replace("[User]", "Borrower: <b>" + sendRequestModel.BorrowerName + "</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", sendRequestModel.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", sendRequestModel.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", sendRequestModel.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", sendRequestModel.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", sendRequestModel.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", sendRequestModel.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", sendRequestModel.RateOfInterest.ToString());

            objHelper.SendEMail(sendRequestModel.BorrowerEmailId, HelperClass.LenderSendRequestAccepted, bodyOfMail);
            #endregion

            return Json(new { IsSuccess = true });
        }

        [HttpPost]
        public IHttpActionResult RejectLendedRequest(LenderSendRequestModel sendRequestModel)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IsRejected", sendRequestModel.IsRejected);
            param[1] = new SqlParameter("@RequestId", sendRequestModel.RequestId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSendRequest_RejectLendedRequest", System.Data.CommandType.StoredProcedure, param);

            #region Sending Mail to Lender
            // Sending Email to Lender.
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\LenderSendRequestRejected.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[User]", "Lender: <b>" + sendRequestModel.LenderName + "</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", sendRequestModel.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", sendRequestModel.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", sendRequestModel.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", sendRequestModel.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", sendRequestModel.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", sendRequestModel.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", sendRequestModel.RateOfInterest.ToString());
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(sendRequestModel.LenderEmailId, HelperClass.LenderSendRequestRejected, bodyOfMail);
            #endregion

            #region Sending Mail to Borrower

            // Sending email to borrower
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }
            bodyOfMail = bodyOfMail.Replace("[User]", "Borrower: <b>" + sendRequestModel.BorrowerName + "</b>");
            bodyOfMail = bodyOfMail.Replace("[Amount]", sendRequestModel.Amount.ToString());
            bodyOfMail = bodyOfMail.Replace("[StartDate]", sendRequestModel.StartDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[EndDate]", sendRequestModel.EndDate.ToString("MM/dd/yyyy"));
            bodyOfMail = bodyOfMail.Replace("[NoOfDay]", sendRequestModel.NoOfDays.ToString());
            bodyOfMail = bodyOfMail.Replace("[INTERESTCONVENTION]", sendRequestModel.InterestConventionName);
            bodyOfMail = bodyOfMail.Replace("[Payments]", sendRequestModel.PaymentsName);
            bodyOfMail = bodyOfMail.Replace("[RateOfInterest]", sendRequestModel.RateOfInterest.ToString());

            objHelper.SendEMail(sendRequestModel.BorrowerEmailId, HelperClass.LenderSendRequestRejected, bodyOfMail);
            #endregion
            return Json(new { IsSuccess = true });
        }

    }
}
