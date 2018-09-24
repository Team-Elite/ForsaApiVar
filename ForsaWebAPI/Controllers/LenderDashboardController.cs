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
        public IHttpActionResult GetAllBanksWithStatusIsDeselected(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
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

    }
}
