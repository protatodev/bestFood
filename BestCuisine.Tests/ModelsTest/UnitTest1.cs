using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestCuisine.Models;
using System;
using System.Collections.Generic;

namespace BestCuisine.Tests
{

    [TestClass]
    public class BestCuisineTests : IDisposable
    {
        public void Dispose()
        {
            Restaurants.DeleteAll();
            // Category.DeleteAll();
        }

        public BestCuisineTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=CuisineTest;";
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = Cuisine.GetAll().Count;

            //Assert
            Assert.AreEqual(7, result);
        }

        /*
        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
        {
            // Arrange, Act
            Restaurants firstItem = new Restaurants("Mow the lawn", 1);
            Restaurants secondItem = new Restaurants("Mow the lawn", 1);

            // Assert
            Assert.AreEqual(firstItem, secondItem);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ItemList()
        {
            //Arrange
            Restaurants testItem = new Restaurants("Mow the lawn", 1);

            //Act
            testItem.Save();
            List<Restaurants> result = Restaurants.GetAll();
            List<Restaurants> testList = new List<Restaurants> { testItem };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //Arrange
            Restaurants testItem = new Restaurants("Mow the lawn", 1);

            //Act
            testItem.Save();
            Restaurants savedItem = Restaurants.GetAll()[0];

            int result = savedItem.GetId();
            int testId = testItem.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsItemInDatabase_Item()
        {
            //Arrange
            Restaurants testItem = new Restaurants("Mow the lawn", 1);
            testItem.Save();

            //Act
            Restaurants foundItem = Restaurants.Find(testItem.GetId());

            //Assert
            Assert.AreEqual(testItem, foundItem);
        }

        [TestMethod]
        public void Edit_UpdatesItemInDatabase_String()
        {
            //Arrange
            string firstDescription = "Walk the Dog";
            Restaurants testItem = new Restaurants(firstDescription, 1);
            testItem.Save();
            string secondDescription = "Mow the lawn";

            //Act

            testItem.Edit(secondDescription);
            string result = Restaurants.Find(testItem.GetId()).GetDescription();

            //Assert
            Assert.AreEqual(secondDescription, result);
        }
        */
    }
}