using ForsaWebAPI.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Persistance.Repository
{
    public interface IBorrowerRepositary
    {
        List<USP_GetMaturityList_Result> GetBorrowerMaturityList(int iUserId, bool History);
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