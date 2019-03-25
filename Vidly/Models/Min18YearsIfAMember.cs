using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            // magic numbers hurts maintainability of the application
            // when we use enum, we need to cast with byte, should avoid extra casting
            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthday is required");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age > 18)
                       ? ValidationResult.Success
                       : new ValidationResult("Customer should be at least 18 years old to go on a membership");
        }
    }
}