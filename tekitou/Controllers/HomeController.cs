using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Microsoft.CSharp;

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
			string url = "http://ec2-54-64-79-127.ap-northeast-1.compute.amazonaws.com:8080/Home/GameStart";
			return url;
		}

		public string GameStart () {
			System.Diagnostics.Process.Start ("/root/test.sh");
			string url = "http://ec2-54-64-79-127.ap-northeast-1.compute.amazonaws.com:8080/";
			return url;

		}

		[HttpPost]
		public JsonResult GameResult () {
			string name = Request.Form.Get ("name");
			string returnJson = "{name:" + name + "}";
			return Json(returnJson);
		}

	}
}

