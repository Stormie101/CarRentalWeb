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
        // Check if the user is already logged in
        if (HttpContext.Session.GetString("IsLoggedIn") == "true")
        {
            Console.WriteLine("User already logged in. Redirecting to Manage page.");
            return RedirectToAction("Manage", "Home");
        }

        // Show the login page if not logged in
        return View();
    }


    [HttpPost]
    public IActionResult Login(string loginEmail, string loginPassword)
    {
        Console.WriteLine($"Attempting login with Email: {loginEmail}, Password: {loginPassword}");

        // Normalize input
        loginEmail = loginEmail.Trim().ToLower();

        // Query the database
        var user = _context.Users
            .AsEnumerable()
            .SingleOrDefault(u => string.Equals(u.Email, loginEmail, StringComparison.OrdinalIgnoreCase));

        if (user != null)
        {
            Console.WriteLine($"User found: {user.FullName}");

            if (user.Password == loginPassword)
            {
                // Use null-coalescing operator to provide a default if user.Role or user.FullName is null.
                HttpContext.Session.SetString("UserRole", user.Role ?? "");
                HttpContext.Session.SetString("IsLoggedIn", "true"); // Set session flag
                HttpContext.Session.SetString("UserName", user.FullName ?? "");

                Console.WriteLine($"Password matches for user: {user.FullName}");
                TempData["Message"] = $"Welcome, {user.FullName}!";
                return RedirectToAction("Index", "Home");
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
        return View("Login");
    }

    public IActionResult Logout()
    {
        // Clear the session data
        HttpContext.Session.Clear();
        // Redirect the user to the Login page (or any other page as needed)
        return RedirectToAction("Login", "Account");
    }



    public IActionResult Index()
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
    //[HttpPost]
    //public IActionResult Login(string loginEmail, string loginPassword)
    //{
    //    Console.WriteLine($"Attempting login with Email: {loginEmail}, Password: {loginPassword}");

    //    // Normalize input
    //    loginEmail = loginEmail.Trim().ToLower();

    //    // Query the database
    //    // Query the database
    //    var user = _context.Users
    //    .AsEnumerable()
    //    .SingleOrDefault(u => string.Equals(u.Email, loginEmail.Trim(), StringComparison.OrdinalIgnoreCase));

    //    if (user != null)
    //    {
    //        Console.WriteLine($"User found: {user.FullName}");
    //        if (user.Password == loginPassword)
    //        {
    //            Console.WriteLine($"Password matches for user: {user.FullName}");
    //            Console.WriteLine($"Role : {user.Role}");
    //            TempData["Message"] = $"Welcome, {user.FullName}!";
    //            Console.WriteLine("Redirecting to Home/Index");
    //            return RedirectToAction("Index", "Home"); // Redirection logic
    //        }
    //        else
    //        {
    //            Console.WriteLine("Password does not match.");
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("No matching user found in database.");
    //    }

    //    ViewBag.ErrorMessage = "Invalid email or password.";
    //    Console.WriteLine("Staying on Login page due to invalid login.");
    //    return View("Login");
    //}



}
