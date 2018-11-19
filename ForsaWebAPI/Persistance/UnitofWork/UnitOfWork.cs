using ForsaWebAPI.Perrsistance.Data;
using ForsaWebAPI.Perrsistance.Repository;
using ForsaWebAPI.Perrsistance.Repository.Lender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ForsaWebAPI.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
       // IUserRepository UserRepository { get; }
        ILenderRepositary lenderRepositary { get; }
        IBorrowerRepositary borrowerRepositary { get}
        Task SaveChangesAsync();

        //Task<IList<T>> GetList();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private ForsaEntities _dbContext { get; set; }

       // public IUserRepository UserRepository { get { return new UserRepositary(_dbContext); } }

        public ILenderRepositary lenderRepositary { get { return new LenderRepositary(_dbContext); } }

        public UnitOfWork(ForsaEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public UnitOfWork()
        {
        }

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext = null;
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
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