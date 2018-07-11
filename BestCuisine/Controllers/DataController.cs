using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BestCuisine.Models;

namespace BestCuisine.Controllers
{
    public class DataController : Controller
    {


        //[HttpGet("/form")]
        //public ActionResult CuisineForm()
        //{
        //    return View();
        //}

        [HttpGet("/restaurants/new")]
        public ActionResult Create()
        {
            List<Cuisine> allItems = new List<Cuisine>();
            allItems = Cuisine.GetAll();
            return View(allItems);
        }

        //[HttpPost("/click_cuisine")]
        //public ActionResult Create()
        //{
            


        //    return View("CuisineForm", allItems);
        //}


    }
}