using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Services.AppServices
{
    public class UserService : IUser
    {
        IEnumerable<Claim> IUser.GetClaimsIdentity()
        {
            throw new NotImplementedException();
        }

        string IUser.GetToken()
        {
            throw new NotImplementedException();
        }

        EUserType IUser.GetUserType()
        {
            throw new NotImplementedException();
        }

        long IUser.GetUsrId()
        {
            throw new NotImplementedException();
        }

        bool IUser.IsAuthenticated()
        {
            throw new NotImplementedException();
        }
    }
}
