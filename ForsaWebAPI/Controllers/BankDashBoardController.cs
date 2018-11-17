using AutoMapper;
using ForsaWebAPI.Helper;
using ForsaWebAPI.Models;
using ForsaWebAPI.Persistance;
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
        private readonly ForsaEntities _context;
        private IUnitOfWork _unitofWork;
        public BankDashBoardController(ForsaEntities context, IUnitOfWork unitofWork
            )
        {
            _context = context;
            _unitofWork = unitofWork;
        }

        [HttpPost]

        public IHttpActionResult GetRateOfInterestOfBank(ApiRequestModel requestModel)
        {
            var user = JsonConvert.DeserializeObject<UserModel>(new JwtTokenManager().DecodeToken(requestModel.Data));
            var response = _unitofWork.borrowerRepositary.GetRateOfInterestOfBank(user.UserId);
            if (response == null)
            {
                return Json(new { Success = false });
            }
            else
            {

                var list = Mapper.Map<List<USP_GetRateOfInterestOfBank_Result>, List<RateOfInterestOfBankModel>>(response);
                return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(list)) });

            }

        }

        [HttpPost]
        public IHttpActionResult GetUserGroupForSettingRateOfInterestVisibility(ApiRequestModel requestModel)
        {
            var user = JsonConvert.DeserializeObject<UserModel>((new JwtTokenManager().DecodeToken(requestModel.Data)));
            var response = _unitofWork.borrowerRepositary.GetUserGroup(user.UserId);
            if (response == null)
            {
                return Json(new { Success = false });
            }
            else
            {

                var list = Mapper.Map<List<USP_GetUserGroupForSettingRateOfInterestVisibility_Result>, List<GroupModel>>(response);
                return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(list)) });

            }

        }

        [HttpPost]
        public IHttpActionResult PublishAndUnPublish(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel rateOfInterestOfBank = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);
            return Json(new { Success = _unitofWork.borrowerRepositary.PublishAndUnPublish(rateOfInterestOfBank) });
        }

        [HttpPost]
        public IHttpActionResult UpdateRateOfInterestOfBank(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel rateOfInterestOfBank = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);
            return Json(new { Success = _unitofWork.borrowerRepositary.UpdateRateOfInterestOfBank(rateOfInterestOfBank) });
         
        }

        [HttpPost]
        public IHttpActionResult UpdateUserGroupAgainstBankWhomRateOfInterestWillBeVisible(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            RateOfInterestOfBankModel rateOfInterestOfBank = JsonConvert.DeserializeObject<RateOfInterestOfBankModel>(data);
            return Json(new { Success = _unitofWork.borrowerRepositary.VisibleBankRateToGroups(rateOfInterestOfBank) });

        }

        [HttpPost]
        public IHttpActionResult GetLenderSendRequestRequestdOnTheBasisOfBorrowerId(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
            var response = _unitofWork.borrowerRepositary.GetLenderSendRequest(user.UserId);
            if (response == null)
            {
                return Json(new { Success = false });
            }
            else
            {

                var list = Mapper.Map<List<USP_GetLenderSendRequestRequestdOnTheBasisOfBorrowerId_Result>, List<MaturityModel>>(response);
                return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(list)) });

            }

        }

        [HttpPost]
        public IHttpActionResult UpdateRateOfInterest(ApiRequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            LenderSendRequestModel sendRequestModel = JsonConvert.DeserializeObject<LenderSendRequestModel>(data);
            return Json(new { Success = _unitofWork.borrowerRepositary.BorrowerOfferRateOfInterrestToLenderRequest(sendRequestModel) });
        }
    }
}
