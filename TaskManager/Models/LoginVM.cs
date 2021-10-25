using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.WebUI.Models
{
    public class LoginVM
    {
         
        [Required(ErrorMessage = "Zorunlu")]
        [EmailAddress(ErrorMessage = "Lütfen doðru formatta mail adresinizi girin.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zorunlu")]
        [StringLength(50, ErrorMessage = "Minumum 6 karakter olmak zorunda", MinimumLength = 6)]
        [DataType(DataType.Password)]

        public string Password { get; set; }
      
    }
}
