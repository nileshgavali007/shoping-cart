using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ScanAndGo.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name!")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters !")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Enter Price!")]
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Display(Name = "In Stock")]
        public Boolean Active { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Stocking Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime stockingDate { get; set; }
        [Display(Name = "Category")]
        public int categoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}