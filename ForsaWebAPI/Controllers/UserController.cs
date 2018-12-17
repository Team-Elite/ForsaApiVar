using System;
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
using ForsaWebAPI.Persistance.Data;
using ForsaWebAPI.Controllers.Models;

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
        //[HttpPost]
        //public IHttpActionResult GettblUser(ApiRequestModel requestModel)
        //{
        //    int id = int.Parse(new JwtTokenManager().DecodeToken(requestModel.Data));
        //    tblUser tblUser = db.tblUsers.Find(id);
        //    if (tblUser == null)
        //    {
        //        return NotFound();
        //    }

        //    // return Ok(tblUser);
        //    return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(tblUser)) });

        //}

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
        public IHttpActionResult RegisterUser()
        {
            ApiRequestModel requestModel = JsonConvert.DeserializeObject<ApiRequestModel>(System.Web.HttpContext.Current.Request.Form["encrypted"].ToString());

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);

            var result = 0;
            // CHECK THE FILE COUNT.
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string password = RandomString(6);
            SqlParameter[] param = new SqlParameter[38];
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
            param[29] = new SqlParameter("@DepositInsuranceAmount", user.DepositInsuranceAmount);
            param[30] = new SqlParameter("@ClientGroupId", user.ClientGroupId);
            param[31] = new SqlParameter("@AgreeToThePrivacyPolicy", user.AgreeToThePrivacyPolicy);
            param[32] = new SqlParameter("@AgreeToTheRatingsMayPublish", user.AgreeToTheRatingsMayPublish);
            param[33] = new SqlParameter("@AgreeThatInformationOfCompanyMayBePublished", user.AgreeThatInformationOfCompanyMayBePublished);
            param[34] = new SqlParameter("@AcceptAGBS", user.AcceptAGBS);
            param[35] = new SqlParameter("@CommercialRegisterExtract", string.Empty);
            param[36] = new SqlParameter("@IdentityCard", string.Empty);
            param[37] = new SqlParameter("@result", result);
            param[37].Direction = ParameterDirection.InputOutput;
            SqlHelper.ExecuteNonQuery(HelperClass.ConnectionString, "USP_InsertUser", System.Data.CommandType.StoredProcedure, param);
            user.UserId = (int)param[37].Value;
            if (user.UserId < 0) return Json(new { IsSuccess = false });
            var FilePath = String.Format(@"{0}uploads\docs\{1}\userprofile", AppDomain.CurrentDomain.BaseDirectory, user.UserId);
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (iCnt == 0)
                    {
                        user.CommercialRegisterExtract = HelperClass.UploadDocument(hpf, EnumClass.UploadDocumentType.CommercialRegisterExtract, FilePath);
                    }
                    else
                    {
                        user.IdentityCard = HelperClass.UploadDocument(hpf, EnumClass.UploadDocumentType.IdendityCard, FilePath);
                    }
                    //// CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    //if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    //{
                    //    // SAVE THE FILES IN THE FOLDER.
                    //    hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                    //    if (iCnt == 0)
                    //        CommercialRegisterExtractFileName = hpf.FileName;
                    //    else if (iCnt == 1)
                    //        IdentityCardFileName = hpf.FileName;
                    //}
                }
            }
            UpdateUserPeronalDocs(user);
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

        private void UpdateUserPeronalDocs(UserModel user)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserId", user.UserId);
            param[1] = new SqlParameter("@CommercialRegisterExtract", user.CommercialRegisterExtract);
            param[2] = new SqlParameter("@IdentityCard", user.IdentityCard);
            var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_UpdateUserPeronalDocs", System.Data.CommandType.StoredProcedure, param);

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

        //// DELETE: api/User/5
        ////[ResponseType(typeof(tblUser))]
        //[HttpDelete]
        //public IHttpActionResult DeletetblUser(int id)
        //{
        //    tblUser tblUser = db.tblUsers.Find(id);
        //    if (tblUser == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tblUsers.Remove(tblUser);
        //    db.SaveChanges();

        //    return Ok(tblUser);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool tblUserExists(int id)
        //{

        //    return db.tblUsers.Count(e => e.UserId == id) > 0;
        //}

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






        [HttpPost]
        public IHttpActionResult UpdateUserDetails()
        {
            try
            {
                ApiRequestModel requestModel = JsonConvert.DeserializeObject<ApiRequestModel>(System.Web.HttpContext.Current.Request.Form["encrypted"].ToString());

                var data = new JwtTokenManager().DecodeToken(requestModel.Data);
                UserModel user = JsonConvert.DeserializeObject<UserModel>(data);


                // CHECK THE FILE COUNT.
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                var FilePath = String.Format(@"{0}uploads\docs\{1}\userprofile", AppDomain.CurrentDomain.BaseDirectory, user.UserId);
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        if (iCnt == 0)
                        {
                            user.CommercialRegisterExtract = HelperClass.UploadDocument(hpf, EnumClass.UploadDocumentType.CommercialRegisterExtract, FilePath);
                        }
                        else
                        {
                            user.IdentityCard = HelperClass.UploadDocument(hpf, EnumClass.UploadDocumentType.IdendityCard, FilePath);
                        }
                    }
                }

                //var data = new JwtTokenManager().DecodeToken(requestModel.Data);
                //UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
                string password = RandomString(6);
                SqlParameter[] param = new SqlParameter[36];
                param[0] = new SqlParameter("@NameOfCompany", user.NameOfCompany == null ? "":user.NameOfCompany);
                param[1] = new SqlParameter("@Street", user.Street == null ? "": user.Street);
                param[2] = new SqlParameter("@PostalCode", user.PostalCode == null ? "" : user.PostalCode);
                param[3] = new SqlParameter("@Place", user.Place == null ? "" : user.Place);
                param[4] = new SqlParameter("@AccountHolder", user.AccountHolder == null ? "" : user.AccountHolder);
                param[5] = new SqlParameter("@Bank", user.Bank == null ? "" : user.Bank);
                param[6] = new SqlParameter("@IBAN", user.IBAN == null ? "" : user.IBAN);
                param[7] = new SqlParameter("@BICCode", user.BICCode == null ? "" : user.BICCode);
                param[8] = new SqlParameter("@GroupIds", user.GroupIds == null ? "" : user.GroupIds);
                param[9] = new SqlParameter("@SubGroupId", user.SubGroupId == null ? "" : user.SubGroupId);
                param[10] = new SqlParameter("@LEINumber", user.LEINumber == null ? "" : user.LEINumber);
                param[11] = new SqlParameter("@FurtherField4", user.FurtherField4 == null ? "" : user.FurtherField4);
                param[12] = new SqlParameter("@Salutation", user.Salutation );
                param[13] = new SqlParameter("@Title", user.Title == null ? "" : user.Title);

                param[14] = new SqlParameter("@FirstName", user.FirstName == null ? "" : user.FirstName);
                param[15] = new SqlParameter("@SurName", user.SurName == null ? "" : user.SurName);
                param[16] = new SqlParameter("@ContactNumber", user.ContactNumber == null ? "" : user.ContactNumber);
                param[17] = new SqlParameter("@FurtherField1", user.FurtherField1 == null ? "" : user.FurtherField1);
                param[18] = new SqlParameter("@FurtherField2", user.FurtherField2 == null ? "" : user.FurtherField2);
                param[19] = new SqlParameter("@FurtherField3", user.FurtherField3 == null ? "" : user.FurtherField3);
                param[20] = new SqlParameter("@UserTypeId", user.UserTypeId == null ? "" : user.UserTypeId);
                param[21] = new SqlParameter("@RatingAgentur1", user.RatingAgentur1 == null ? "" : user.RatingAgentur1);
                param[22] = new SqlParameter("@RatingAgenturValue1", user.RatingAgenturValue1 == null ? "" : user.RatingAgenturValue1);
                param[23] = new SqlParameter("@RatingAgentur2", user.RatingAgentur2 == null ? "" : user.RatingAgentur2);
                param[24] = new SqlParameter("@RatingAgenturValue2", user.RatingAgenturValue2 == null ? "" : user.RatingAgenturValue2);
                param[25] = new SqlParameter("@DepositInsurance", user.DepositInsurance);
                param[26] = new SqlParameter("@ClientGroupId", user.ClientGroupId );
                param[27] = new SqlParameter("@AgreeToThePrivacyPolicy", user.AgreeToThePrivacyPolicy );
                param[28] = new SqlParameter("@AgreeToTheRatingsMayPublish", user.AgreeToTheRatingsMayPublish );
                param[29] = new SqlParameter("@AgreeThatInformationOfCompanyMayBePublished", user.AgreeThatInformationOfCompanyMayBePublished );
                param[30] = new SqlParameter("@AcceptAGBS", user.AcceptAGBS );
                param[31] = new SqlParameter("@UserId", user.UserId );
                param[32] = new SqlParameter("@DepositInsuranceAmount", user.DepositInsuranceAmount);
                param[33] = new SqlParameter("@CommercialRegisterExtract", user.CommercialRegisterExtract == null ? "" : user.CommercialRegisterExtract);
                param[34] = new SqlParameter("@IdentityCard", user.IdentityCard == null ? "" : user.IdentityCard);
                param[35] = new SqlParameter("@UserName",string.IsNullOrEmpty( user.UserName) ? string.Empty : user.UserName);
                SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateKontactUserInformation", System.Data.CommandType.StoredProcedure, param);

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
                //  return Json(new { IsSuccess = true, data  = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(HelperClass.DataTableToJSONWithJavaScriptSerializer(dt))) });

            }
            catch( Exception ex)
            {
                return Json(new { IsSuccess = false });
            }


        }

        [HttpPost]
        public IHttpActionResult UpdateUserProfile2()
        {
            try
            {
                ApiRequestModel requestModel = JsonConvert.DeserializeObject<ApiRequestModel>(System.Web.HttpContext.Current.Request.Form["encrypted"].ToString());

                var data = new JwtTokenManager().DecodeToken(requestModel.Data);
                UserModel user = JsonConvert.DeserializeObject<UserModel>(data);


                // CHECK THE FILE COUNT.
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                var FilePath = String.Format(@"{0}uploads\docs\{1}\userprofile", AppDomain.CurrentDomain.BaseDirectory, user.UserId);
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        if (iCnt == 0)
                        {
                            user.CommercialRegisterExtract = HelperClass.UploadDocument(hpf, EnumClass.UploadDocumentType.CommercialRegisterExtract, FilePath);
                        }
                        else
                        {
                            user.IdentityCard = HelperClass.UploadDocument(hpf, EnumClass.UploadDocumentType.IdendityCard, FilePath);
                        }
                    }
                }

                //var data = new JwtTokenManager().DecodeToken(requestModel.Data);
                //UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
                string password = RandomString(6);
                SqlParameter[] param = new SqlParameter[15];
                param[0] = new SqlParameter("@NameOfCompany", user.NameOfCompany == null ? "" : user.NameOfCompany);
                param[1] = new SqlParameter("@Street", user.Street == null ? "" : user.Street);
                param[2] = new SqlParameter("@PostalCode", user.PostalCode == null ? "" : user.PostalCode);
                param[3] = new SqlParameter("@Place", user.Place == null ? "" : user.Place);
                param[4] = new SqlParameter("@AccountHolder", user.AccountHolder == null ? "" : user.AccountHolder);
                param[5] = new SqlParameter("@Bank", user.Bank == null ? "" : user.Bank);
                param[6] = new SqlParameter("@IBAN", user.IBAN == null ? "" : user.IBAN);
                param[7] = new SqlParameter("@BICCode", user.BICCode == null ? "" : user.BICCode);
                param[8] = new SqlParameter("@Title", user.Title == null ? "" : user.Title);
                param[9] = new SqlParameter("@FirstName", user.FirstName == null ? "" : user.FirstName);
                param[10] = new SqlParameter("@SurName", user.SurName == null ? "" : user.SurName);
                param[11] = new SqlParameter("@ContactNumber", user.ContactNumber == null ? "" : user.ContactNumber);
                param[12] = new SqlParameter("@RatingAgenturValue1", user.RatingAgenturValue1 == null ? "" : user.RatingAgenturValue1);
                param[13] = new SqlParameter("@MinVolume", user.MinVolume );
                param[14] = new SqlParameter("@UserId", user.UserId);
                
                SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateUserProfile", System.Data.CommandType.StoredProcedure, param);

                return Json(new { IsSuccess = true });
                //  return Json(new { IsSuccess = true, data  = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(HelperClass.DataTableToJSONWithJavaScriptSerializer(dt))) });

            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false });
            }


        }

    }
}