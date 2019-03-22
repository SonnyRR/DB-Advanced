namespace EmployeesMapping.App.Commands
{
    using EmployeesMapping.App.Commands.Contracts;
    using System;

    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
