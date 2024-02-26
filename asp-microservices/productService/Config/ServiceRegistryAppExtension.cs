

using Consul;
using Microsoft.Extensions.Options;

namespace Config {
    public static class ServiceRegistryAppExtension {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration Configuration) {
            services.Configure<ConsulConfig>(Configuration.GetSection("Configuration"));
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig => {
                var address = Configuration["Configuration:ConsulAddress"];
                consulConfig.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app){
                // Retrieve Consul client from DI
                var consulClient = app.ApplicationServices
                                    .GetRequiredService<IConsulClient>();
                var consulConfig = app.ApplicationServices
                                    .GetRequiredService<IOptions<ConsulConfig>>();
                // Setup logger
                var loggingFactory = app.ApplicationServices
                                    .GetRequiredService<ILoggerFactory>();
                var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

                var lifetime = app.ApplicationServices
                                    .GetRequiredService<Microsoft.Extensions.Hosting.IApplicationLifetime>();

                // Register service with consul
                // var uri = new Uri(address);
                var registration = new AgentServiceRegistration()
                {
                    ID = consulConfig.Value.ServiceName, // uri.Port
                    Name = consulConfig.Value.ServiceName, 
                    Address = consulConfig.Value.ServiceHost, // $"{uri.Host}"
                    Port = consulConfig.Value.ServicePort, // uri.Port
                    Tags = new[] { "Product" }
                };

                logger.LogInformation("Registering with Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                consulClient.Agent.ServiceRegister(registration).Wait();

                lifetime.ApplicationStopping.Register(() => {
                    logger.LogInformation("Deregistering from Consul");
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait(); 
                });

                return app;
        }
    }
}
           