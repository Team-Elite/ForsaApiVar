
using ForsaWebAPI.Helper;
using ForsaWebAPI.Models;
using ForsaWebAPI.Persistance.Data;
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

    public class BankDashBoardController : ApiController
    {
        private ForsaEntities db = new ForsaEntities();

        [HttpPost]
        public IHttpActionResult GetMaturityList(ApiRequestModel requestModel)
        {
            var user = JsonConvert.DeserializeObject<UserModel>((new JwtTokenManager().DecodeToken(requestModel.Data)));
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BorrowerId", user.UserId);
            param[1] = new SqlParameter("@History", requestModel.ShowAll);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetMaturityList", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
            {
                //return NotFound();
                return Json(new { IsSuccess = false });
            }

            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
        }





        [HttpPost]

        public IHttpActionResult GetRateOfInterestOfBank(ApiRequestModel requestModel)
        {
            var user = JsonConvert.DeserializeObject<UserModel>((new JwtTokenManager().DecodeToken(requestModel.Data)));
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", user.UserId);
            var dtRates = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetRateOfInterestOfBank", System.Data.CommandType.StoredProcedure, param);
            if (dtRates == null)
            {
                //return NotFound();
                return Json(new { IsSuccess = false });
            }

            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dtRates)) });
        }

        [HttpPost]
        public IHttpActionResult GetUserGroupForSettingRateOfInterestVisibility(ApiRequestModel requestModel)
        {
            var user = JsonConvert.DeserializeObject<UserModel>((new JwtTokenManager().DecodeToken(requestModel.Data)));
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", user.UserId);
            var dtRates = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetUserGroupForSettingRateOfInterestVisibility", System.Data.CommandType.StoredProcedure, param);
            if (dtRates == null)
            {
                //return NotFound();
                return null;
            }

            // return dtRates;
            //return Json(new { IsSuccess = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dtRates) });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dtRates)) });

        }

        [HttpPost]
        public IHttpActionResult PublishAndUnPublish(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel objRate = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);

            // int id = int.Parse(new JwtTokenManager().DecodeToken(requestModel.Data));
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", objRate.UserId);
            param[1] = new SqlParameter("@IsPublished", objRate.IsPublished);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_PublishAndUnPublish", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
            //  return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult UpdateRateOfInterestOfBank(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel objRate = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Id", objRate.Id);
            param[1] = new SqlParameter("@RateOfInterest", objRate.RateOfInterest);
            param[2] = new SqlParameter("@Modifiedby", objRate.ModifiedBy);
            var dt = SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateRateOfInterestOfBank", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

            // return Json(new { IsSuccess = true });
        }

        [HttpPost]
        public IHttpActionResult UpdateUserGroupAgainstBankWhomRateOfInterestWillBeVisible(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", user.UserId);
            param[1] = new SqlParameter("@GroupIds", user.GroupIds);
            var dt = SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateUserGroupAgainstBankWhomRateOfInterestWillBeVisible", System.Data.CommandType.StoredProcedure, param);
            // return Json(new { IsSuccess = true });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult GetLenderSendRequestRequestdOnTheBasisOfBorrowerId(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
           
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BorrowerId", user.UserId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetLenderSendRequestRequestdOnTheBasisOfBorrowerId", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
                return Json(new { IsSuccess = false });
            //if (dt.Rows.Count == 0)
            //    return Json(new { IsSuccess = false, IfDataFound = false });
            //return Json(new { IsSuccess = true, IfDataFound = true, data = HelperClass.DataTableToJSONWithJavaScriptSerializer(dt) });
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });

        }

        [HttpPost]
        public IHttpActionResult UpdateRateOfInterest(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LenderSendRequestModel sendRequestModel = JsonConvert.DeserializeObject<LenderSendRequestModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RateOfInterestOfferred", sendRequestModel.RateOfInterest);
            param[1] = new SqlParameter("@RequestId", sendRequestModel.RequestId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_LenderSendRequest_UpdateRateOfInterest", System.Data.CommandType.StoredProcedure, param);
            return Json(new { IsSuccess = true });
        }
    }
}
