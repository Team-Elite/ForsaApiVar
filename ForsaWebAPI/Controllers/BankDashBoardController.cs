
using ForsaWebAPI.Controllers.Models;
using ForsaWebAPI.Helper;
using ForsaWebAPI.Models;
using ForsaWebAPI.Persistance.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Id", objRate.Id);
            param[1] = new SqlParameter("@RateOfInterest", objRate.RateOfInterest);
            param[2] = new SqlParameter("@RateOfInterest2", objRate.RateOfInterest2);
            param[3] = new SqlParameter("@RateOfInterest3", objRate.RateOfInterest3);
            param[4] = new SqlParameter("@Modifiedby", objRate.ModifiedBy);
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

        [HttpPost()]
        public int UploadFiles()
        {
            int iUploadedCnt = 0;
            int documentId = 0;
            string sPath = "";
            //sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Test/");
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Docs/" + Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["userId"]) + "/UserProfile/");
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            System.Web.HttpPostedFile hpf;
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        documentId = SaveDocumentInfoInDb(sPath + "/" + hpf.FileName,
                            "BankUserProfile", Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["userId"]), hpf.FileName);
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return documentId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentNameWithPath">Path of file with name</param>
        /// <param name="calledFrom">That is from Banker Dashboard profile, lender profile etc.</param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int SaveDocumentInfoInDb(string documentNameWithPath, string calledFrom, int UserId, string fileName)
        {

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DocumentNameWithPath", documentNameWithPath);
            param[1] = new SqlParameter("@Type", calledFrom);
            param[2] = new SqlParameter("@UserId", UserId);
            param[3] = new SqlParameter("@FileName", fileName);
            var docId = SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_SaveDocumentInfo", System.Data.CommandType.StoredProcedure, param);
            return Convert.ToInt32(docId);

        }

        [HttpPost]
        public IHttpActionResult DeleteDocument(ApiRequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            DocumentModel sendRequestModel = JsonConvert.DeserializeObject<DocumentModel>(data);
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Id", sendRequestModel.docId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_DeleteDocument", System.Data.CommandType.StoredProcedure, param);
            var sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Docs/" + sendRequestModel.userId + "/UserProfile/");
            if (sendRequestModel.calledFrom == (int)EnumClass.DocUploadCalledFrom.BankUserProfile)
                if (File.Exists(sPath + sendRequestModel.docName))
                    File.Delete(sPath + sendRequestModel.docName);
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken("True") });

        }

        [HttpPost]
        public IHttpActionResult GetDocList(ApiRequestModel requestModel)
        {
            var user = JsonConvert.DeserializeObject<DocumentModel>((new JwtTokenManager().DecodeToken(requestModel.Data)));
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", user.userId);
            param[1] = new SqlParameter("@Type", user.type);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetDocList", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
            {
                //return NotFound();
                return Json(new { IsSuccess = false });
            }

            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
        }


    }
}
