namespace EmployeesMapping.App.Commands.Contracts
{
    using System;

    public interface ICommandInterpreter
    {
        string Read(string[] args);
    }
}
