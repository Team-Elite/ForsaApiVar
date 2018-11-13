using ForsaWebAPI.Helper;
using Newtonsoft.Json;
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
    public class DepositInsuranceController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetDepositInsurance()
        {
            var dtRates = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetDepositInsurance", System.Data.CommandType.StoredProcedure);
            if (dtRates == null)
            {
                //return NotFound();
                return null;
            }

           // return dtRates;
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dtRates)) });

        }
    }
}
