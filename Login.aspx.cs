using Italianos.Logic;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Italianos
{
    public partial class Login : System.Web.UI.Page
    {
        UserDao _userDao;
        protected void Page_Load(object sender, EventArgs e)
        {
            _userDao = new UserDao();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            
            if (_userDao.IsAuthentic(TxtEmail.Text, TxtPassword.Text))
            {
                Logic.User usr = _userDao.Login(TxtEmail.Text, TxtPassword.Text);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usr.UserId.ToString(), DateTime.Now,
                DateTime.Now.AddMinutes(10), false, usr.Role == Role.USER ? "user" : "admin");
                string strTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, strTicket));
                Response.Redirect(FormsAuthentication.GetRedirectUrl(usr.UserId.ToString(), false));
            }
        }
    }
}