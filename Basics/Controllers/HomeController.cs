using Basics.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Authenticate()
        {
            var granmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Name,"BOB@fmail.com"),
                new Claim("Grandma.Says","Very nice boy"),
            };
            var licenseClaims=new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob K foo"),
                new Claim("DrivingLicense","A+"),
            };

            var grandmaIdentity = new ClaimsIdentity(granmaClaims,"Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Goverment");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity,licenseIdentity });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}