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
		private List<UserData> userData = new List<UserData>();
		private int userID = 0;
		private string userList;

		public ActionResult Index ()
		{
			var mvcName = typeof(Controller).Assembly.GetName ();
			var isMono = Type.GetType ("Mono.Runtime") != null;

			ViewData ["Version"] = mvcName.Version.Major;
			ViewData ["Runtime"] = isMono ? "Mono" : ".NET";

			return View ();
		}

		public string Test () {
			string url = "http://ec2-54-64-96-247.ap-northeast-1.compute.amazonaws.com:8080/Home/GameStart";
			return url;
		}

		public string Connection() {
			UserData data = new UserData();
			data.userID = ++userID;
			data.userName = Request.Form.Get ("name");
			data.userConnectingServer = "http://ec2-54-64-96-247.ap-northeast-1.compute.amazonaws.com:8080";
			userData.Add (data);

			GameLaunch ();

			return data.userConnectingServer;
		}

		public string ShowUsers() {
			userList = new string();
			int i;
			for (i = userData.Count; i > 0; i--) {
				userList += userList += "[" + userData[i].userID + "," + userData[i].userName + userData[i].userConnectingServer + "]\n";
			}
			return userList;
		}

		//実際にはこのスクリプトにはインスタンスの立ち上げとかゲームサーバの起動とかが書かれる
		private void GameLaunch () {
			System.Diagnostics.Process.Start ("launchGame.sh");
		}

		public string GameStart () {
			System.Diagnostics.Process.Start ("test.sh");
			string url = "http://ec2-54-64-96-247.ap-northeast-1.compute.amazonaws.com:8080/";
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

