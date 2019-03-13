namespace BillPaymentSystem.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class ExpirationAttribute : ValidationAttribute
    {
        private string _desiredPropertyName;
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        { 

            var currentTime = DateTime.Now;

            if (currentTime > (DateTime)value)
            {
                return new ValidationResult("Card has expired!");
            }

            return ValidationResult.Success;
        }
    }
}
