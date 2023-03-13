namespace LoadBalancer.Controllers;

public class LeastConnectionsStrategy : ILoadBalancerStrategy
{
    private List<Server> _servers = new List<Server>();

    public LeastConnectionsStrategy(List<string> services)
    {
        foreach(string service in services)
        {
            int index = 0;
            _servers.Add(new Server(){ Id = index, Connections = 0, Url = service});
            index++;
        }
    }
    
    public string NextService(List<string> services)
    {
        return _servers.Find(s => s.Id == ChooseServer()).Url;
    }
    
    private int ChooseServer()
    {
        int minConnections = int.MaxValue;
        List<int> candidates = new List<int>();

        foreach (var server in _servers)
        {
            if (server.Connections < minConnections)
            {
                minConnections = server.Connections;
                candidates.Clear();
            }

            if (server.Connections == minConnections)
            {
                candidates.Add(server.Id);
            }
        }

        if (candidates.Count == 0)
        {
            throw new Exception("No servers available.");
        }

        int index = new Random().Next(candidates.Count);
        int chosenServerId = candidates[index];

        foreach (var server in _servers)
        {
            if (server.Id == chosenServerId)
            {
                server.Connections++;
                break;
            }
        }

        return chosenServerId;
    }
    
    public void ReleaseServer(string url)
    {
        foreach (var server in _servers)
        {
            if (server.Url == url)
            {
                server.Connections--;
                break;
            }
        }
    }
}

public class Server
{
    public int Id { get; set; }
    public int Connections { get; set; }
    public string Url { get; set; }
}