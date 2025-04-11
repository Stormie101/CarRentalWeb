using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarRentalFrontEnd.Models;

namespace CarRentalFrontEnd.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5087/api";


        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> CreateRentalAsync(Rental rental)
        {
            // Serialize the rental object to JSON
            var rentalJson = JsonSerializer.Serialize(rental);

            // Prepare the HTTP content
            var content = new StringContent(rentalJson, Encoding.UTF8, "application/json");

            // Send the POST request to the API
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/booked", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error Content: {errorContent}");
            }
            // Return true if the request was successful
            return response.IsSuccessStatusCode;



        }
    }
}
