using ForsaWebAPI.Helper;
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

        
    }
}
