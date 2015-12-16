using EnglishDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishDictionary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = ModelWrapper.TrieDictionary.Stats();
            return View();
        }

        public ActionResult Editor()
        {
            return View();
        }
    }
}
