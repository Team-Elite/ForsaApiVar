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
    public class LenderBestPriceViewController : ApiController
    {
        [HttpGet]

        public IHttpActionResult GetRatesByTimePeriod(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_BestPriceView_GetRatesByTimePeriod", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        public IHttpActionResult GetBanksByTimePeriod(int userId, int TimePeriod, int PageNumber)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserId", userId);
            param[1] = new SqlParameter("@TimePeriodId", TimePeriod);
            param[2] = new SqlParameter("@PageNumber", PageNumber);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_BestPriceView_GetBanksByTimePeriod", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }

        [HttpPost]
        public IHttpActionResult SaveSendRequest(LenderSendRequestModel sendRequestModel)
        {
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
            return Json(new { IsSuccess = true });
        }
    }
}
