using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        // Serve the Login view without any backend logic
        return View();
    }

    public IActionResult SignUp()
    {
        // Serve the Sign-Up view without any backend logic
        return View();
    }
}
