using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using GauranteedRateApi.Controllers;
using static GuaranteedRateApiTests.RecordsControllerTests;

namespace GuaranteedRateApiTests
{

    public class RecordsControllerTests
    {

        public class Record
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public string FavoriteColor { get; set; }
            public DateTime DateOfBirth { get; set; }

            public Record(string lastName, string firstName, string email, string favoriteColor, DateTime dateOfBirth)
            {
                LastName = lastName;
                FirstName = firstName;
                Email = email;
                FavoriteColor = favoriteColor;
                DateOfBirth = dateOfBirth;
            }
        }

        private RecordsController controller;

        public RecordsControllerTests()
        {
            controller = new RecordsController();
        }

        [Fact]
        public void PostRecord_WithValidData_ReturnsOk()
        {
            var recordData = "LastName | FirstName | Email | FavoriteColor | 01/01/2000";

            var result = controller.PostRecord(recordData);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostRecord_WithInvalidDataFormat_ReturnsBadRequest()
        {
            var recordData = "LastName-FirstName-Email-FavoriteColor-01/01/2000";

            var result = controller.PostRecord(recordData);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetRecordsByColor_ReturnsSortedRecords()
        {

            //Check to see if records has anything and delete it
            RecordsController.records.Clear();

            // Add some records to the list
            RecordsController.records.Add(new Customer("Smith", "John", "jsmith@gmail.com", "Green", new DateTime(1980, 1, 1)));
            RecordsController.records.Add(new Customer("Johnson", "Jane", "jjohnson@gmail.com", "Blue", new DateTime(1985, 1, 1)));
            RecordsController.records.Add(new Customer("Williams", "Bob", "bwilliams@gmail.com", "Red", new DateTime(1990, 1, 1)));

            List<Customer> castedList = RecordsController.records.OfType<Customer>().ToList();

            var result = controller.GetRecordsByColor() as OkObjectResult;

            Assert.Equal("Green", castedList[0].FavoriteColor);
            Assert.Equal("Blue", castedList[1].FavoriteColor);
            Assert.Equal("Red", castedList[2].FavoriteColor);
        }

        [Fact]
        public void GetRecordsByBirthdate_ReturnsSortedRecords()
        {

            //Check to see if records has anything and delete it
            RecordsController.records.Clear();

            // Add some records to the list
            RecordsController.records.Add(new Customer("Smith", "John", "jsmith@gmail.com", "Green", new DateTime(1980, 1, 1)));
            RecordsController.records.Add(new Customer("Johnson", "Jane", "jjohnson@gmail.com", "Blue", new DateTime(1985, 1, 1)));
            RecordsController.records.Add(new Customer("Williams", "Bob", "bwilliams@gmail.com", "Red", new DateTime(1990, 1, 1)));


            List<Customer> castedList = RecordsController.records.OfType<Customer>().ToList();

            var result = controller.GetRecordsByBirthdate() as OkObjectResult;

            Assert.Equal(new DateTime(1980, 1, 1), castedList[0].DateOfBirth);
            Assert.Equal(new DateTime(1985, 1, 1), castedList[1].DateOfBirth);
            Assert.Equal(new DateTime(1990, 1, 1), castedList[2].DateOfBirth);
        }

        [Fact]
        public void GetRecordsByName_ReturnsSortedRecords()
        {

            //Check to see if records has anything and delete it
            RecordsController.records.Clear();

            // Add some records to the list
            RecordsController.records.Add(new Customer("Smith", "John", "jsmith@gmail.com", "Green", new DateTime(1980, 1, 1)));
            RecordsController.records.Add(new Customer("Johnson", "Jane", "jjohnson@gmail.com", "Blue", new DateTime(1985, 1, 1)));
            RecordsController.records.Add(new Customer("Williams", "Bob", "bwills@gmail.com", "Red", new DateTime(1990, 1, 1)));

            List<Customer> castedList = RecordsController.records.OfType<Customer>().ToList();

            var result = controller.GetRecordsByName() as OkObjectResult;

            //var records = result.Value as IEnumerable<Record>;

            Assert.Equal("Smith", castedList[0].LastName);
            Assert.Equal("Johnson", castedList[1].LastName);
            Assert.Equal("Williams", castedList[2].LastName);
        }
    }
}