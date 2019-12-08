using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace Italianos.Logic
{
    public enum Role { USER = 0, ADMIN = 1 }
    public class User
    {
        public int UserId { get; internal set; }
        public String FirstName { get; internal set; }
        public String LastName { get; internal set; }
        public String Email { get; internal set; }
        public String PhoneNumber { get; internal set; }
        public bool Verified { get; internal set; }
        public Role Role { get; internal set; }


        public User(int id, String fname, String lname, String email, String number, bool verified, Role role)
        {
            UserId = id;
            FirstName = fname;
            LastName = lname;
            Email = email;
            PhoneNumber = number;
            Verified = verified;
            Role = role;
        }
        public override String ToString()
        {
            return $"ID: {UserId}, Name: {FirstName} {LastName}, Email: {Email}, " +
                $"Phone Number: {PhoneNumber}";
        }
    }
}
