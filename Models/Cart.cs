using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ScanAndGo.Models
{
    public class Cart
    {
        [Key]

        public int Id { get; set; }

        public int userId { get; set; }

        public int productId { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        public virtual Product Product { get; set; }

    }
}