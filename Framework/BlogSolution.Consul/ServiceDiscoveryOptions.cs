namespace BlogSolution.Consul
{
    public class ServiceDiscoveryOptions
    {
        public string ServiceName { get; set; }

        public bool Enabled { get; set; }

        public ConsulOptions Consul { get; set; }

        public string[] HealthCheckTemplates { get; set; }

        public string[] Endpoints { get; set; }
    }

    public class ConsulOptions
    {
        public string HttpEndpoint { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }        
    }
}
