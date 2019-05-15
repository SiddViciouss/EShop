using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Models
{
    public class UserDTO
    {
        public string Id { get; set; }

        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        public string UserName { get; set; }
        public string Contact { get; set; }

        [Display(Name = "Пол")]
        public int Gender { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        public string JoinIp { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Улица")]
        public string Street { get; set; }

        [StringLength(20)]
        [Display(Name = "Дом/корпус")]
        public string Building { get; set; }

        [Display(Name = "Номер квартиры")]
        public int FlatNumber { get; set; }

        [Display(Name = "Баланс")]
        public decimal AvailableMoney { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
