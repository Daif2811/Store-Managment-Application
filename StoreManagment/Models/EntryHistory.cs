using StoreManagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace StoreManagement.Models
{
    public class EntryHistory
    {

        public int EntryHistoryId { get; set; }



        [Required, Display(Name = "Product Name")]
        public string ProductName { get; set; }



        [Required]
        public float Price { get; set; }




        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }



        [Required]
        public int Damaged { get; set; }


        [Required]
        public int Count { get; set; }




        // FK
        public int CategoryId { get; set; }


        public int CompanyId { get; set; }


        public string UserId { get; set; }



        // Navigation Properties
        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
        public virtual Company Company { get; set; }


    }
}