using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Models
{
    public class Category
    {
        public int CategoryId { get; set; }


        [Required,Display(Name = "Category Name")]
        public string CategoryName { get; set; }





        public virtual ICollection<EntryProduct> EntryProducts { get; set; }
        public virtual ICollection<EntryHistory> EntryHistories { get; set; }
    }
}