using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestCuisine;
using System;

namespace BestCuisine.Models
{
    public class Restaurants
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public Restaurants(string name, int id = 0)
        {
            this.Name = name;
            this.Id = Id;
        }

        public void AddCuisine(Cuisine newCuisine)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO cuisine_restaurants (cuisine_id, restaurant_id) VALUES (@CuisineId, @RestaurantId);";

            MySqlParameter cuisine_id = new MySqlParameter();
            cuisine_id.ParameterName = "@CuisineId";
            cuisine_id.Value = newCuisine.GetId();
            cmd.Parameters.Add(cuisine_id);

            MySqlParameter restaurant_id = new MySqlParameter();
            restaurant_id.ParameterName = "@RestaurantId";
            restaurant_id.Value = Id;
            cmd.Parameters.Add(restaurant_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


        public List<Cuisine> GetCuisines()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT cuisine_id FROM cuisine_restaurants WHERE restaurant_id = @RestaurantId;";

            MySqlParameter restaurantIdParameter = new MySqlParameter();
            restaurantIdParameter.ParameterName = "@RestaurantId";
            restaurantIdParameter.Value = Id;
            cmd.Parameters.Add(restaurantIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<int> cuisineIds = new List<int> { };
            while (rdr.Read())
            {
                int cuisineId = rdr.GetInt32(0);
                cuisineIds.Add(cuisineId);
            }
            rdr.Dispose();

        List<Cuisine> cuisines = new List<Cuisine> { };
        foreach (int cuisine in cuisineIds)
        {
            var cuisineQuery = conn.CreateCommand() as MySqlCommand;
            cuisineQuery.CommandText = @"SELECT * FROM Cuisine WHERE id = @CuisineId;";

            MySqlParameter cuisineIdParameter = new MySqlParameter();
            cuisineIdParameter.ParameterName = "@CuisineId";
            cuisineIdParameter.Value = cuisines;
            cuisineQuery.Parameters.Add(cuisineIdParameter);

            var cuisineQueryRdr = cuisineQuery.ExecuteReader() as MySqlDataReader;
            while (cuisineQueryRdr.Read())
            {
                int thisCuisineId = cuisineQueryRdr.GetInt32(0);
                string cuisineName = cuisineQueryRdr.GetString(1);
                Cuisine foundCuisine = new Cuisine(cuisineName, thisCuisineId);
                cuisines.Add(foundCuisine);
            }
            cuisineQueryRdr.Dispose();
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return cuisines;
    }

        /*
        public void Edit(string newDescription)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@newDescription";
            description.Value = newDescription;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            this.description = newDescription;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        */

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM items WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Restaurants> GetAll()
        {
            List<Restaurants> allItems = new List<Restaurants> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Restaurants;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int newId = rdr.GetInt32(0);
                string newName = rdr.GetString(1);
                Restaurants newItem = new Restaurants(newName, newId);
                allItems.Add(newItem);
            }

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }

            return allItems;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM Restaurants;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherItem)
        {
            if (!(otherItem is Restaurants))
            {
                return false;
            }
            else
            {
                Restaurants newItem = (Restaurants)otherItem;
                bool idEquality = (this.Id == newItem.Id);
                bool nameEquality = (this.Name == newItem.Name);

                return (idEquality && nameEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO Restaurants (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "name";
            name.Value = this.Name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Restaurants Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `Restaurants` WHERE id = @thisId;";

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

            Restaurants foundItem = new Restaurants(newName, newId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundItem;
        }
    }
}
