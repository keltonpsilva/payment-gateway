using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.WebAPI.Models
{
    public class PaymentCreateModel : IValidatableObject
    {
        private readonly List<string> _allowedCurrency;

        public PaymentCreateModel()
        {
            _allowedCurrency = new List<string> { "GBP", "EUR" };
        }

        /// <summary>
        /// The customer Id
        /// </summary>
        /// <example>12345678</example>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// Three-letter ISO currency code, in lowercase.         
        /// </summary>
        /// <remarks>
        /// Support Currencies:
        /// - GBP
        /// - EUR       
        /// </remarks>
        /// <example>EUR</example>
        [Required]
        public string Currency { get; set; }

        /// <example>123</example>
        [Required]
        public decimal Amount { get; set; }

        /// <example>1234-1234-1234-1234</example>
        [Required]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        /// <example>123</example>
        [Required]
        [MaxLength(3)]
        public string CVC { get; set; }

        /// <summary>
        /// Two-digit number representing the card’s expiration month.
        /// </summary>
        /// <example>12</example>
        [Required]
        [Range(1, 12)]
        public int ExpirationMonth { get; set; }

        /// <summary>
        /// Four-digit number representing the card’s expiration year.
        /// </summary>
        /// <example>2433</example>
        [Required]
        public int ExpirationYear { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!_allowedCurrency.Contains(Currency.ToUpper())) {
                yield return new ValidationResult($"Unfortunately we only support 'GBP, EUR' ", new[] { nameof(Currency) });
            }

            if (Amount < 1) {
                yield return new ValidationResult($"Please insert a valid Amount' ", new[] { nameof(Amount) });
            }

            if (ExpirationYear < DateTime.Today.Year) {
                yield return new ValidationResult($"Please insert a valid Expiration Year' ", new[] { nameof(ExpirationYear) });
            }
        }
    }
}
