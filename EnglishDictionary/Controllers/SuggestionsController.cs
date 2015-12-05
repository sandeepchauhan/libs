using EnglishDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnglishDictionary.Controllers
{
    public class SuggestionsController : ApiController
    {
        // GET api/suggestions/appl
        public IEnumerable<string> Get(string word)
        {
            return ModelWrapper.TrieDictionary.GetSuggestions(word);
        }
    }
}
