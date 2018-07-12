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
            Restaurants newRestaurant = new Restaurants("name", 0);
            newRestaurant.GetCuisineJoin();
            return list;
        }

        public string GetRestaurantName()
        {
            return restaurant.Name;    
        }

        public string GetCuisineName()
        {
            return cuisine.Name;
        }
    }
}

