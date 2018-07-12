using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BestCuisine.Models;

namespace BestCuisine.Controllers
{
    public class CuisineController : Controller
    {
        [HttpGet("/search/cuisine")]
        public ActionResult SearchCuisine()
        {
            List<Cuisine> allItems = new List<Cuisine>();
            allItems = Cuisine.GetAll();

            return View(allItems);
        
        }

        [HttpPost("/search/cuisine/results")]
        public ActionResult SearchResults()
        {
            int Id = int.Parse(Request.Form["selection"]);

            return View(Restaurants.SearchByCuisine(Id));

        }
    
    }
}