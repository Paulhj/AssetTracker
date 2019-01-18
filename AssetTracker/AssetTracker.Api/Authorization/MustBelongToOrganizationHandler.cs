using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using AssetTracker.Core.Services;

namespace AssetTracker.Api.Authorization
{
    public class MustBelongToOrganizationHandler : AuthorizationHandler<MustBelongToOrganizationRequirement>
    {
        private readonly IAssetService _assetService;
        private readonly IUserService _userService;

        public MustBelongToOrganizationHandler(IAssetService assetService,
            IUserService userService)
        {
            _assetService = assetService;
            _userService = userService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MustBelongToOrganizationRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var organizationId = Convert.ToInt32(filterContext.RouteData.Values["id"].ToString());

            if (Convert.ToInt32(organizationId) == 0)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var userId = context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;

            if (!_userService.UserBelongToOrganization(Convert.ToInt32(userId), organizationId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // all checks out
            context.Succeed(requirement);
            return Task.CompletedTask;

        }
    }
}
