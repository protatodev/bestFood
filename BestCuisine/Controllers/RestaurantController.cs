using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BestCuisine.Models;

namespace BestCuisine.Controllers
{
    public class RestaurantController : Controller
    {
        
        [HttpGet("/restaurants/new")]
        public ActionResult Create()
        {
            List<Cuisine> allItems = new List<Cuisine>();
            allItems = Cuisine.GetAll();
            return View(allItems);
        }

        [HttpPost("/restaurant")]
        public ActionResult input()
        {
            string newPlace = Request.Form["restaurant"];
            Restaurants newRestaurant = new Restaurants(newPlace);
            newRestaurant.Save();

            int id = int.Parse(Request.Form["selection"]);
            Cuisine cuisine = Cuisine.Find(id);
            newRestaurant.AddCuisine(cuisine);

            ViewModel collection = new ViewModel(newRestaurant, cuisine);

            return View("ViewAll", ViewModel.GetObjects());
        }
    }
}