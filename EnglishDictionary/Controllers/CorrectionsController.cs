using EnglishDictionary.Models;
using Learning.Libs.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnglishDictionary.Controllers
{
    public class CorrectionsController : ApiController
    {
        // GET api/corrections/appli
        public Tuple<IEnumerable<string>, FunctionPerfData> Get(string word)
        {
            var ret = ModelWrapper.TrieDictionary.GetCorrections(word);
            return ret;
        }
    }
}
