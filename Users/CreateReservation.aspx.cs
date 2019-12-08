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

namespace Italianos.User
{
    public partial class CreateReservation : System.Web.UI.Page
    {
        ReservationDAO _reservationDao = new ReservationDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimeSlot.Visible = false;
                TableNumber.Visible = false;
                Appetizer.Visible = false;
                Main.Visible = false;
                Dessert.Visible = false;
                TxtNumOfGuests.Visible = false;
            }

        }

        protected void Date_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime rangeStart = DateTime.Now.AddDays(3);
            DateTime rangeEnd = DateTime.Now.AddDays(33);

            if (e.Day.Date < rangeStart || e.Day.Date > rangeEnd)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
        }
        /*                 TimeSlot.DataSource = _reservationDao.TablesAvailable(Date.SelectedDate); */
        protected void Date_SelectionChanged(object sender, EventArgs e)
        {
            if (!(Date.SelectedDate.Date == null))
            {
                if(TimeSlot.Items.Count > 0)
                {
                    TimeSlot.Items.Clear();
                    TableNumber.Visible = false;
                    Appetizer.Visible = false;
                    Main.Visible = false;
                    Dessert.Visible = false;
                    TxtNumOfGuests.Visible = false;
                }
                TableNumber.DataSource = _reservationDao.TablesAvailable(Date.SelectedDate.AddHours(16));
                TableNumber.DataBind();
                TableNumber.Visible = true;
                TimeSlot.Items.Add("4PM");
                TimeSlot.Items.Add("6PM");
                TimeSlot.Items.Add("8PM");
                TimeSlot.Visible = true;
            }
        }

        protected void TimeSlot_TextChanged(object sender, EventArgs e)
        {
            if (TimeSlot.SelectedValue.Equals("4PM"))
                TableNumber.DataSource = _reservationDao.TablesAvailable(Date.SelectedDate.AddHours(16));
            else if (TimeSlot.SelectedValue.Equals("6PM"))
                TableNumber.DataSource = _reservationDao.TablesAvailable(Date.SelectedDate.AddHours(18));
            else if (TimeSlot.SelectedValue.Equals("8PM"))
                TableNumber.DataSource = _reservationDao.TablesAvailable(Date.SelectedDate.AddHours(20));
            TableNumber.DataBind();
        }

        protected void TableNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Appetizer.DataSource = _reservationDao.GetAvailableItems(Course.Appetizer);
            Appetizer.DataBind();
            Appetizer.Visible = true;
            Main.DataSource = _reservationDao.GetAvailableItems(Course.Main);
            Main.DataBind();
            Main.Visible = true;
            Dessert.DataSource = _reservationDao.GetAvailableItems(Course.Dessert);
            Dessert.DataBind();
            Dessert.Visible = true;
            TxtNumOfGuests.Visible = true;
            List<int> nums = new List<int>();
            for (int i = 0; i < 8; i++)
                nums.Add(i + 1);
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            DateTime resTime = Date.SelectedDate;
            if (TimeSlot.SelectedValue.Equals("4PM"))
                resTime.AddHours(16);
            else if (TimeSlot.SelectedValue.Equals("6PM"))
                resTime.AddHours(18);
            else if (TimeSlot.SelectedValue.Equals("8PM"))
                resTime.AddHours(20);
            _reservationDao.CreateReservation(int.Parse(cookie.Name),
                                              resTime,
                                              int.Parse(TableNumber.Text),
                                              Appetizer.Text,
                                              Main.Text,
                                              Dessert.Text,
                                              int.Parse(TxtNumOfGuests.Text));
        }
    }
}