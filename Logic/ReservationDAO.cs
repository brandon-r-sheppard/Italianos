using Italianos.App_Data.RestaurantDataSetTableAdapters;
using Italianos.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static Italianos.App_Data.RestaurantDataSet;

namespace Logic
{
    class ReservationDAO
    {
        ReservationTableAdapter _reservationAdapter;
        ReservationDataTable _reservationTable;
        ItemTableAdapter _itemAdapter;
        ItemDataTable _itemTable;
        UserDao _userDao;
        public ReservationDAO()
        {
            _userDao = new UserDao();
            _reservationAdapter = new ReservationTableAdapter();
            _reservationTable = new ReservationDataTable();
            _reservationAdapter.Fill(_reservationTable);
            _itemAdapter = new ItemTableAdapter();
            _itemTable = new ItemDataTable();
            _itemAdapter.Fill(_itemTable);
        }
        
        public List<String> GetAvailableItems()
        {
            List<String> items = new List<String>();
            foreach(DataRow r in _itemTable.Rows)
            {
                items.Add(r.Field<String>(0));
            }
            return items;
        }
        public List<String> GetAvailableItems(Course course)
        {
            List<String> items = new List<String>();
            foreach (DataRow r in _itemTable.Rows)
            {
                if(r.Field<Course>(3) == course)
                {
                    items.Add(r.Field<String>(0));
                }
            }
            return items;
        }
        public List<int> TablesAvailable(DateTime dt)
        {
            List<int> tableNums = new List<int>();
            for(int i = 0; i < 12; i++)
            {
                tableNums.Add(i + 1);
            }
            foreach (DataRow r in _reservationTable)
            {
                if(r.Field<DateTime>(3).Date == dt.Date
                    && r.Field<Status>(9) == Status.Reserved)
                {
                    tableNums.Remove(r.Field<int>(3));
                }
            }
            return tableNums;
        }
        public DataTable GetItems()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Course");
            dt.Columns.Add("Available");
            foreach(DataRow r in _itemTable.Rows)
            {
                DataRow row = dt.NewRow();
                row[0] = r.Field<String>(0);
                row[1] = r.Field<Category>(1);
                row[2] = r.Field<Course>(3);
                row[3] = r.Field<bool>(2);
                dt.Rows.Add(row);
            }
            return dt;
        }
        public void AddItem(string name, Category cat, Course course)
        {
            _itemAdapter.Insert(name, (int)cat, true, (int)course);
            _itemAdapter.Update(_itemTable);
        }
        public Item GetItemById(int id)
        {
            foreach(DataRow r in _itemTable.Rows)
            {
                if(r.Field<int>(0) == id)
                {
                    return new Item(
                           r.Field<String>(0),
                           r.Field<Category>(1),
                           r.Field<bool>(2),
                           r.Field<Course>(3));
                }
            }
            return null;
        }
        public List<Reservation> GetReservationsByUserId(int id)
        {
            List<Reservation> reservations = new List<Reservation>();
            foreach(DataRow r in _reservationTable.Rows)
            {
                if(r.Field<int>(1) == id)
                {
                    reservations.Add(new Reservation(
                                    r.Field<int>(0),
                                    _userDao.GetUserById(r.Field<int>(1)),
                                    r.Field<int>(2),
                                    r.Field<DateTime>(3),
                                    GetItemById(r.Field<int>(4)),
                                    GetItemById(r.Field<int>(5)),
                                    GetItemById(r.Field<int>(6)),
                                    r.Field<int>(7),
                                    r.Field<DateTime>(8),
                                    r.Field<Status>(9)));
                }
            }
            return null; // Reservation was not found;
        }
        public Reservation GetReservationByReservationId(int id)
        { 
            foreach (DataRow r in _reservationTable.Rows)
            {
                return new Reservation(
                            r.Field<int>(0),
                            _userDao.GetUserById(r.Field<int>(1)),
                            r.Field<int>(2),
                            r.Field<DateTime>(3),
                            GetItemById(r.Field<int>(4)),
                            GetItemById(r.Field<int>(5)),
                            GetItemById(r.Field<int>(6)),
                            r.Field<int>(7),
                            r.Field<DateTime>(8),
                            r.Field<Status>(9));
            }
            return null; // Reservation was not found;

        }

        public int CreateReservation(int hostId, DateTime reservationDate, int tableNumber, string appetizer, string main, string dessert, int numOfGuests)
        {
            /* Tables are booked in blocks of 2 hours.
             * To determine if a reservation is correct
             * we must check if a specific table has been
             * booked on the given day, for the given block
             * of time (4pm-6pm, 6pm-8pm, 8pm-10pm). */
            foreach (DataRow r in _reservationTable.Rows)
            {
                if (r.Field<DateTime>(3) == reservationDate && r.Field<int>(2) == tableNumber)
                    return 1; //Reservation could not be made becasue table is already booked
                else if (r.Field<int>(1) == hostId && r.Field<DateTime>(3) == reservationDate)
                    return 2; //Reservation could not be made because customer already has reservation
            }

            _reservationAdapter.Insert(hostId, tableNumber, reservationDate, appetizer, main, dessert, numOfGuests, DateTime.Now,(int) Status.Reserved);
            _reservationAdapter.Update(_reservationTable);
            return 0; //Added
        }
        public int DisableReservation(int reservationId)
        {
            foreach(DataRow r in _reservationTable.Rows)
            {
                if(r.Field<int>(0) == reservationId)
                {
                    r["Status"] = Status.Canceled;
                    _reservationAdapter.Update(_reservationTable);
                }
            }
            return 0; //Ran successfully
        }
        
        public List<Reservation> ReadAll()
        {
            List<Reservation> reservationList = new List<Reservation>();

            foreach (DataRow r in _reservationTable.Rows)
            {
                reservationList.Add(new Reservation(
                            r.Field<int>(0),
                            _userDao.GetUserById(r.Field<int>(1)),
                            r.Field<int>(2),
                            r.Field<DateTime>(3),
                            GetItemById(r.Field<int>(4)),
                            GetItemById(r.Field<int>(5)),
                            GetItemById(r.Field<int>(6)),
                            r.Field<int>(7),
                            r.Field<DateTime>(8),
                            r.Field<Status>(9)));
            }
            return reservationList;
        }

        public DataTable GetReservations(int id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Table Number");
            dt.Columns.Add("Reservation Date");
            dt.Columns.Add("Time Slot");
            dt.Columns.Add("Appetizer");
            dt.Columns.Add("Main");
            dt.Columns.Add("Dessert");
            dt.Columns.Add("Number Of Guests");
            dt.Columns.Add("Status");
            User user = _userDao.GetUserById(id);
            if (user.Role == Role.USER)
            {
                foreach(Reservation r in GetReservationsByUserId(user.UserId))
                {
                    DataRow row = dt.NewRow();
                    row[0] = r.TableNumber;
                    row[1] = r.ReservationDate.Date;
                    row[2] = r.ReservationDate.TimeOfDay;
                    row[3] = r.Appetizer.Name;
                    row[4] = r.Main.Name;
                    row[5] = r.Dessert.Name;
                    row[6] = r.NumberOfGuests;
                    row[7] = r.Status;

                }
            }
            else
            {
                foreach (Reservation r in ReadAll())
                {
                    if(r.Status == Status.Reserved)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = r.TableNumber;
                        row[1] = r.ReservationDate.Date;
                        row[2] = r.ReservationDate.TimeOfDay;
                        row[3] = r.Appetizer.Name;
                        row[4] = r.Main.Name;
                        row[5] = r.Dessert.Name;
                        row[6] = r.NumberOfGuests;
                        row[7] = r.Status;
                    }
                    

                }
            }
            return dt;
        }
    }
}
