using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace tekitou.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			var mvcName = typeof(Controller).Assembly.GetName ();
			var isMono = Type.GetType ("Mono.Runtime") != null;

			ViewData ["Version"] = mvcName.Version.Major;
			ViewData ["Runtime"] = isMono ? "Mono" : ".NET";

			return View ();
		}

		public string Test () {
			string hoge = "my name is kodai.";
//			return View ();
			return hoge;
		}

		[HttpPost]
		public JsonResult GameResult () {
			string name = Request.Form.Get ("name");
			string returnJson = "{name:" + name + "}";
			return Json(returnJson);
		}

	}
}

