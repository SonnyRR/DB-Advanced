namespace BillPaymentSystem.App.Engine
{
    using System;
    using System.Collections.Generic;
    using BillPaymentSystem.Data;
    using Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public class Engine : IEngine
    {
        private IServiceCollection services = new ServiceCollection();

        public Engine(BillPaymentSystemContext context)
        {
            this.services.AddScoped(x => context);

        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
