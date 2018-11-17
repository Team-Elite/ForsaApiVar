using ForsaWebAPI.Models;
using ForsaWebAPI.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Perrsistance.Repository
{
    public interface ILenderRepositary 
    {
        List<USP_GetMaturityList_Result> GetLenderMaturityList(int userId, bool History);
        List<USP_Lender_GetAllBanksWithStatusIsDeselected_Result> GetAllBankListDeselected(int userId, int pageNumber);
        List<USP_GetPagesForLenderSettingStartPage_Result> GetLenderPages(UserModel userModel);

    }

    public class LenderRepositary : ILenderRepositary, IDisposable
    {
        private readonly forsawebEntities _context;
        public LenderRepositary(forsawebEntities context)
        {
            _context = context;
        }
        public List<USP_Lender_GetAllBanksWithStatusIsDeselected_Result> GetAllBankListDeselected(int userId, int pageNumber)
        {
            return _context.USP_Lender_GetAllBanksWithStatusIsDeselected(userId,pageNumber).ToList();
        }
        public List<USP_GetMaturityList_Result> GetLenderMaturityList(int userId, bool History)
        {
            return _context.USP_GetMaturityList(History, 0, userId).ToList();
        }
        public List<USP_GetPagesForLenderSettingStartPage_Result> GetLenderPages(UserModel userModel)
        {
            return _context.USP_GetPagesForLenderSettingStartPage(userModel.UserId).ToList();
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
        // ~LenderRepositary() {
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