using System;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace WebApplication2.Models
{
    [MetadataType(typeof(PartialCustomer))]
    public partial class Customer
    {

    }
    public class PartialCustomer
    {

        public string CustomerId { get; set; }

        [Required(ErrorMessage = "姓名必須輸入")]
        [Display(Name = "姓名")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "公司名稱必須輸入")]
        [Display(Name = "公司名稱")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "性別必須輸入")]
        [Display(Name = "性別")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "PhoneNumber必須輸入")]
        public Nullable<int> PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adress必須輸入")]
        public string Adress { get; set; }

        [Display(Name = "會員帳號")]
        [Required(ErrorMessage = "Account必須輸入")]
        [Remote(action: "VerifyAccount", controller: "MainPage")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Password必須輸入")]
        public string Password { get; set; }
    }
}