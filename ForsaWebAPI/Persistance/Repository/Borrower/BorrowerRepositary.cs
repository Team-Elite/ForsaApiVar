using ForsaWebAPI.Models;
using ForsaWebAPI.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Perrsistance.Repository
{
    public interface IBorrowerRepositary
    {
        List<USP_GetMaturityList_Result> GetBorrowerMaturityList(int iUserId, bool History);
        List<USP_GetRateOfInterestOfBank_Result> GetRateOfInterestOfBank(int userId);
        List<USP_GetUserGroupForSettingRateOfInterestVisibility_Result> GetUserGroup(int userId);

        List<USP_GetLenderSendRequestRequestdOnTheBasisOfBorrowerId_Result> GetLenderSendRequest(int userId);
        
       bool VisibleBankRateToGroups(RateOfInterestOfBankModel rateOfInterestOfBank);
        bool PublishAndUnPublish(RateOfInterestOfBankModel rateOfInterestOfBank);
      bool  UpdateRateOfInterestOfBank(RateOfInterestOfBankModel rateOfInterestOfBank);
        bool BorrowerOfferRateOfInterrestToLenderRequest(LenderSendRequestModel sendRequestModel);
    }

    public class BorrowerRepositary : IBorrowerRepositary, IDisposable
    {
        private readonly ForsaEntities _context;
        public BorrowerRepositary(ForsaEntities context)
        {
            _context = context;
        }

        public List<USP_GetMaturityList_Result> GetBorrowerMaturityList(int iUserId, bool History)
        {
            return _context.USP_GetMaturityList(History, iUserId, 0).ToList();
        }
        public List<USP_GetRateOfInterestOfBank_Result> GetRateOfInterestOfBank(int userId)
        {
            return _context.USP_GetRateOfInterestOfBank(userId).ToList(); ;
        }

        public List<USP_GetUserGroupForSettingRateOfInterestVisibility_Result> GetUserGroup(int userId)
        {
            return _context.USP_GetUserGroupForSettingRateOfInterestVisibility(userId).ToList(); ;
        }
        public bool PublishAndUnPublish(RateOfInterestOfBankModel rateOfInterestOfBank)
        {
            return (_context.USP_PublishAndUnPublish(rateOfInterestOfBank.UserId, rateOfInterestOfBank.IsPublished)>=1);
        }
        public bool UpdateRateOfInterestOfBank(RateOfInterestOfBankModel rateOfInterestOfBank)
        {
            return (_context.USP_UpdateRateOfInterestOfBank(rateOfInterestOfBank.UserId, rateOfInterestOfBank.RateOfInterest, rateOfInterestOfBank.ModifiedBy) >= 1);
        }

        public List<USP_GetLenderSendRequestRequestdOnTheBasisOfBorrowerId_Result> GetLenderSendRequest(int userId)
        {
            return _context.USP_GetLenderSendRequestRequestdOnTheBasisOfBorrowerId(userId).ToList();
        }
        public bool VisibleBankRateToGroups(RateOfInterestOfBankModel rateOfInterestOfBank)
        {
            return (_context.USP_UpdateUserGroupAgainstBankWhomRateOfInterestWillBeVisible(rateOfInterestOfBank.UserId, rateOfInterestOfBank.GroupIds) >= 1);
        }
        public bool BorrowerOfferRateOfInterrestToLenderRequest(LenderSendRequestModel sendRequestModel)
        {
            return (_context.USP_LenderSendRequest_UpdateRateOfInterest(sendRequestModel.RateOfInterest, sendRequestModel.RequestId) >= 1);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BorrowerRepositary() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

      










        #endregion
    }
}