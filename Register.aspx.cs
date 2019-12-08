using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Italianos
{
    public partial class Register : System.Web.UI.Page
    {
        UserDao _userDao;
        protected void Page_Load(object sender, EventArgs e)
        {
            _userDao = new UserDao();
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            bool canRegister = true;

            if (_userDao.EmailExists(TxtEmail.Text))
            {
                canRegister = false;
                ErrEmail.Text = "Email exists";
            } else if (String.IsNullOrEmpty(TxtEmail.Text))
            {
                canRegister = false;
                ErrEmail.Text = "Email cannot be empty";
            }

            if(TxtPassword.Text.Length < 10)
            {
                canRegister = false;
                ErrPassword.Text = "Password cannot be less than 10 characters";
            }
            else if(TxtPassword.Text.Length > 15)
            {
                canRegister = false;
                ErrPassword.Text = "Password cannot be greater than 15 characters";
            }

            if (String.IsNullOrEmpty(TxtFirstName.Text))
            {
                canRegister = false;
                ErrFName.Text = "First name cannot be empty";
            }

            if (String.IsNullOrEmpty(TxtLastName.Text))
            {
                canRegister = false;
                ErrLName.Text = "Last name cannot be empty";
            }

            if (canRegister)
            {
                _userDao.Register(TxtEmail.Text, TxtPassword.Text, TxtFirstName.Text, TxtLastName.Text, TxtPhoneNumber.Text);
            
            }
        }
    }
}