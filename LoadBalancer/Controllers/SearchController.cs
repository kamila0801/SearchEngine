using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoadBalancer.Controllers
{
    [Route("Search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ILoadBalancer _balancer;

        public SearchController(ILoadBalancer balancer, ILoadBalancerStrategy strategy)
        {
            _balancer = balancer;
            _balancer.AddService("http://api-1");
            _balancer.AddService("http://api-2");
            _balancer.AddService("http://api-3");
            _balancer.SetActiveStrategy(strategy);
        }
        
        [HttpGet]
        public async Task<SearchResult> Search(string terms, int numberOfResults)
        {
            HttpClient api = new HttpClient();
            api.BaseAddress = new Uri(_balancer.NextService());
            Console.WriteLine("SERVICE USED: " + api.BaseAddress);
            Task<string> task = api.GetStringAsync("/Search?terms=" + terms + "&numberOfResults=" + numberOfResults);
            task.Wait();
            string resultString = task.Result;
            SearchResult result = JsonConvert.DeserializeObject<SearchResult>(resultString);
            _balancer.ReleaseServer(api.BaseAddress.ToString());
            return result;
        }

    }
}
