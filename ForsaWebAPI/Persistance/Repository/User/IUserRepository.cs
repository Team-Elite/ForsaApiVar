
using ForsaWebAPI.Perrsistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForsaWebAPI.Perrsistance.Repository
{
    public interface IUserRepository
    {

       // USP_ValidateUser_Result Authentication(string userName, string password, string emailId);
         USP_GetUser_Result GetUser(string userName, string password);
        USP_GetUserPassword_Result ForGotPassword(string email);

    }
}
