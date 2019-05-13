using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models
{
    public class ShippingModel
    {
        #region user parameters
        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Имя")]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        public decimal AvailableMoney { get; set; }
        #endregion

        #region address 
        [Required]
        [StringLength(100)]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Дом/корпус")]
        public string Building { get; set; }

        [Required]
        [Range(0, 999)]
        [Display(Name = "Номер квартиры")]
        public int FlatNumber { get; set; }
        #endregion

        [Range(0, 999999.99)]
        [Display(Name = "Итого к оплате")]
        public decimal PriceTotal { get; set; }

        [StringLength(500)]
        [Display(Name = "Комментарий к заказу")]
        public string Comment { get; set; }

    }
}
