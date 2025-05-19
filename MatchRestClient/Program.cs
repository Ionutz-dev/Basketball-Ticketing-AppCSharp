using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MatchRestClient
{
    class Program
    {
        static HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
        private static string URL_Base = "http://localhost:8080/basketball/api/matches";

        public static void Main(string[] args)
        {
            RunMenuAsync().Wait();
        }

        static async Task RunMenuAsync()
        {
            client.BaseAddress = new Uri(URL_Base);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Basketball Match REST Client ---");
                Console.WriteLine("1. Get all matches");
                Console.WriteLine("2. Get match by ID");
                Console.WriteLine("3. Create match");
                Console.WriteLine("4. Update match");
                Console.WriteLine("5. Delete match");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid option, please try again.");
                    continue;
                }

                try
                {
                    switch (option)
                    {
                        case 1:
                            await GetAllMatchesAsync();
                            break;
                        case 2:
                            await GetMatchByIdAsync();
                            break;
                        case 3:
                            await CreateMatchAsync();
                            break;
                        case 4:
                            await UpdateMatchAsync();
                            break;
                        case 5:
                            await DeleteMatchAsync();
                            break;
                        case 0:
                            running = false;
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static async Task GetAllMatchesAsync()
        {
            var response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var matches = await response.Content.ReadAsAsync<List<Match>>();
                Console.WriteLine("\n--- All Matches ---");
                foreach (var match in matches)
                {
                    Console.WriteLine(match);
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static async Task GetMatchByIdAsync()
        {
            Console.Write("Enter match ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var response = await client.GetAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                var match = await response.Content.ReadAsAsync<Match>();
                Console.WriteLine("\n--- Match Details ---");
                Console.WriteLine(match);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Match with ID {id} not found.");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static async Task CreateMatchAsync()
        {
            var match = new Match();
            
            Console.Write("Enter Team A: ");
            match.TeamA = Console.ReadLine();
            
            Console.Write("Enter Team B: ");
            match.TeamB = Console.ReadLine();
            
            Console.Write("Enter Ticket Price: ");
            if (double.TryParse(Console.ReadLine(), out double price))
            {
                match.TicketPrice = price;
            }
            else
            {
                Console.WriteLine("Invalid price format. Using default value 0.");
                match.TicketPrice = 0;
            }
            
            Console.Write("Enter Available Seats: ");
            if (int.TryParse(Console.ReadLine(), out int seats))
            {
                match.AvailableSeats = seats;
            }
            else
            {
                Console.WriteLine("Invalid seats format. Using default value 0.");
                match.AvailableSeats = 0;
            }

            var response = await client.PostAsJsonAsync("", match);
            if (response.IsSuccessStatusCode)
            {
                var createdMatch = await response.Content.ReadAsAsync<Match>();
                Console.WriteLine("\n--- Created Match ---");
                Console.WriteLine(createdMatch);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

        static async Task UpdateMatchAsync()
        {
            Console.Write("Enter match ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            // First get current match
            var getResponse = await client.GetAsync($"{id}");
            if (!getResponse.IsSuccessStatusCode)
            {
                if (getResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Match with ID {id} not found.");
                }
                else
                {
                    Console.WriteLine($"Error: {getResponse.StatusCode}");
                }
                return;
            }

            var match = await getResponse.Content.ReadAsAsync<Match>();

            Console.Write($"Enter Team A (leave empty to keep '{match.TeamA}'): ");
            string teamA = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(teamA))
            {
                match.TeamA = teamA;
            }

            Console.Write($"Enter Team B (leave empty to keep '{match.TeamB}'): ");
            string teamB = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(teamB))
            {
                match.TeamB = teamB;
            }

            Console.Write($"Enter Ticket Price (leave empty to keep '{match.TicketPrice}'): ");
            string priceStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceStr) && double.TryParse(priceStr, out double price))
            {
                match.TicketPrice = price;
            }

            Console.Write($"Enter Available Seats (leave empty to keep '{match.AvailableSeats}'): ");
            string seatsStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(seatsStr) && int.TryParse(seatsStr, out int seats))
            {
                match.AvailableSeats = seats;
            }

            var updateResponse = await client.PutAsJsonAsync($"{id}", match);
            if (updateResponse.IsSuccessStatusCode)
            {
                var updatedMatch = await updateResponse.Content.ReadAsAsync<Match>();
                Console.WriteLine("\n--- Updated Match ---");
                Console.WriteLine(updatedMatch);
            }
            else
            {
                Console.WriteLine($"Error: {updateResponse.StatusCode}");
                var content = await updateResponse.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

        static async Task DeleteMatchAsync()
        {
            Console.Write("Enter match ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var response = await client.DeleteAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Match with ID {id} deleted successfully!");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Match with ID {id} not found.");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
    }

    public class Match
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("teamA")] public string TeamA { get; set; }
        [JsonProperty("teamB")] public string TeamB { get; set; }
        [JsonProperty("ticketPrice")] public double TicketPrice { get; set; }
        [JsonProperty("availableSeats")] public int AvailableSeats { get; set; }

        public override string ToString()
        {
            return $"Match{{id={Id}, {TeamA} vs {TeamB}, price={TicketPrice:F2}, seats={AvailableSeats}}}";
        }
    }

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }

            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            Console.WriteLine();

            return response;
        }
    }
}