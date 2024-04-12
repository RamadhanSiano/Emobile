using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;


namespace EMobile.Controllers
{
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password, string fingerprint)
    {
        // Placeholder logic for authentication
        if (AuthenticateWithUsernamePassword(username, password) || AuthenticateWithFingerprint(fingerprint))
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "cookie");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("cookie", principal);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.Error = "Invalid username, password, or fingerprint";
            return View();
        }
    }

    private bool AuthenticateWithUsernamePassword(string username, string password)
    {
        // Placeholder logic for username/password authentication
        return (username == "user" && password == "password");
    }

    private bool AuthenticateWithFingerprint(string fingerprint)
    {
        // Placeholder logic for fingerprint authentication
        // You would integrate this with a biometric authentication library or service
        // For demonstration, simply return true if fingerprint is provided
        return !string.IsNullOrEmpty(fingerprint);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("cookie");
        return RedirectToAction("Index", "Home");
    }
}

}
