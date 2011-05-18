using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using ButterflyNet.Parser;
using log4net;
using Newtonsoft.Json;

namespace ButterflyNet.WebApi.Controllers {

	public class ButterflyController : Controller {

		private static ILog logger = LogManager.GetLogger("butterfly");

		protected new ActionResult Json(object data) {
			return new ContentResult {
				ContentType = "application/json",
				ContentEncoding = Encoding.UTF8,
				Content = JsonConvert.SerializeObject(data)
			};
		}

		[HttpGet]
		public ActionResult Index() {
			return View();
		}

		public class ParseModel {
			public string MarkUp { get; set; }
		}

		public class ParseModelBinder : IModelBinder {
			public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
				return new ParseModel {
					MarkUp = controllerContext.HttpContext.Request.Form["markup"]
				};
			}
		}

		[HttpPost, ValidateInput(false)]
		public ActionResult Index(ParseModel model) {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.Analyzer = new HtmlAnalyzer(new StringWriter());

			try {
				return Json(new { error = (string)null, html = parser.ParseAndReturn(model.MarkUp) });
			} catch (Exception e) {
				return Json(new { error = e.Message, html = (string)null });
			}
		}

		public ActionResult Error() {
			return HttpNotFound();
		}

		protected override void HandleUnknownAction(string actionName) {
			HttpNotFound().ExecuteResult(ControllerContext);
		}

	}
}
