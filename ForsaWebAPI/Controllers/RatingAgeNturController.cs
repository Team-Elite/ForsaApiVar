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
    public class RatingAgeNturController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetRatingAgeNtur()
        {
            var dtRates = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetRatingAgeNtur", System.Data.CommandType.StoredProcedure);
            if (dtRates == null)
            {
                //return NotFound();
                return null;
            }

           // return dtRates;
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(HelperClass.DataTableToJSONWithJavaScriptSerializer(dtRates))) });

        }
    }
}
