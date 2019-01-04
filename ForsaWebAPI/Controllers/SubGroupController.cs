using ForsaWebAPI.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ForsaWebAPI.Controllers
{
    public class SubGroupController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetSubGroup()
        {
            var dtRates = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetSubGroup", System.Data.CommandType.StoredProcedure);
            if (dtRates == null)
            {
                //return NotFound();
                return null;
            }

          //  return dtRates;
           return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dtRates)) });

        }
    }
}
