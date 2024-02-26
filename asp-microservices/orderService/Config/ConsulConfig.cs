public class ConsulConfig
{
    public const string Configuration = "Configuration";

    public string ServiceName { get; set; } = String.Empty;
    public string ServiceHost { get; set; } = String.Empty;
    public int ServicePort { get; set; }
    public string ConsulAddress { get; set; } = String.Empty;
   
}