namespace LoadBalancer.Controllers;

public class RoundRubinStrategy : ILoadBalancerStrategy
{
    private int _count;

    public RoundRubinStrategy()
    {
        _count = 0;
    }
    
    public string NextService(List<string> services)
    {
        var serviceToUse = services[_count];
        _count += 1;
        if (_count == services.Count) _count = 0;
        return serviceToUse;
    }

    public void ReleaseServer(string url)
    {
        //do nothing
    }
}