using Italianos.Logic;
using System;

namespace Logic
{
    public enum Status { Reserved = 0, Completed = 1, Canceled = 2}
    public class Reservation
    {
        public int ReservationId { get; internal set; }
        public User Host { get; internal set; }
        public int TableNumber { get; internal set; }
        public DateTime ReservationDate { get; internal set; }
        public Item Appetizer { get; internal set; }
        public Item Main { get; internal set; }
        public Item Dessert { get; internal set; }
        public int NumberOfGuests { get; internal set; }
        public DateTime RequestDate { get; internal set; }
        public Status Status { get; internal set; }
        public Reservation(int resId, User host, int tableNum, DateTime resDate, Item appetizer,
                            Item main, Item dessert, int numOfGuests, DateTime reqDate, Status status)
        {
            ReservationId = resId;
            Host = host;
            TableNumber = tableNum;
            ReservationDate = resDate;
            Appetizer = appetizer;
            Main = main;
            Dessert = dessert;
            NumberOfGuests = numOfGuests;
            RequestDate = reqDate;
            Status = status;
        }

        public override string ToString()
        {
            return $"Reservation '{ReservationId}', Host '{Host.FirstName}', Table Number '{TableNumber}', Appetizer: {Appetizer}, Main: {Main}, Dessert: {Dessert}, Number of Guests: {NumberOfGuests}";
        }
    }
}
