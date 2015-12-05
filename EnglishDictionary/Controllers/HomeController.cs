using DictionaryConsoleApp;
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
        private TrieDictionary _trieDictionary = new TrieDictionary(@"D:\gitrepos\myrepos\resources\EnglishDictionary-Processed.txt");

        public ActionResult Index()
        {
            return View();
        }
    }
}
