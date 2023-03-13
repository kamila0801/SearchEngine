namespace LoadBalancer.Controllers;

public class LoadBalancer : ILoadBalancer
{
    private ILoadBalancerStrategy _strategy;
    private List<string> _services;

    public LoadBalancer(ILoadBalancerStrategy strategy)
    {
        _strategy = strategy;
        _services = new List<string>();
    }

    public List<string> GetAllServices()
    {
        return _services;
    }

    public int AddService(string url)
    {
        _services.Add(url);
        return _services.Count - 1;
    }
        
    public int RemoveService(int id)
    {
        _services.RemoveAt(id);
        return id;
    }
        
    public ILoadBalancerStrategy GetActiveStrategy()
    {
        return _strategy;
    }

    public void SetActiveStrategy(ILoadBalancerStrategy strategy)
    {
        _strategy = strategy;
    }

    public string NextService()
    {
        return _strategy.NextService(_services);
    }
    
    public void ReleaseServer(string url)
    {
        _strategy.ReleaseServer(url);
    }
}