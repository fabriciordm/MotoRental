using Motorcycle.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Interfaces.Commons
{
    public interface IUser
    {
        string GetToken();
        long GetUsrId();
      
        EUserType GetUserType();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
