using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Model.RequestModel
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Kullanıcı adı girilmesi zorunludur.")]
        [MinLength(4, ErrorMessage = "Kullanıcı adı en az 4 karakter olmalıdır."), MaxLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olmaldır.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre girilmesi zorunludur.")]
        [MinLength(7, ErrorMessage = "Şifre en az 7 karakter olmalıdır."), MaxLength(50, ErrorMessage = "Şifre en fazla 50 karakter olmaldır.")]
        public string Password { get; set; }
    }
}
