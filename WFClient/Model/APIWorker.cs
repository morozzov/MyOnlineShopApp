using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClientLibrary;
using CommunicationEntities;

namespace WFClient.Model
{
	public class APIWorker
	{
		private static APIWorker instance = null;
		private Client client = null;
		private string APIKey;
		public static APIWorker GetInstance()
		{
			if (instance == null)
			{
				instance = new APIWorker();
			}
			return instance;
		}
		public APIWorker()
		{
			client = new Client();
			APIKey = "JDI89U283UDj892uj389du2389U**(U&&*Y*#WU*DJ#*(UJ*@JHDUJ*(U*)(UD";
		}

		public Task<Response> UsersGetUserByLoginPassword(string login, string password)
		{
			Dictionary<string, string> userParams = new Dictionary<string, string>();
			userParams["login"] = login;
			userParams["password"] = password;

			Request request = new Request()
			{
				Command = "Users.GetUserByLoginPassword",
				Parameters = JsonConvert.SerializeObject(userParams),
				APIKey = APIKey
			};

			return client.RecieveServerResponseAsync(request);
		}
	}
}
