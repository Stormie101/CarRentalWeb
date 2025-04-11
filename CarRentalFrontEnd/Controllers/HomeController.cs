using CarRentalFrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CancelBooking(int id)
        {
            var booking = _context.Booked.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                _context.Booked.Remove(booking);
                _context.SaveChanges();
            }

            // Redirect back to the Booked page
            return RedirectToAction("Booked");
        }


        public IActionResult Booked()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }
            // Fetch all records from the Booked table
            var bookings = _context.Booked.ToList();
            return View(bookings); // Pass the data to the view
        }

        public IActionResult BrowseCars()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        public IActionResult Index()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }

            //ViewBag.Role = HttpContext.Session.GetString("Role");
            return View();
        }


        public IActionResult About()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult FeedbackThankYou()
        {

            return View();
        }

        public IActionResult Feedback()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }
            return View();
        }
        //[HttpPost] for testing feedback only!
        //public IActionResult SubmitFeedback(string OverallSatisfaction, string CarCondition, string PickupProcess, string DropoffProcess, string CustomerService, string AdditionalFeedback)
        //{
        //    // Log the feedback (for now, we'll just simulate saving it)
        //    Console.WriteLine($"Overall Satisfaction: {OverallSatisfaction}");
        //    Console.WriteLine($"Car Condition: {CarCondition}");
        //    Console.WriteLine($"Pickup Process: {PickupProcess}");
        //    Console.WriteLine($"Drop-Off Process: {DropoffProcess}");
        //    Console.WriteLine($"Customer Service: {CustomerService}");
        //    Console.WriteLine($"Additional Feedback: {AdditionalFeedback}");

        //    // Redirect to a thank-you page
        //    return RedirectToAction("FeedbackThankYou");
        //}
        [HttpPost]
        public IActionResult SubmitFeedback(string OverallSatisfaction, string CarCondition, string PickupProcess, string DropoffProcess, string CustomerService, string AdditionalFeedback)
        {
            var feedback = new Feedback
            {
                OverallSatisfaction = OverallSatisfaction,
                CarCondition = CarCondition,
                PickupProcess = PickupProcess,
                DropoffProcess = DropoffProcess,
                CustomerService = CustomerService,
                AdditionalFeedback = AdditionalFeedback
            };

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            return RedirectToAction("FeedbackThankYou");
        }

        public IActionResult Policy()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }
            return View();
        }

        public IActionResult Manage()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }

            // Retrieve all users
            var users = _context.Users.ToList();

            // Retrieve all cars
            var cars = _context.Cars.ToList();

            // Retrieve all bookings
            var bookings = _context.Booked.ToList();

            // Pass data to the view using ViewBag
            ViewBag.Users = users;
            ViewBag.Cars = cars;
            ViewBag.Bookings = bookings;

            return View();
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            // Redirect back to the Manage page
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public IActionResult DeleteCar(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }

            // Redirect back to the Manage page
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Booked.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                _context.Booked.Remove(booking);
                _context.SaveChanges();
            }

            // Redirect back to the Manage page
            return RedirectToAction("Manage");
        }

        public IActionResult AddUserPage()
        {
            return View("AddUserPage");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitUser(string Username, string Email, string Password, string Role)
        {
            try
            {
                // Create a new User object
                var newUser = new User
                {
                    FullName = Username,
                    Email = Email,
                    Password = Password,
                    Role = Role // Save the selected role
                };

                // Save the user to the database
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                TempData["Message"] = "User added successfully!";
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                ViewBag.ErrorMessage = "Failed to add user. Please try again.";
                return View("AddUserPage");
            }
        }

        public IActionResult AddCarPage()
        {
            return View("AddCarPage");
        }


        [HttpPost]
        public async Task<IActionResult> SubmitCar(string Model, string Brand, decimal PricePerDay, string Type, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile == null || ImageFile.Length == 0)
                {
                    ViewBag.ErrorMessage = "Please upload an image for the car.";
                    return View("AddCarPage");
                }

                // Set the upload path based on your directory structure
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "cars");

                // Create the folder if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique filename and save the file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Create the new Car object with Availability automatically set to 1 (Available)
                var newCar = new Car
                {
                    Model = Model,
                    Brand = Brand,
                    PricePerDay = PricePerDay,
                    Type = Type,
                    Availability = 1,
                    ImagePath = "cars/" + fileName  // Use the "image" folder as specified in your project
                };

                // Save the car to the database
                _context.Cars.Add(newCar);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Car added successfully!";
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding car: {ex.Message}");
                ViewBag.ErrorMessage = "Failed to add car. Please try again.";
                return View("AddCarPage");
            }
        }


        public IActionResult AddBookingPage()
        {
            // Retrieve the list of cars from the database
            var cars = _context.Cars.ToList();
            ViewBag.Cars = cars;
            return View("AddBookingPage");
        }


        [HttpPost]
        public async Task<IActionResult> SubmitBooking(int CarId, DateTime PickupDate, DateTime DropoffDate)
        {
            try
            {
                // Retrieve the selected car to get additional details (e.g., Brand and Model)
                var car = _context.Cars.FirstOrDefault(c => c.Id == CarId);
                if (car == null)
                {
                    TempData["ErrorMessage"] = "Selected car not found.";
                    return RedirectToAction("AddBookingPage");
                }

                // Create a new booking record using the Booked model
                var newBooked = new Booked
                {
                    CarId = CarId,
                    CarName = $"{car.Brand} {car.Model}",
                    PickupDate = PickupDate,
                    DropoffDate = DropoffDate
                };

                _context.Booked.Add(newBooked);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Booking added successfully!";
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding booking: {ex.Message}");
                TempData["ErrorMessage"] = "Failed to add booking. Please try again.";
                return RedirectToAction("AddBookingPage");
            }
        }



    }
}
