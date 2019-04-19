using Contracts.Commands.Bets;
using MassTransit;
using System.Threading.Tasks;

namespace Microservice1.Consumers.Bets
{
    public class CreateBetConsumer : IConsumer<ICreateBetCommand>
    {
        public async Task Consume(ConsumeContext<ICreateBetCommand> context)
        {
            
            var teste = context.Message.SomeProperty;

            //await context.RespondAsync<BetCreated>(new
            //{
            //    Value = $"Received: {context.Message.Value}"
            //})
        }
    }
}