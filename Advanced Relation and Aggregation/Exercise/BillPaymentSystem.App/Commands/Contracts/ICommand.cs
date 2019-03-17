namespace BillPaymentSystem.App.Commands.Contracts
{
    using BillPaymentSystem.Data;

    public interface ICommand
    {
        string Execute(string[] args, BillPaymentSystemContext context);
    }
}
