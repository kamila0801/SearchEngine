using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace ConsoleSearch
{
    public class App
    {
        public void Run()
        {
            HttpClient api = new HttpClient();
            api.BaseAddress = new Uri("http://localhost:5193");
            //SearchLogic mSearchLogic = new SearchLogic(new Database());
            Console.WriteLine("Console Search");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("q")) break;

                Task<string> task = api.GetStringAsync("/Search?terms=" + input + "&numberOfResults=10");
                task.Wait();
                string resultString = task.Result;
                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(resultString);

                foreach (var ignore in result.IgnoredTerms)
                {
                    Console.WriteLine("Ignored: " + ignore);
                }

                foreach (var resultDoc in result.Documents)
                {
                    Console.WriteLine(resultDoc.Id + ": " + resultDoc.Path + " - number of terms found: " + resultDoc.NumberOfApearances);
                }
                
                Console.WriteLine("Found " + result.Documents.Count + " in " + result.ElapsedMilliseconds + "ms");
            }
        }
    }
}
