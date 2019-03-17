namespace BillPaymentSystem.App.Core
{
    using System;
    using System.Collections.Generic;
    using BillPaymentSystem.App.Commands;
    using BillPaymentSystem.Data;
    using Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public class Engine : IEngine
    {
        private readonly CommandInterpretator _cmdInterpretator;

        public Engine()
        {
            this._cmdInterpretator = new CommandInterpretator();
        }

        public void Run()
        {
            while (true)
            {
                string[] inputArgs = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    using (BillPaymentSystemContext context = new BillPaymentSystemContext())
                    {
                        var output = this._cmdInterpretator.ReadCommand(inputArgs, context);
                        Console.WriteLine(output);
                    }
                }

                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
