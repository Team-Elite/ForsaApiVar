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
    public class LenderStartPageController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetLenderStartPage(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", userId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetLenderStartPage", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false });
            }
            return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
        }
    }
}
