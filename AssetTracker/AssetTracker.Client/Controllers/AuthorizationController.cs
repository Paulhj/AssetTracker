using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Client.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
