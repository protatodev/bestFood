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


        public Restaurants(string name, int id)
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
            cmd.CommandText = @"DELETE FROM Restaurants WHERE id = @searchId; Delete From cuisine_restaurants WHERE restaurant_id = @searchId;";

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



        public static void GetCuisineJoin()
        {
            MySqlConnection conn = DB.Connection(); conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM
                                 Restaurants JOIN cuisine_restaurants ON (Restaurants.id = cuisine_restaurants.restaurant_id)
                                        JOIN Cuisine ON (cuisine_restaurants.cuisine_id = Cuisine.id);";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            //List<ViewModel> modelList = new List<ViewModel> { };

            while (rdr.Read())
            {
                string restaurantName = rdr.GetString(1);
                int restaurantId = rdr.GetInt32(4);
                string cuisineName = rdr.GetString(6);
                int cuisineId = rdr.GetInt32(3);

                Restaurants newRestaurant = new Restaurants(restaurantName, restaurantId);
                Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);

                ViewModel newList = new ViewModel(newRestaurant, newCuisine); 
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            //return modelList;
        }

        public static List<ViewModel> SearchByCuisine(int Id)
        {
            MySqlConnection conn = DB.Connection(); conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM
                                 Restaurants JOIN cuisine_restaurants ON (Restaurants.id = cuisine_restaurants.restaurant_id)
                                        JOIN Cuisine ON (cuisine_restaurants.cuisine_id = Cuisine.id)
                                             WHERE Cuisine.id = @Id;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@Id";
            thisId.Value = Id;
            cmd.Parameters.Add(thisId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<ViewModel> modelList = new List<ViewModel> { };

            while (rdr.Read())
            {
                string restaurantName = rdr.GetString(1);
                int restaurantId = rdr.GetInt32(4);
                string cuisineName = rdr.GetString(6);
                int cuisineId = rdr.GetInt32(3);

                Restaurants newRestaurant = new Restaurants(restaurantName, restaurantId);
                Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
                ViewModel newList = new ViewModel(newRestaurant, newCuisine);
                modelList.Add(newList);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return modelList;
        }



    }
}
