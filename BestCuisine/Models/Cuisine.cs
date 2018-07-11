using System;
namespace BestCuisine.Models
{
    public class Cuisine
    {
        public string Name { get; set; }
        private int id = 0;

        public Cuisine(string name, int id =0)
        {
            this.Name = name;
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }
    }
}
