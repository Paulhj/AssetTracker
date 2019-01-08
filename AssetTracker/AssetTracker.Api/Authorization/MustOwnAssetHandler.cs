using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using AssetTracker.Core.Services;

namespace AssetTracker.Api.Authorization
{
    public class MustOwnAssetHandler : AuthorizationHandler<MustOwnAssetRequirement>
    {
        private readonly IAssetService _assetService;
        private readonly IUserService _userService;

        public MustOwnAssetHandler(IAssetService assetService,
            IUserService userService)
        {
            _assetService = assetService;
            _userService = userService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MustOwnAssetRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var assetId = filterContext.RouteData.Values["id"].ToString();

            if (Convert.ToInt32(assetId) == 0)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var userId = context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var organizationId = _userService.GetById(Convert.ToInt32(userId)).SelectedOrganizationId;

            if (!_assetService.IsAssetOwner(assetId, organizationId))
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
