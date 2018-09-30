using ForsaWebAPI.Helper;
using ForsaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ForsaWebAPI.Controllers
{
    public class BankDashBoardController : ApiController
    {
        private ForsaEntities db = new ForsaEntities();

        [HttpGet]
        public IHttpActionResult GetRateOfInterestOfBank(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
            var dtRates = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetRateOfInterestOfBank", System.Data.CommandType.StoredProcedure, param);
            if (dtRates == null)
            {
                //return NotFound();
                return Json(new { IsSuccess = false });
            }

            return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dtRates) });
        }

        [HttpGet]
        public DataTable GetUserGroupForSettingRateOfInterestVisibility(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
            var dtRates = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetUserGroupForSettingRateOfInterestVisibility", System.Data.CommandType.StoredProcedure, param);
            if (dtRates == null)
            {
                //return NotFound();
                return null;
            }

            return dtRates;
        }

        [HttpPost]
        public IHttpActionResult PublishAndUnPublish(int userId, bool IsPublished)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", userId);
            param[1] = new SqlParameter("@IsPublished", IsPublished);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_PublishAndUnPublish", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
        }

        [HttpPost]
        public IHttpActionResult UpdateRateOfInterestOfBank(RateOfInterestOfBankModel objRate)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Id", objRate.Id);
            param[1] = new SqlParameter("@RateOfInterest", objRate.RateOfInterest);
            param[2] = new SqlParameter("@Modifiedby", objRate.ModifiedBy);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateRateOfInterestOfBank", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
        }

        [HttpPost]
        public IHttpActionResult UpdateUserGroupAgainstBankWhomRateOfInterestWillBeVisible(int UserId, string GroupIds)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", UserId);
            param[1] = new SqlParameter("@GroupIds", GroupIds);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateUserGroupAgainstBankWhomRateOfInterestWillBeVisible", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
        }

        [HttpGet]
        public IHttpActionResult GetLenderSendRequestRequestdOnTheBasisOfBorrowerId(int borrowerId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BorrowerId", borrowerId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetLenderSendRequestRequestdOnTheBasisOfBorrowerId", System.Data.CommandType.StoredProcedure, param);
            if(dt ==null)
                return Json(new { IsSuccess = false });
            if (dt.Rows.Count == 0)
                return Json(new { IsSuccess = false, IfDataFound = false });
            return Json(new { IsSuccess = true, IfDataFound = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpPost]
        public IHttpActionResult UpdateRateOfInterest(LenderSendRequestModel sendRequestModel)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RateOfInterestOfferred", sendRequestModel.RateOfInterest);
            param[1] = new SqlParameter("@RequestId", sendRequestModel.RequestId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSendRequest_UpdateRateOfInterest", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true});
        }
    }
}
