namespace BillPaymentSystem.App.Commands
{
    using System;

    using Commands.Contracts;

    public class RetrieveUserCommand : ICommand
    {

        private string _firstName;
        private string _lastName;


        public RetrieveUserCommand(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName
        {
            get { return this._firstName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    this._firstName = value;
                else
                    throw new ArgumentException("Invalid name!");

                
            }
        }

        public string LastName
        {
            get { return this._lastName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    this._lastName = value;
                else
                    throw new ArgumentException("Invalid name!");
            }
        }

        public bool Execute()
        {
            bool hasExecutedSuccessfully = true;



            return hasExecutedSuccessfully;
        }
    }
}
