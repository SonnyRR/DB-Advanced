namespace BillPaymentSystem.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using BillPaymentSystem.Data;
    using BillPaymentSystem.Models;
    using BillPaymentSystem.Models.Enums;

    public class DbInitializer
    {
        private Random _rng;
        private BillPaymentSystemContext _context;

        public DbInitializer(BillPaymentSystemContext context)
        {
            this._rng = new Random();
            this._context = context;
        }

        public void Seed()
        {
            using (this._context)
            {

                //SeedUsers(this._context);
                //SeedCreditCards(this._context, this._rng);
                //SeedBankAccounts(this._context, this._rng);
                SeedPaymentMethods(this._context, this._rng);

            }
        }

        private void SeedUsers(BillPaymentSystemContext context)
        {
            ICollection<User> users = new List<User>();

            string[] firstNames = new[]
            {
                    "Lilly",
                    "Mirela",
                    "Antonia",
                    "Cvetan",
                    "Pesho",
                    "Gosho",
                    null,
                    "",
                    "Vyara",
                    "Maria"
                };

            string[] secondNames = new[]
            {
                    "Alexandrova",
                    "Dimova",
                    "Elenova",
                    "Dimitrov",
                    "Petkov",
                    "Goshov",
                    "",
                    "Dobromirova",
                    "Atanasova",
                    "Blagova"
                };

            string[] emails = new[]
            {
                    "Alexandrova@abv.bg",
                    "Dimova@abv.bg",
                    "Elenova@abv.bg",
                    "Dimitrov@abv.bg",
                    "Petkov@abv.bg",
                    "Goshov@abv.bg",
                    "",
                    "Dobromirova@abv.bg",
                    "Atanasova@abv.bg",
                    "Blagova@abv.bg"
                };

            string[] passwords = new[]
            {
                    "123456789",
                    "asdasdas",
                    "eeeeeeee",
                    "12233444556",
                    "wwwwwwawd3#d",
                    "123456789",
                    "123456789",
                    "123",
                    "12345678",
                    "idkdkdkdk"
                };

            for (int i = 0; i < firstNames.Length; i++)
            {
                var user = new User();
                user.FirstName = firstNames[i];
                user.LastName = secondNames[i];
                user.Password = passwords[i];
                user.Email = emails[i];

                bool isUserValid = IsValid(user);

                if (isUserValid == false)
                    continue;

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

        }

        private void SeedCreditCards(BillPaymentSystemContext context, Random rng)
        {

            ICollection<CreditCard> cards = new List<CreditCard>();

            for (int i = 0; i < 9; i++)
            {
                CreditCard creditCard = new CreditCard();
                creditCard.ExpirationDate = DateTime.Now.AddDays(rng.Next(-10, 51));
                creditCard.Limit = (decimal)rng.NextDouble() * 100M;
                creditCard.MoneyOwed = (decimal)rng.NextDouble() * 10;

                if (!IsValid(creditCard))
                {
                    continue;
                }

                cards.Add(creditCard);
            }

            context.CreditCards.AddRange(cards);
            context.SaveChanges();


        }

        private void SeedBankAccounts(BillPaymentSystemContext context, Random rng)
        {

            ICollection<BankAccount> accounts = new List<BankAccount>();

            var bankNames = new[]
            {
                "Pireus Bank",
                "OBB",
                "DSK Bank",
                "FiBank",
                "Ebaligo",
                "KTB",
                "VTB",
                "Raifaisen"
            };

            var swiftCodes = new[]
            {
                "AAAA BB CC DDD",
                "BFAD 4E FC",
                "BBBB BB BB BBB",
                "IDKK ID ID IDK",
                "1234 56 78 900",
                "A3F2 3D 4A FKK"
            };


            for (int i = 0; i < 9; i++)
            {
                BankAccount account = new BankAccount();
                account.Balance = (decimal)rng.NextDouble() * 100M;
                account.BankName = bankNames[rng.Next(0, bankNames.Length)];
                account.SWIFT = swiftCodes[rng.Next(0, swiftCodes.Length)];

                if (!IsValid(account))
                {
                    continue;
                }

                accounts.Add(account);
            }

            context.BankAccounts.AddRange(accounts);
            context.SaveChanges();

        }

        private void SeedPaymentMethods(BillPaymentSystemContext context, Random rng)
        {
            ICollection<PaymentMethod> methods = new List<PaymentMethod>();

            int cardsCount = context.CreditCards.Count();
            int bankAccountsCount = context.BankAccounts.Count();
            int usersCount = context.Users.Count();

            var users = context.Users.ToList();

            for (int i = 0; i < 20; i++)
            {
                PaymentMethod paymentMethod = new PaymentMethod();
                var randomId = rng.Next(1, usersCount + 1);

                var id = context
                    .Users
                    .FirstOrDefault(x => x.UserId == randomId)
                    .UserId;

                paymentMethod.UserId = id;

                if (i % 2 == 0)
                {
                    paymentMethod.Type = PaymentType.CreditCard;

                    randomId = rng.Next(1, cardsCount + 1);

                    var creditCardId = context
                    .CreditCards
                    .FirstOrDefault(x => x.CreditCardId == randomId)
                    .CreditCardId;

                    paymentMethod.CreditCardId = creditCardId;
                }

                else if (i % 3 == 0)
                {
                    paymentMethod.Type = PaymentType.CreditCard;

                    randomId = rng.Next(1, cardsCount + 1);

                    var creditCardId = context
                    .CreditCards
                    .FirstOrDefault(x => x.CreditCardId == randomId)
                    .CreditCardId;

                    paymentMethod.CreditCardId = creditCardId;

                    randomId = rng.Next(1, bankAccountsCount + 1);

                    var bankAccountId = context
                        .BankAccounts
                        .FirstOrDefault(x => x.BankAccountId == randomId)
                        .BankAccountId;

                    paymentMethod.BankAccountId = bankAccountId;
                }

                else
                {
                    paymentMethod.Type = PaymentType.BankAccount;

                    randomId = rng.Next(1, bankAccountsCount + 1);

                    var bankAccountId = context
                        .BankAccounts
                        .FirstOrDefault(x => x.BankAccountId == randomId)
                        .BankAccountId;

                    paymentMethod.BankAccountId = bankAccountId;
                }

                if (!IsValid(paymentMethod))
                {
                    continue;
                }

                methods.Add(paymentMethod);
            }

            context.PaymentMethods.AddRange(methods);
            context.SaveChanges();

        }

        private bool IsValid(object @object)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, true);

            return isValid;
        }
    }
}
