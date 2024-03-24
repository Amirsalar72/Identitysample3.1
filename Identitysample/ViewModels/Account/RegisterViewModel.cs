using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

namespace Identitysample.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        [Remote("ISUserNameAvailable","Account")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        [Remote("ISEmailAvailable", "Account")]

        public string Email { get; set; }
        [Required]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required]
        [Display(Name = "تکرار رمز عبور")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
