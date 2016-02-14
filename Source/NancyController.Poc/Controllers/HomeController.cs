using System.Linq;
using System.Text;
using System.Web.Mvc;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using NancyController.Poc.Models;

namespace NancyController.Poc.Controllers
{
    public sealed class HomeModule : NancyControllerModule
    {
        public Negotiator Index()
        {
            return View["Index"];
        }

        [HttpGet, HttpPost]
        public Negotiator Post(string title)
        {
            var model = new ExampleViewModel
            {
                Title = title,
                Iteration = Enumerable.Range(1, 10)
            };
            return View["Post", model];
        }

        public TextResponse Script()
        {
            return new TextResponse("// JavaScript");
        }

        public TextResponse Style()
        {
            return new TextResponse(
                "/* Style */", "text/css",Encoding.UTF8);
        }
    }
}
