using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;

namespace github_activity
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please provide a GitHub username as an argument.");
                return;
            }

            string username = args[0];
            await getRepos(username);
        }

        public static async Task getRepos(string username)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "github-tracker-cli");

            try
            {
                HttpResponseMessage respone = await client.GetAsync($"https://api.github.com/users/{username}/events");

                if (respone.IsSuccessStatusCode)
                {
                    string json = await respone.Content.ReadAsStringAsync();

                    if(string.IsNullOrWhiteSpace(json))
                    {
                        Console.WriteLine("No data found for the given username.");
                        return;
                    }

                    using JsonDocument doc = JsonDocument.Parse(json);
                    string formattedJson = JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });

                    Console.WriteLine(formattedJson);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Network Error: {e.Message}");
            }
            catch(jsonException e)
            {
                Console.WriteLine($"JSON Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected Error: {e.Message}");
            }
        }
    }
}
