using Learning.Libs.DataStructures;
using Learning.Libs.DataStructures.Enums;
using Learning.Libs.DataStructures.Interfaces;
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
            string[] sortingAlgorithms = Enum.GetNames(typeof(SortingAlgorithm));
            foreach(string item in sortingAlgorithms)
            {
                CheckBox cb = new CheckBox();
                cb.Text = item.ToString();
                cb.ID = item.ToString();
                homePageModel.SortingAlgorithms.Add(cb);
            }

            return View(homePageModel);
        }

        public ActionResult DoSorting(string sortingAlgorithms, int numElements)
        {
            List<SortingAlgorithm> sortingAlgorithmsArray = sortingAlgorithms.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select<string, SortingAlgorithm>(x => (SortingAlgorithm)Enum.Parse(typeof(SortingAlgorithm), x)).ToList();
            SortingAlgorithm sa = sortingAlgorithmsArray[0];
            SortingScenario sortingScenario = new SortingScenario(sa, numElements, SortInputType.Random);
            Console.WriteLine(sortingScenario);
            ISortableCollection<int> collection = new ArrayImpl<int>(numElements);
            collection.AddMany(GetInput<int>(sa, numElements, SortInputType.Random));
            collection.Sort(sa, SortingAlgorithmType.Iterative);
            JsonResult ret = Json(collection, JsonRequestBehavior.AllowGet);
            return ret;
        }

        private static List<T> GetInput<T>(SortingAlgorithm sortingAlgorithm, int numElements, SortInputType sortInputType)
        {
            switch (sortInputType)
            {
                case SortInputType.BestCase:
                    return DataProviderForSortAlgorithms.GenerateBestCaseInput<T>(sortingAlgorithm, numElements);
                case SortInputType.Random:
                    return DataProviderForSortAlgorithms.GenerateRandomInput<T>(numElements);
                case SortInputType.WorstCase:
                    return DataProviderForSortAlgorithms.GenerateWorstCaseInput<T>(sortingAlgorithm, numElements);
            }

            throw new NotSupportedException();
        }
    }
}
