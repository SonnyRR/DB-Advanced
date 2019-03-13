namespace BillPaymentSystem.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BillPaymentSystem.Data;
    using BillPaymentSystem.Models;

    public class DbInitializer
    {
        private Random _rng;
        private BillPaymentSystemContext _context;

        public DbInitializer(BillPaymentSystemContext context)
        {
            this._rng = new Random();
            this._context = context;
        }

        public static void Seed()
        {

        }

        public static void SeedUsers(BillPaymentSystemContext context)
        {
            using (context)
            {
                ICollection<User> users = new List<User>();

                string[] firstNames = new[]
                {
                "Lilly", "Mirela", "Antonia", "Cvetan", "Pesho", "Gosho", null, "", "Vyara", "Maria"
            };

                string[] secondNames = new[]
                {
                "Alexandrova", "Dimova", "Elenova", "Dimitrov", "Petkov", "Goshov", "", "Dobromirova", "Atanasova", "Blagova"
            };

                string[] emails = new[]
                {
                "Alexandrova@abv.bg", "Dimova@abv.bg", "Elenova@abv.bg", "Dimitrov@abv.bg", "Petkov@abv.bg", "Goshov@abv.bg", "", "Dobromirova@abv.bg", "Atanasova@abv.bg", "Blagova@abv.bg"
            };

                string[] passwords = new[]
                {
                "123456789", "asdasdas", "eeeeeeee", "12233444556", "wwwwwwawd3#d", "123456789", "123456789", "123", "12345678"
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
        }

        public static void CreditCardSeed(BillPaymentSystemContext context)
        {
            using (context)
            {

            }
        }

        public static bool IsValid(object @object)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, true);

            return isValid;
        }
    }
}
