using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestCuisine;
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

        public static List<Cuisine> GetAll()
        {
            List<Cuisine> allItems = new List<Cuisine> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Cuisine;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int newId = rdr.GetInt32(0);
                string newName = rdr.GetString(1);
                Cuisine newItem = new Cuisine(newName, newId);
                allItems.Add(newItem);
            }

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }

            return allItems;
        }

        public static Cuisine Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `Cuisine` WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int newId = 0;
            string newName = "";

            while (rdr.Read())
            {
                newId = rdr.GetInt32(0);
                newName = rdr.GetString(1);
            }

            Cuisine foundItem = new Cuisine(newName, newId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundItem;
        }
        public void AddItem(Restaurants newRestaurant)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO cuisine_restaurants (cuisine_id, restaurant_id) VALUES (@CusineId, @RestaurantId);";

            MySqlParameter cuisine_id = new MySqlParameter();
            cuisine_id.ParameterName = "@CategoryId";
            cuisine_id.Value = id;
            cmd.Parameters.Add(cuisine_id);

            MySqlParameter restaurant_id = new MySqlParameter();
            restaurant_id.ParameterName = "@ItemId";
            restaurant_id.Value = newRestaurant.Id;
            cmd.Parameters.Add(restaurant_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
