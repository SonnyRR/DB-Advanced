namespace BillPaymentSystem.App
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using BillPaymentSystem.App.Core;
    using BillPaymentSystem.Data;

    public class EntryPoint
    {
        public static void Main()
        {

            using (BillPaymentSystemContext context = new BillPaymentSystemContext())
            {
                //DbInitializer initializer = new DbInitializer(context);
                //context.Database.EnsureCreated();
                //initializer.Seed();               

                var methods = context.PaymentMethods.ToList();
                ;
            }

            var engine = new Engine();
            engine.Run();
        }
    }
}
