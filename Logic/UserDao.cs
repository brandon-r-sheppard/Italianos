using Italianos.App_Data.RestaurantDataSetTableAdapters;
using Italianos.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows;
using static Italianos.App_Data.RestaurantDataSet;

namespace Logic
{
    public class UserDao
    {

        UserTableAdapter _userAdapter;
        UserDataTable _userTable;
        public UserDao()
        {
            _userAdapter = new UserTableAdapter();
            _userTable = new UserDataTable();
            _userAdapter.Fill(_userTable);
        }

        public bool IsAuthentic(string email, string password)
        {
            foreach (DataRow r in _userTable.Rows)
            {
                
                if (r.Field<string>(3).Equals(email) && encrypt(password).Equals(r.Field<string>(7)))
                {
                    MessageBox.Show(r.Field<string>(3) + " " + r.Field<string>(7));
                    return true;
                }
                    
            }
            return false;
        }

        public bool EmailExists(string txtEmail)
        {
            foreach(DataRow r in _userTable.Rows)
            {
                if (r.Field<String>(3) == txtEmail)
                    return true;
            }
            return false;
        }

        public static string encrypt(string stringToEncrypt)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uft8 = new UTF8Encoding();
                return Convert.ToBase64String(md5.ComputeHash(uft8.GetBytes(stringToEncrypt)));
            }
        }

        public User GetUserById(int id)
        {
            foreach (DataRow r in _userTable.Rows)
            {
                if(r.Field<int>(0) == id)
                {
                    string fname = r.Field<string>(1);
                    string lname = r.Field<string>(1);
                    string email = r.Field<string>(3);
                    string phoneNumber = r.Field<string>(4);
                    bool verified = r.Field<bool>(5);
                    Role role = r.Field<Role>(6);
                    return new User(id, fname, lname, email, phoneNumber, verified, role);
                }
                
            }
            return null;
        }

        public User Login(String email, String password)
        {
            foreach (DataRow r in _userTable.Rows)
            {
                if(r.Field<String>(3) == email && encrypt(password) == r.Field<String>(7)) 
                {
                    int id = r.Field<int>(0);
                    string fname = r.Field<string>(1);
                    string lname = r.Field<string>(1);
                    string phoneNumber = r.Field<string>(4);
                    bool verified = r.Field<bool>(5);
                    return new User(id, fname, lname, email, phoneNumber, verified, (Role) r.Field<int>(6));
                }
            }
            return null;
        }

        public void Register(String email, String password, String fname, String lname, String number)
        {
            _userAdapter.Insert(fname, lname, email, number, false, 0, encrypt(password));
        }

        public List<User> ReadAll()
        {
           List<User> users = new List<User>();
           foreach(DataRow r in _userTable.Rows)
           {
                int id = r.Field<int>(0);
                string fname = r.Field<string>(1);
                string lname = r.Field<string>(1);
                string email = r.Field<string>(3);
                string phoneNumber = r.Field<string>(4);
                bool verified = r.Field<bool>(5);
                Role role = r.Field<Role>(6);
                users.Add(new User(id, fname, lname, email, phoneNumber, verified, role));
           }
            return users;
        }
    }
}