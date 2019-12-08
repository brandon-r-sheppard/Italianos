using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Italianos.Logic;
using Logic;
namespace Italianos.Admin
{
    public partial class AddMenuItem : System.Web.UI.Page
    {
        ReservationDAO _reservationDAO = new ReservationDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDCategory.DataSource = Enum.GetValues(typeof(Category));
                DDCategory.DataBind();
                DDCourse.DataSource = Enum.GetValues(typeof(Course));
                DDCourse.DataBind();
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            bool canCreateItem = true;
            foreach(String s in _reservationDAO.GetAvailableItems())
            {
                if (s == TxtItemName.Text)
                    canCreateItem = false;
            }
            if (canCreateItem)
            {
                _reservationDAO.AddItem(TxtItemName.Text,
                       (Category)Enum.Parse(typeof(Category), DDCategory.SelectedValue),
                       (Course)Enum.Parse(typeof(Course), DDCourse.SelectedValue));
                Response.Redirect("~/Admin/ViewItems.aspx");
            }
            else
                ErrName.Text = "Item name in use";
        }

        protected void BtnCreateItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AddMenuItem.aspx");
        }
    }
}