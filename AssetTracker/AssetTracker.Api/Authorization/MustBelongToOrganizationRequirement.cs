using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Api.Authorization
{
    public class MustBelongToOrganizationRequirement : IAuthorizationRequirement
    {
        public MustBelongToOrganizationRequirement()
        {

        }
    }
}
