using CommunicationEntities;
using DbEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFClient.Model;
using WFClient.View;

namespace WFClient.Controller
{
	public class ControllerFormAuth
	{
		private AuthenForm form = null;
		private APIWorker API = null;

		public ControllerFormAuth(AuthenForm form)
		{
			this.form = form;
			API = APIWorker.GetInstance();
		}
		public async Task UsersGetUserByLoginPassword()
		{
			string login = form.textBoxLogin.Text;
			string password = form.textBoxPassword.Text;

			Response response = await API.UsersGetUserByLoginPassword(login, password);

			if (response.Status == Response.StatusList.OK)
			{
				User user = JsonConvert.DeserializeObject<User>(response.Data);
				form.textBoxResult.Text = "Success";
			}
			else
			{
				form.textBoxResult.Text = $"Error: {response.Data}";
			}
		}
	}
}
