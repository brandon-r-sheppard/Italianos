using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Italianos.Admin
{
    public partial class ViewItems : System.Web.UI.Page
    {
        ReservationDAO _reservationDAO = new ReservationDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            ItemGrid.DataSource = _reservationDAO.GetItems();
            ItemGrid.DataBind();
        }
    }
}