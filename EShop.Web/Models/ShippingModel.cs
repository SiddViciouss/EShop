using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models
{
    public class ShippingModel: IValidatableObject
    {
        public const string RequiredErrorMessage = "Это поле является обязательным для заполнения";
        public const string RangeErrorMessage = "Допустимый диапазон значений: от {1} до {2}";
        #region user parameters
        public string UserId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(100)]
        [Display(Name = "Имя")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Баланс")]
        public decimal AvailableMoney { get; set; }
        #endregion

        #region address 
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(100)]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(200)]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(20)]
        [Display(Name = "Дом/корпус")]
        public string Building { get; set; }

        [Range(1, 999, ErrorMessage = RangeErrorMessage)]
        [Display(Name = "Номер квартиры")]
        public int FlatNumber { get; set; }
        #endregion

        [Range(0, 999999.99, ErrorMessage = RangeErrorMessage)]
        [Display(Name = "Итого к оплате")]
        public decimal PriceTotal { get; set; }

        [StringLength(500)]
        [Display(Name = "Комментарий к заказу")]
        public string Comment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PriceTotal > AvailableMoney)
            {
                yield return new ValidationResult("Сумма к оплате превышает баланс счёта");
            }
        }
    }
}
