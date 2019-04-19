namespace Contracts.Commands.Bets
{
    public interface ICreateBetCommand : ICommand
    {
        string SomeProperty { get; set; }
    }
}