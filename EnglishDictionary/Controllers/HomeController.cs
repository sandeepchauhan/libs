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
            ViewBag.Title = Model.Dictionary.GetStats();
            return View();
        }

        public ActionResult Meaning(string word)
        {
            string meaning = Model.Dictionary.GetData(word);
            return Content(meaning);
        }

        public ActionResult Suggestions(string word)
        {
            IEnumerable<string> suggestions = Model.Dictionary.GetSuggestions(word);
            JsonResult ret = Json(suggestions, JsonRequestBehavior.AllowGet);
            return ret;
        }
    }
}
