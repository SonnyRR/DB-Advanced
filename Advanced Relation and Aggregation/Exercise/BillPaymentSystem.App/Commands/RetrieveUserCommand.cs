namespace BillPaymentSystem.App.Commands
{
    using System.Text;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    using Commands.Contracts;
    using BillPaymentSystem.Models.Enums;
    using BillPaymentSystem.Data;

    public class RetrieveUserCommand : ICommand
    {

        public string Execute(string[] args, BillPaymentSystemContext context)
        {
            StringBuilder builder = new StringBuilder();

            int desiredId = int.Parse(args[0]);

            var user = context
                .Users
                .Include(x => x.PaymentMethods)
                .FirstOrDefault(x => x.UserId == desiredId);

            if (user != null)
            {
                builder.AppendLine($"User {user.FirstName} {user.LastName}");
                builder.AppendLine($"Bank Accounts:");

                var bankAccs = user.PaymentMethods
                    .Where(x => x.Type == PaymentType.BankAccount)
                    .ToList();

                bankAccs.ForEach(x =>
                {
                    builder.AppendLine($"-- ID: {x.BankAccountId}");
                    builder.AppendLine($"--- Balance: {x.BankAccount.Balance:F2}");
                    builder.AppendLine($"--- Bank: {x.BankAccount.BankName}");
                    builder.AppendLine($"--- SWIFT: {x.BankAccount.SWIFT}");
                });

                builder.AppendLine($"Credit Cards:");

                var cards = user.PaymentMethods
                    .Where(x => x.Type == PaymentType.CreditCard)
                    .ToList();

                cards.ForEach(x =>
                    {
                        builder.AppendLine($"-- ID: {x.CreditCardId}");
                        builder.AppendLine($"--- Limit: {x.CreditCard.Limit:F2}");
                        builder.AppendLine($"--- Money Owed: {x.CreditCard.MoneyOwed:F2}");
                        builder.AppendLine($"--- Limit Left: {x.CreditCard.LimitLeft:F2}");
                        builder.AppendLine($"--- Expiration Date: {x.CreditCard.ExpirationDate.ToString("yyyy/MM")}");
                    });
            }

            else
            {
                builder.AppendLine($"User with id: {desiredId} not found!");
            }

            return builder.ToString();

        }
    }
}
