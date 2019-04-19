using System;

namespace Contracts
{
    public interface ICommand
    {
        Guid CommandId { get; set; }
        DateTime Date { get; set; }
    }
}