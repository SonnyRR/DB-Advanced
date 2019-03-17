namespace BillPaymentSystem.App.Commands
{
    using System;
    using System.Linq;
    using System.Reflection;

    using BillPaymentSystem.App.Commands.Contracts;
    using BillPaymentSystem.Data;

    public class CommandInterpretator
    {
        public string ReadCommand(string[] args, BillPaymentSystemContext context)
        {
            var commandName = args[0];

            var command = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == $"{commandName}Command");

            var instance = (ICommand)Activator.CreateInstance(command);
            var output = instance.Execute(args.Skip(1).ToArray(), context);

            return output;
        }
    }
}
