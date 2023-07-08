using StoreManagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace StoreManagement.Models
{
    public class ExitProduct
    {

        public int ExitProductId { get; set; }


        [DataType(DataType.Date)]
        public DateTime ExitDate { get; set; }


        [Required , Range(1 , 1000, ErrorMessage ="Sorry, Available from  1  to  1000")]
        public int Count { get; set; }



        // FK
        public string UserId { get; set; }
        public int EntryProductId { get; set; }





        // Navigation Properties
        public virtual EntryProduct EntryProduct { get; set; }
        public virtual ApplicationUser User { get; set; }





    }
}