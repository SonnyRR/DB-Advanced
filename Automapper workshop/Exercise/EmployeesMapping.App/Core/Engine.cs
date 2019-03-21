namespace EmployeesMapping.App.Core
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.App.Core.Contracts;

    public class Engine : IEngine
    {
        private readonly IServiceProvider _provider;

        public Engine(IServiceProvider services)
        {
            this._provider = services;
        }

        public void Run()
        {
            while (true)
            {
                var input = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var interpreter = this._provider.GetService<ICommandInterpreter>();

                var result = interpreter.Read(input);

                // Better to implement IWriter abstraction in the commands themselves.
                Console.WriteLine(result);
            }
        }
    }
}
