using CarRentalFrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    //[HttpPost] ONLY USE THIS TO TEST FORM FUNCTION
    //public IActionResult Login(string loginEmail, string loginPassword)
    //{
    //    // Debugging logs to track input and logic flow
    //    Console.WriteLine($"Email: {loginEmail}, Password: {loginPassword}");

    //    if (loginEmail == "test@example.com" && loginPassword == "password123")
    //    {
    //        Console.WriteLine("Hardcoded login successful.");
    //        TempData["Message"] = "Login Successful!";
    //        return RedirectToAction("Index", "Home");
    //    }

    //    Console.WriteLine("Hardcoded login failed.");
    //    ViewBag.ErrorMessage = "Invalid email or password.";
    //    return View();
    //}
    [HttpPost]
    public IActionResult Login(string loginEmail, string loginPassword)
    {
        Console.WriteLine($"Attempting login with Email: {loginEmail}, Password: {loginPassword}");

        // Normalize input
        loginEmail = loginEmail.Trim().ToLower();

        // Query the database
        // Query the database
        var user = _context.Users
        .AsEnumerable()
        .SingleOrDefault(u => string.Equals(u.Email, loginEmail.Trim(), StringComparison.OrdinalIgnoreCase));

        if (user != null)
        {
            Console.WriteLine($"User found: {user.FullName}");
            if (user.Password == loginPassword)
            {
                Console.WriteLine($"Password matches for user: {user.FullName}");
                TempData["Message"] = $"Welcome, {user.FullName}!";
                Console.WriteLine("Redirecting to Home/Index");
                return RedirectToAction("Index", "Home"); // Redirection logic
            }
            else
            {
                Console.WriteLine("Password does not match.");
            }
        }
        else
        {
            Console.WriteLine("No matching user found in database.");
        }

        ViewBag.ErrorMessage = "Invalid email or password.";
        Console.WriteLine("Staying on Login page due to invalid login.");
        return View("Login");
    }



}
