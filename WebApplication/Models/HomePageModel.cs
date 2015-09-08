using Learning.Libs.DataStructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication.Models
{
    public class HomePageModel
    {
        public List<CheckBox> SortingAlgorithms { get; set; }

        public HomePageModel()
        {
            SortingAlgorithms = new List<CheckBox>();
        }
    }
}