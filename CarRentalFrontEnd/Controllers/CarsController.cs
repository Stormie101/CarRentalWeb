using CarRentalFrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CarRentalFrontEnd.Services;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApiService _apiService;

        public CarsController(ApplicationDbContext context)
        {
            _context = context; // Initialize ApplicationDbContext first
            _apiService = new ApiService(); // Then initialize ApiService
        }


        //public IActionResult Index()
        //{
        //    var cars = _context.Cars.Where(c => c.Availability).ToList();
        //    return View(cars);
        //}
        [HttpGet]
        public IActionResult Rent(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View("Rent", car); // Specifies the view file name 'Rent.cshtml'
        }




        [HttpPost]
        public IActionResult RentalHistory()
        {
            var rentals = _context.Rentals.ToList();
            return View(rentals); 
        }



        public IActionResult Index()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page
            }
            var cars = _context.Cars.ToList(); // Fetch all cars without filtering
            return View(cars);
        }





        //// Handle form submission
        //[HttpPost]
        //public IActionResult SubmitRental(string PickupLocation, string DropoffLocation, DateTime PickupDate, int RentalDays, decimal TotalPayment, int CarId)
        //{
        //    // Fetch car details by CarId
        //    var car = _context.Cars.FirstOrDefault(c => c.Id == CarId);
        //    if (car == null)
        //    {
        //        return NotFound(); // Handle case where car is not found
        //    }

        //    // Create a new Booked object and populate it
        //    var booked = new Booked
        //    {
        //        CarName = car.Model,
        //        PickupDate = PickupDate,
        //        DropoffDate = PickupDate.AddDays(RentalDays),
        //        TotalPrice = TotalPayment,
        //        CarImage = car.ImagePath // Use the car's image path for display
        //    };

        //    // Add the new booking to the Booked table
        //    _context.Booked.Add(booked);
        //    _context.SaveChanges();

        //    // Redirect to the Booked page
        //    return RedirectToAction("Booked", "Home");
        //}

        [HttpPost]
        public async Task<IActionResult> SubmitRental(string PickupLocation, string DropoffLocation, DateTime PickupDate, int RentalDays, int CarId, string CarName, string CarImage, decimal PricePerDay)
        {
            try
            {
                // Calculate dropoff date and total price
                var dropoffDate = PickupDate.AddDays(RentalDays);
                var totalPrice = RentalDays * PricePerDay;

                // Create a new booking record
                var booked = new Booked
                {
                    CarId = CarId,
                    CarName = CarName,
                    CarImage = CarImage,
                    PickupLocation = PickupLocation,
                    DropoffLocation = DropoffLocation,
                    PickupDate = PickupDate,
                    DropoffDate = dropoffDate,
                    TotalPrice = totalPrice
                };

                // Save the record to the database
                _context.Booked.Add(booked);
                await _context.SaveChangesAsync();

                // Redirect to a success page
                TempData["Message"] = "Rental confirmed successfully!";
                return RedirectToAction("RentalSuccess");
            }
            catch (Exception ex)
            {
                // Handle errors and display a message
                Console.WriteLine($"Error saving booking: {ex.Message}");
                ViewBag.ErrorMessage = "Failed to confirm rental. Please try again.";
                return View("Rent"); // Redirect back to the form
            }
        }





        // Show confirmation page
        public IActionResult RentalSuccess()
        {
            return View();
        }
    }
}
