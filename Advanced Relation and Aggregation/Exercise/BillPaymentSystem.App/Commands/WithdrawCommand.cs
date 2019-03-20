namespace BillPaymentSystem.App.Commands
{
    using BillPaymentSystem.App.Commands.Contracts;
    using BillPaymentSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WithdrawCommand : ICommand
    {
        public string Execute(string[] args, BillPaymentSystemContext context)
        {
            decimal amount = decimal.Parse(args[1]);
            int id = int.Parse(args[0]);
            var output = "Insufficient funds!";

            var user = context
                .Users
                .Include(x=>x.PaymentMethods)
                .ThenInclude(x=> x.BankAccount)
                .FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                throw new ArgumentException($"User with id: {id} does not exist!");
            }


            var firstFoundAcc = user.PaymentMethods.FirstOrDefault(x => x.BankAccount.Balance >= amount).BankAccount;

            if (firstFoundAcc != null)
            {
                firstFoundAcc.Balance -= amount;
                output = $"Successfuly withdrawed: {amount:f2}";
                context.SaveChanges();
            }

            return output;
        }
    }
}
