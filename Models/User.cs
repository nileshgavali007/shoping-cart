using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScanAndGo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Username..")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter the Confirm Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string Confirmpwd { get; set; }

        public int Balance { get; set; }

        [Required(ErrorMessage = "Please Enter Email...")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Select the Gender...")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Select the Marital Status...")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

    }
}