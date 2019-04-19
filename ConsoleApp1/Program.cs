using Contracts.Commands.Bets;
using MassTransit;
using System;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main()
        {
            var busControl = ConfigureBus();

            busControl.Start(); 

            do
            {
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;

                busControl.Publish<ICreateBetCommand>(new
                {
                    SomeProperty = value
                });
            }
            while (true);

            busControl.Stop();
        }

        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }
    }
}
