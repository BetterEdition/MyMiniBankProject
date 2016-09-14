using System;

namespace Interfaces
{
    public interface ITransaction
    {
        int Id { get; }
        DateTime DateTime { get; }
        String Message { get; }
        double Amount { get; }
    }
}