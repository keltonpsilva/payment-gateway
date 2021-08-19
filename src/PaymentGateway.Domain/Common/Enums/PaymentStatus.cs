using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Domain.Common.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "requires_payment_method")]
        RequiresPaymentMethod,

        [Display(Name = "requires_confirmation")]
        RequiresConfirmation,

        [Display(Name = "requires_action")]
        RequiresAction,

        [Display(Name = "processing")]
        Processing,

        [Display(Name = "requires_capture")]
        Requires_capture,

        [Display(Name = "canceled")]
        Canceled,

        [Display(Name = "succeeded")]
        Succeeded,

        [Display(Name = "failed")]
        Failed,
    }
}
