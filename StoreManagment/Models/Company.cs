using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Models
{
    public class Company
    {
        public int CompanyId { get; set; }


        [Required,Display(Name = "Company Name")]
        public string CompanyName { get; set;}



        [Required, Display(Name = "Company Email"), EmailAddress]
        public string CompanyEmail { get; set; }




        public virtual ICollection<EntryProduct> EntryProducts { get; set; }
        public virtual ICollection<EntryHistory> EntryHistories { get; set; }




    }
}