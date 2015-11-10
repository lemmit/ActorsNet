using System.Collections.Generic;
using System.Web.Mvc;
using ActorsNet.JavascriptGenerator.Attributes;
using ActorsNet.JavascriptGenerator.Collections;
using ActorsNet.JavascriptGenerator.Providers.Interfaces;

namespace ActorsNet.JavascriptGenerator.Controllers
{
    /// <summary>
    /// Javascript Generator controller
    /// Returns strongly typed message factories 
    /// that can be used to write JS client side 
    /// using code completion tools 
    /// </summary>
    public class ActorNetController : Controller
    {
        private readonly IJsonStringOfMessageProvider _jsonStringOfMessageProvider;
        private readonly INamesBySystemProvider _namesBySystemProvider;

        public ActorNetController(
            INamesBySystemProvider namesBySystemProvider,
            IJsonStringOfMessageProvider jsonStringOfMessageProvider
            )
        {
            _namesBySystemProvider = namesBySystemProvider;
            _jsonStringOfMessageProvider = jsonStringOfMessageProvider;
        }

        // GET: Akka
        [JavaScriptFile]
        //[OutputCache(Duration=int.MaxValue)]
        public ActionResult Js()
        {
            ViewBag.MessagesBySystem = MessagesNamesBySystemDictionary();
            ViewBag.JsonStringFromMessageType =
                JsonStringFromMessageDictionary();
            return PartialView();
        }

        private IDictionary<string, string> JsonStringFromMessageDictionary()
        {
            if (_jsonStringOfMessageProvider == null)
            {
                return new Dictionary<string, string>().WithDefaultValue("{}");
            }
            return _jsonStringOfMessageProvider.Get()
                .WithDefaultValue("{}");
        }

        private IDictionary<string, List<string>> MessagesNamesBySystemDictionary()
        {
            var emptyList = new List<string>();
            if (_namesBySystemProvider == null)
            {
                var emptyDict = new Dictionary<string, List<string>>();
                var emptyDefaultableDict = emptyDict.WithDefaultValue(emptyList);
                return emptyDefaultableDict;
            }
            return _namesBySystemProvider.Get();
        }
    }
}