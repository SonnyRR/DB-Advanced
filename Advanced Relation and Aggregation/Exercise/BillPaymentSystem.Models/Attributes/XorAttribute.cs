namespace BillPaymentSystem.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class XorAttribute : ValidationAttribute
    {
        private string _targetProperty;

        public XorAttribute(string targetProperty)
        {
            this._targetProperty = targetProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object desiredPropertyValue = validationContext
               .ObjectType
               .GetProperty(_targetProperty, BindingFlags.Instance | BindingFlags.Public)
               .GetValue(validationContext.ObjectInstance);

            if (value == null && desiredPropertyValue == null
                || value!=null && desiredPropertyValue != null)
            {
                return new ValidationResult("The two properties must have opposite values!");
            }

            return ValidationResult.Success;
        }
    }
}
