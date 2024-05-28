using System.ComponentModel.DataAnnotations;

namespace MEAL_2024_API.Models
{
    public class CouponModel
    {
        [Key]
        public Guid CouponId { get; set; }

        // Foreign key reference to BookingModel
        public Guid BookingId { get; set; }
        public BookingModel Booking { get; set; }

        // QR Code stored as Base64 string
        public string QrCodeBase64 { get; set; }

        // Date when the coupon is created
        public DateTime CreatedDate { get; set; }

        // Indicates if the coupon is redeemed
        public bool IsRedeemed { get; set; }

        // Indicates if the coupon is deleted
        public bool IsDeleted { get; set; }

    }
}
