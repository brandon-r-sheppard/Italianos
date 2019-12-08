using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Italianos.Logic
{
    public enum Category { Meats, Pastas, Breads, Soups, Salads, Desserts}
    public enum Course { Appetizer, Main, Dessert}
    public class Item
    {
        public String Name { get; internal set; }
        public Category Category { get; internal set; }
        public Boolean Available { get; internal set; }
        public Course Course { get; internal set; }

        public Item(string name, Category category, bool available, Course course)
        {
            Name = name;
            Category = category;
            Available = available;
            Course = course;
        }
        
        public override String ToString()
        {
            return $"{Name}, {Category}, {Available}, {Course}";
        }
    }
}