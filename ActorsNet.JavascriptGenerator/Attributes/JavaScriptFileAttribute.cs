using System.Web.Mvc;

namespace ActorsNet.JavascriptGenerator.Attributes
{
    public sealed class JavaScriptFileAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.ContentType = "text/javascript";
        }
    }
}