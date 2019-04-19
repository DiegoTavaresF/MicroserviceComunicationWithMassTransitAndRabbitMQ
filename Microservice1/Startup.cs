using Contracts.Commands.Bets;
using GreenPipes;
using MassTransit;
using Microservice1.Buss;
using Microservice1.Consumers.Bets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microservice1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<CreateBetConsumer>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateBetConsumer>();

                
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "create_bet", e =>
                {
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(mr => mr.Interval(2, 100));
                   // e.PurgeOnStartup = true;
                    e.Consumer<CreateBetConsumer>(provider);
                });
            }));

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<ICreateBetCommand>());
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();
        }
    }
}