using EnglishDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnglishDictionary.Controllers
{
    public class MeaningController : ApiController
    {
        // GET api/meaning/apple
        public string Get(string word)
        {
            return ModelWrapper.TrieDictionary.GetMeaning(word);
        }
    }
}
