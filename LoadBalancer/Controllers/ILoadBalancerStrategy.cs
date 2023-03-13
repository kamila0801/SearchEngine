namespace LoadBalancer.Controllers;

public interface ILoadBalancerStrategy
{
    public string NextService(List<string> services);
    public void ReleaseServer(string url);
}