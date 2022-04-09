﻿using System.Collections.Generic;
using System.Security.Claims;

namespace Lear.CRS.Common.HttpContextUser
{
    public interface IUser
    {
        string Name { get; }
       
        long ID { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);

        string GetToken();
        List<string> GetUserInfoFromToken(string ClaimType);
    }
}
