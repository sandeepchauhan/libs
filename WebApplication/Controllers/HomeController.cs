using Learning.Libs.DataStructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            HomePageModel homePageModel = new HomePageModel();
            Array sortingAlgorithms = Enum.GetValues(typeof(SortingAlgorithm));
            foreach(var item in sortingAlgorithms)
            {
                CheckBox cb = new CheckBox();
                cb.Text = item.ToString();
                cb.ID = item.ToString();
                homePageModel.SortingAlgorithms.Add(cb);
            }

            return View(homePageModel);
        }
    }
}
