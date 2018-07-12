using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestCuisine;
using System;

namespace BestCuisine.Models
{
    public class ViewModel
    {
        private Restaurants restaurant;
        private Cuisine cuisine;
        public static List<ViewModel> list = new List<ViewModel>() { };

        public ViewModel(Restaurants restaurant, Cuisine cuisine)
        {
            this.restaurant = restaurant;
            this.cuisine = cuisine;
            list.Add(this);
        }

        public static List<ViewModel> GetObjects()
        {
            Restaurants.GetCuisineJoin();
            return list;
        }

        public static List<ViewModel> GetCuisines(int id)
        {
            Restaurants.SearchByCuisine(id);
            return list;
        }

        public string GetRestaurantName()
        {
            return restaurant.Name;    
        }

        public int GetRestaurantId()
        {
            
            return restaurant.Id;
        }

        public string GetCuisineName()
        {
            return cuisine.Name;
        }

        public int GetCuisineId()
        {
            return cuisine.GetId();
        }
    }
}

