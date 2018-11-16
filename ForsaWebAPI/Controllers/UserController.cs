﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ForsaWebAPI.Models;
using ForsaWebAPI.Helper;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ForsaWebAPI.Controllers
{
    public class UserController : ApiController
    {
        private ForsaEntities db = new ForsaEntities();

        // GET: api/User
        [HttpGet]
        public IHttpActionResult GettblUsers()
        {
            // return db.tblUsers;
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(db.tblCountries)) });

        }

        // GET: api/User/5
        //[ResponseType(typeof(tblUser))]
        [HttpPost]
        public IHttpActionResult GettblUser(ApiRequestModel requestModel)
        {
            int id = int.Parse(new JwtTokenManager().DecodeToken(requestModel.Data));
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            // return Ok(tblUser);
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(tblUser)) });

        }

        [HttpPost]
        public bool IfUserNameAvailable(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
           
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserName", user.UserName);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_CheckIfUserNameExist", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                //return NotFound();
                return false;
            }
            return true;
        }

        [HttpPost]
        public bool IfEmailIdIsRegistered(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
           
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@EmailId", user.EmailAddress);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_CheckIfUserEmailIdExist", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                //return NotFound();
                return false;
            }
            return true;
        }

        // PUT: api/User/5
        //[ResponseType(typeof(void))]
        //[HttpPut]
        //public IHttpActionResult PuttblUser(ApiRequestModel requestModel)
        //{

        //    int id; tblUser tblUser = JsonConvert.DeserializeObject<tblUser>(new JwtTokenManager().DecodeToken(requestModel.Data));

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tblUser.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tblUser).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tblUserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/User
        //[ResponseType(typeof(tblUser))]
        [HttpPost]
        public IHttpActionResult RegisterUser(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
            var FilePath = String.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, user.UserName); 
            if (user.CommercialRegisterExtract != null)
            {
              
                user.CommercialRegisterExtract = HelperClass.UploadDocument(user.CommercialRegisterExtract, EnumClass.UploadDocumentType.CommercialRegisterExtract, FilePath);
            }

            if (user.IdentityCard != null)
            {
                user.IdentityCard = HelperClass.UploadDocument(user.IdentityCard, EnumClass.UploadDocumentType.IdendityCard, FilePath);
            }


            string password = RandomString(6);
            SqlParameter[] param = new SqlParameter[34];
            param[0] = new SqlParameter("@NameOfCompany", user.NameOfCompany);
            param[1] = new SqlParameter("@Street", user.Street);
            param[2] = new SqlParameter("@PostalCode", user.PostalCode);
            param[3] = new SqlParameter("@Place", user.Place);
            param[4] = new SqlParameter("@AccountHolder", user.AccountHolder);
            param[5] = new SqlParameter("@Bank", user.Bank);
            param[6] = new SqlParameter("@IBAN", user.IBAN);
            param[7] = new SqlParameter("@BICCode", user.BICCode);
            param[8] = new SqlParameter("@GroupIds", user.GroupIds == null ? "" : user.GroupIds);
            param[9] = new SqlParameter("@SubGroupId", user.SubGroupId);
            param[10] = new SqlParameter("@LEINumber", user.LEINumber == null ? "" : user.LEINumber);
            param[11] = new SqlParameter("@FurtherField4", user.FurtherField4);
            param[12] = new SqlParameter("@Salutation", user.Salutation);
            param[13] = new SqlParameter("@Title", user.Title);
            param[14] = new SqlParameter("@FirstName", user.FirstName);
            param[15] = new SqlParameter("@SurName", user.SurName);
            param[16] = new SqlParameter("@ContactNumber", user.ContactNumber);
            param[17] = new SqlParameter("@EmailAddress", user.EmailAddress);
            param[18] = new SqlParameter("@UserName", user.UserName);
            param[19] = new SqlParameter("@Password", password);
            param[20] = new SqlParameter("@FurtherField1", user.FurtherField1);
            param[21] = new SqlParameter("@FurtherField2", user.FurtherField2);
            param[22] = new SqlParameter("@FurtherField3", user.FurtherField3);
            param[23] = new SqlParameter("@UserTypeId", user.UserTypeId);
            param[24] = new SqlParameter("@RatingAgentur1", user.RatingAgentur1 == null ? "" : user.RatingAgentur1);
            param[25] = new SqlParameter("@RatingAgenturValue1", user.RatingAgenturValue1 == null ? "" : user.RatingAgenturValue1);
            param[26] = new SqlParameter("@RatingAgentur2", user.RatingAgentur2 == null ? "" : user.RatingAgentur2);
            param[27] = new SqlParameter("@RatingAgenturValue2", user.RatingAgenturValue2 == null ? "" : user.RatingAgenturValue2);
            param[28] = new SqlParameter("@DepositInsurance", user.DepositInsurance);
            param[29] = new SqlParameter("@ClientGroupId", user.ClientGroupId);
            param[30] = new SqlParameter("@AgreeToThePrivacyPolicy", user.AgreeToThePrivacyPolicy);
            param[31] = new SqlParameter("@AgreeToTheRatingsMayPublish", user.AgreeToTheRatingsMayPublish);
            param[32] = new SqlParameter("@AgreeThatInformationOfCompanyMayBePublished", user.AgreeThatInformationOfCompanyMayBePublished);
            param[33] = new SqlParameter("@AcceptAGBS", user.AcceptAGBS);

            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_InsertUser", System.Data.CommandType.StoredProcedure, param);


            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\RegistrationTemplate.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[FirstName]", user.FirstName.ToString());
            bodyOfMail = bodyOfMail.Replace("[UserName]", user.UserName.ToString());
            bodyOfMail = bodyOfMail.Replace("[Password]", password);
            bodyOfMail = bodyOfMail.Replace("[LoginUrl]", HelperClass.LoginURL);
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(user.EmailAddress, HelperClass.RegistrationEmailSubject, bodyOfMail);

            return Json(new { IsSuccess = true });
            //  return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(HelperClass.DataTableToJSONWithJavaScriptSerializer(dt))) });

        }

        [HttpPost]
        public IHttpActionResult UpdateUser(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserId", user.UserId);
            param[1] = new SqlParameter("@Password", user.Password);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_CheckIfPasswordIsCorrect", System.Data.CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new { IsSuccess = false, Message = "Old password is not correct." });
            }

            param = new SqlParameter[5];
            param[0] = new SqlParameter("@FirstName", user.FirstName);
            param[1] = new SqlParameter("@SurName", user.SurName);
            param[2] = new SqlParameter("@NameOfCompany", user.NameOfCompany);
            param[3] = new SqlParameter("@Password", user.NewPassword);
            param[4] = new SqlParameter("@UserId", user.UserId);
            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateUser", System.Data.CommandType.StoredProcedure, param);

            param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", user.UserId);
            dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetUserById", System.Data.CommandType.StoredProcedure, param);

            // Sending Email on Password updated.
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\PasswordUpdated.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[FirstName]", user.FirstName.ToString());
            bodyOfMail = bodyOfMail.Replace("[LoginUrl]", HelperClass.LoginURL);
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(user.EmailAddress, HelperClass.PasswordUpdatedEmailSubject, bodyOfMail);

            return Json(new { IsSuccess = true, Message = "Updated", data = JsonConvert.SerializeObject(dt) });
            //  return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(HelperClass.DataTableToJSONWithJavaScriptSerializer(dt))) });

        }


        //private static 
        string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // DELETE: api/User/5
        //[ResponseType(typeof(tblUser))]
        [HttpDelete]
        public IHttpActionResult DeletetblUser(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUsers.Remove(tblUser);
            db.SaveChanges();

            return Ok(tblUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(int id)
        {

            return db.tblUsers.Count(e => e.UserId == id) > 0;
        }

        [HttpPost]
        public IHttpActionResult GetUserDetailByUserId(ApiRequestModel requestModel)
        {
            UserModel user = JsonConvert.DeserializeObject<UserModel>(new JwtTokenManager().DecodeToken(requestModel.Data));
     
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", user.UserId);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetUserDetailById", System.Data.CommandType.StoredProcedure, param);
            if (dt == null)
                return Json(new { IsSuccess = false });
            if (dt.Rows.Count == 0)
                return Json(new { IsSuccess = false, IfDataFound = false });
            return Json(new { IsSuccess = true, IfDataFound = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(dt)) });
            //  return Json(new { IsSuccess = true, data  = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(DataTableToJSONWithJavaScriptSerializer(dt))) });

        }

        [HttpPut]
        public IHttpActionResult UpdateUserDetails(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
            string password = RandomString(6);
            SqlParameter[] param = new SqlParameter[32];
            param[0] = new SqlParameter("@NameOfCompany", user.NameOfCompany);
            param[1] = new SqlParameter("@Street", user.Street);
            param[2] = new SqlParameter("@PostalCode", user.PostalCode);
            param[3] = new SqlParameter("@Place", user.Place);
            param[4] = new SqlParameter("@AccountHolder", user.AccountHolder);
            param[5] = new SqlParameter("@Bank", user.Bank);
            param[6] = new SqlParameter("@IBAN", user.IBAN);
            param[7] = new SqlParameter("@BICCode", user.BICCode);
            param[8] = new SqlParameter("@GroupIds", user.GroupIds == null ? "" : user.GroupIds);
            param[9] = new SqlParameter("@SubGroupId", user.SubGroupId);
            param[10] = new SqlParameter("@LEINumber", user.LEINumber == null ? "" : user.LEINumber);
            param[11] = new SqlParameter("@FurtherField4", user.FurtherField4);
            param[12] = new SqlParameter("@Salutation", user.Salutation);
            param[13] = new SqlParameter("@Title", user.Title);
            param[14] = new SqlParameter("@FirstName", user.FirstName);
            param[15] = new SqlParameter("@SurName", user.SurName);
            param[16] = new SqlParameter("@ContactNumber", user.ContactNumber);
            param[17] = new SqlParameter("@FurtherField1", user.FurtherField1);
            param[18] = new SqlParameter("@FurtherField2", user.FurtherField2);
            param[19] = new SqlParameter("@FurtherField3", user.FurtherField3);
            param[20] = new SqlParameter("@UserTypeId", user.UserTypeId);
            param[21] = new SqlParameter("@RatingAgentur1", user.RatingAgentur1 == null ? "" : user.RatingAgentur1);
            param[22] = new SqlParameter("@RatingAgenturValue1", user.RatingAgenturValue1 == null ? "" : user.RatingAgenturValue1);
            param[23] = new SqlParameter("@RatingAgentur2", user.RatingAgentur2 == null ? "" : user.RatingAgentur2);
            param[24] = new SqlParameter("@RatingAgenturValue2", user.RatingAgenturValue2 == null ? "" : user.RatingAgenturValue2);
            param[25] = new SqlParameter("@DepositInsurance", user.DepositInsurance);
            param[26] = new SqlParameter("@ClientGroupId", user.ClientGroupId);
            param[27] = new SqlParameter("@AgreeToThePrivacyPolicy", user.AgreeToThePrivacyPolicy);
            param[28] = new SqlParameter("@AgreeToTheRatingsMayPublish", user.AgreeToTheRatingsMayPublish);
            param[29] = new SqlParameter("@AgreeThatInformationOfCompanyMayBePublished", user.AgreeThatInformationOfCompanyMayBePublished);
            param[30] = new SqlParameter("@AcceptAGBS", user.AcceptAGBS);
            param[31] = new SqlParameter("@UserId", user.UserId);

            SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateUserInformation", System.Data.CommandType.StoredProcedure, param);


            var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\RegistrationTemplate.html";
            var bodyOfMail = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            {
                bodyOfMail = reader.ReadToEnd();
            }

            bodyOfMail = bodyOfMail.Replace("[FirstName]", user.FirstName.ToString());
            bodyOfMail = bodyOfMail.Replace("[UserName]", user.UserName.ToString());
            bodyOfMail = bodyOfMail.Replace("[Password]", "Same which was earlier.");
            bodyOfMail = bodyOfMail.Replace("[LoginUrl]", HelperClass.LoginURL);
            // Sending Email
            EmailHelper objHelper = new EmailHelper();
            objHelper.SendEMail(user.EmailAddress, HelperClass.RegistrationEmailSubject, bodyOfMail);

            return Json(new { IsSuccess = true });
            //  return Json(new { IsSuccess = true, data  = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(HelperClass.DataTableToJSONWithJavaScriptSerializer(dt))) });




        }

    }
}