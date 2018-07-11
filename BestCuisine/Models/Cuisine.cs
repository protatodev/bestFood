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

    }
}
