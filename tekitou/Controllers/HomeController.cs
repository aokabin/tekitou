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
		static private List<UserData> userData = new List<UserData>();
		static private int userNumber = 0;
		//以下2つは今後不要になる、理由はどちらも動的な値になってくるから。でも今稼働しているサーバー情報は持っておく必要あるかもしれない
		private List<string> serverList = new List<string> {"hoge.com", "kodai.com", "myserver.com"}; 
		public List<string> processList = new List<string> {"1001", "1002", "1003"};

		public ActionResult Index ()
		{
			var mvcName = typeof(Controller).Assembly.GetName ();
			var isMono = Type.GetType ("Mono.Runtime") != null;

			ViewData ["Version"] = mvcName.Version.Major;
			ViewData ["Runtime"] = isMono ? "Mono" : ".NET";

			return View ();
		}

		[HttpPost]
		public string Connection() {
			int listNumber, processNumber;
			processNumber = 0;
			for (listNumber = 0; listNumber < userData.Count; listNumber++) {
				//このprocessListの0番目という指定は、userが接続要求するprocessIDで比較する事になると思われる(ということは、HOSTかそうでないかをリクエストで指定する必要がある、その上でHOSTでないなら接続したいPIDをpostする)
				//今はPIDが1001というリクエストがきたという想定
				if (userData [listNumber].userProcessID == processList [0]) {
					processNumber++;
					//この8という値もdifineとかで定義すべき (やりかたわからない)
					if (processNumber >= 8) {
						return "false";
					}
				}
			}

			UserData data = new UserData();
//			userNumber++;
			Console.WriteLine ("now user numver is " + userNumber);
			int i = userNumber;
			i++;
			data.userID = Convert.ToString(i);
			data.userName = Request.Form.Get ("name");
			//serverは動的に変化するので、変更の可能性
			data.userConnectingServer = "http://" + serverList[0] + ":8080";
			data.userIPAddress = Request.UserHostAddress;
			//PIDは動的に変化するので、変更の可能性
//			data.userProcessID = processList[0];
			data.userProcessID = Request.Form.Get("pid");
			userData.Add (data);

//			Console.WriteLine (userData [i - 1].userIPAddress);
//			Console.WriteLine (userData[i - 1].userName);
//			Console.WriteLine (data.userID);

			//GameLaunch ();

			userNumber++;
//			Console.WriteLine ("next user number is " + userNumber);

			return data.userConnectingServer;
		}

		[HttpPost]
		public bool DisconectGame() {
			string processID = Request.Form.Get ("pid");
//			int i;
//			int removeNumber;
			/*
			for (i = 0; i < userData.Count; i++) {
				if (userData [i].userProcessID == processID) {

				}
			}
			*/
			userData.RemoveAll (s => s.userProcessID == processID);

			return true;
		}

		public string ShowUsers() {
			string userList = null;
			int i;
			Console.WriteLine (userData.Count);
			for (i = userData.Count - 1; i >= 0; i--) {
				Console.WriteLine ("now =" + i);
				userList += "[" 
					+ userData[i].userID + "," 
					+ userData[i].userName + "," 
					+ userData[i].userConnectingServer + "," 
					+ userData[i].userIPAddress + ","
					+ userData[i].userProcessID
					+ "]</br>";
			}
			return userList;
//			return userData.ToString();
		}

		//実際にはこのスクリプトにはインスタンスの立ち上げとかゲームサーバの起動とかが書かれる
		private void GameLaunch () {
			System.Diagnostics.Process.Start ("launchGame.sh");
		}

		//つかってないやつら
		public string GameStart () {
			System.Diagnostics.Process.Start ("test.sh");
			string url = "http://ec2-54-64-96-247.ap-northeast-1.compute.amazonaws.com:8080/";
			return url;
		}

		public string Test () {
			string url = "http://ec2-54-64-96-247.ap-northeast-1.compute.amazonaws.com:8080/Home/GameStart";
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

