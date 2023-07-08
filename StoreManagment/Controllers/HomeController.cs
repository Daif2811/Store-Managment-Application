using StoreManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagment.Controllers
{

    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ControlPanel()
        {
            

            return View();
        }








        public ActionResult Numbers()
        {
            int Entered = 0, Exit = 0, Balance = 0, Damaged = 0;


            // Entered  And  Damaged
            foreach (var item in db.EntryHistories)
            {
                Entered += item.Count;
                Damaged += item.Damaged;
            }

            // Exit 
            foreach (var item in db.ExitProducts)
            {
                Exit += item.Count;
            }

            // Store Balance
            foreach (var item in db.EntryProducts)
            {
                Balance += item.Count;
            }



            ViewBag.Entered = Entered;
            ViewBag.Exit = Exit;
            ViewBag.Balance = Balance;
            ViewBag.Damaged = Damaged;

            return View();
        }








    }
}