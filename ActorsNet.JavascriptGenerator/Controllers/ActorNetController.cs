using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using ActorsNet.JavascriptGenerator.Attributes;
using ActorsNet.JavascriptGenerator.Collections;
using ActorsNet.JavascriptGenerator.Factories.Interfaces;
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
        private readonly IJsonStringifiedObjectFactory _stringifiedObjectFactory;
        private readonly IMessageNamesBySystemProvider _messageNamesBySystemProvider;

        public ActorNetController(
            IMessageNamesBySystemProvider messageNamesBySystemProvider,
            IJsonStringifiedObjectFactory stringifiedObjectFactory
            )
        {
            _messageNamesBySystemProvider = messageNamesBySystemProvider;
            _stringifiedObjectFactory = stringifiedObjectFactory;
        }

        // GET: Akka
        [JavaScriptFile]
        //[OutputCache(Duration=int.MaxValue)]
        public ActionResult Js()
        {
            var messagesBySystem = MessagesNamesBySystemDictionary();
            ViewBag.MessagesBySystem = messagesBySystem;
            ViewBag.JsonStringFromMessageType =
                JsonStringFromMessageDictionary(messagesBySystem);
            return PartialView();
        }

        private IDictionary<string, string> JsonStringFromMessageDictionary(IDictionary<string, IList<string>> messageByName)
        {

            var dict = new Dictionary<string, string>().WithDefaultValue("{}");
            foreach (var system in messageByName)
            {
                foreach (var message in system.Value)
                {
                    try
                    {
                        dict[message] = _stringifiedObjectFactory.CreateExampleJsonObjectOfType(message);
                    }
                    catch (Exception e)
                    {
                        Debug.Write(e);
                    }
                }
            }
            return dict;
        }

        private IDictionary<string, IList<string>> MessagesNamesBySystemDictionary()
        {
            var emptyList = new List<string>();
            if (_messageNamesBySystemProvider == null)
            {
                var emptyDict = new Dictionary<string, IList<string>>();
                var emptyDefaultableDict = emptyDict.WithDefaultValue(emptyList);
                return emptyDefaultableDict;
            }
            return _messageNamesBySystemProvider.Get();
        }
    }
}