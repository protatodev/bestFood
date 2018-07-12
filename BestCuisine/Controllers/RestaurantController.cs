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

        [HttpGet("/restaurant")]
        public ActionResult ViewAll()
        {

            return View(ViewModel.GetObjects());
        }

        [HttpGet("/search")]
        public ActionResult SearchForm()
        {

            return View();
        }


        [HttpPost("/restaurant")]
        public ActionResult input()
        {
            string newPlace = Request.Form["restaurant"];
            Restaurants newRestaurant = new Restaurants(newPlace, 0);
            newRestaurant.Save();

            int id = int.Parse(Request.Form["selection"]);
            Cuisine cuisine = Cuisine.Find(id);
            newRestaurant.AddCuisine(cuisine);

            ViewModel collection = new ViewModel(newRestaurant, cuisine);

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/delete/{id}")]
        public ActionResult Delete(int id)
        {
            Restaurants thisPlace = Restaurants.Find(id);
            thisPlace.Delete();

            return RedirectToAction("ViewAll");
        }
    }
}