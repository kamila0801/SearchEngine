namespace LoadBalancer.Controllers;

public interface ILoadBalancer
{
    public List<string> GetAllServices();
    public int AddService(string url);
    public int RemoveService(int id);
    public ILoadBalancerStrategy GetActiveStrategy();
    public void SetActiveStrategy(ILoadBalancerStrategy strategy);
    public string NextService();
    public void ReleaseServer(string url);
}