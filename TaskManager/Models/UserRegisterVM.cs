using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.WebUI.Models
{
    public class UserRegisterVM
    {
        public string UserName { get; set; }
        [Required(ErrorMessage="Zorunlu")]
        [EmailAddress(ErrorMessage ="Lütfen doğru formatta mail adresinizi girin.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zorunlu")]
        [StringLength(50,ErrorMessage ="Minumum 6 karakter olmak zorunda",MinimumLength =6)]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
    }
}
