using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ForsaWebAPI.Persistance;
using ForsaWebAPI.Persistance.Data;


namespace ForsaWebAPI.Perrsistance.Repository
{
    public class UserRepositary : IUserRepository
    {
        public IUnitOfWork _UnitOfWork { get; set; }
        public ForsaEntities _dbContext { get; set; }
        public UserRepositary(ForsaEntities dbContext)
        {
            _dbContext = dbContext;
        }



        public  USP_GetUser_Result GetUser(string userName, string password)
        {
            using (_UnitOfWork = new UnitOfWork(_dbContext))
            {
                var result = _dbContext.USP_GetUser(userName, password);
                if (result != null) return result.FirstOrDefault();
                return  null;
            }
        }

        public USP_GetUserPassword_Result ForGotPassword(string email)
        {
            using (_UnitOfWork = new UnitOfWork(_dbContext))
            {
                var result = _dbContext.USP_GetUserPassword(email);
                if (result != null) return result.FirstOrDefault();
                return null;
            }
        }
    }




}