using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ParaTakip.Model.RequestModel
{
    public class RegisterServiceRequestModel
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı girilmesi zorunludur.")]
        [MinLength(4, ErrorMessage = "Kullanıcı adı en az 4 karakter olmalıdır."), MaxLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olmaldır.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-Posta adresi girilmesi zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
        public string EmailAddress { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "Şifre girilmesi zorunludur.")]
        [MinLength(7, ErrorMessage = "Şifre en az 7 karakter olmalıdır."), MaxLength(50, ErrorMessage = "Şifre en fazla 50 karakter olmaldır.")]
        public string Password { get; set; }

        [DisplayName("Context")]
        public HttpContext HttpContext { get; set; }

        public string[]? LoginAllowsRoles { get; set; }
    }
}
