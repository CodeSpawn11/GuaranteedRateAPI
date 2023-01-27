using Microsoft.AspNetCore.Mvc;

namespace GauranteedRateApi.Controllers
{
    public class Customer
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Customer(string lastName, string firstName, string email, string favoriteColor, DateTime dateOfBirth)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            FavoriteColor = favoriteColor;
            DateOfBirth = dateOfBirth;
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class RecordsController : ControllerBase
    {

        #region Instructions
        //- POST /records - This endpoint takes a single data line in one of the three formats supported by the existing code(pipe-delimited, comma-delimited, space-delimited), parse it and add it to the records list
        //- GET /records/color - This endpoint returns records sorted by favorite color in JSON format
        //- GET /records/birthdate - This endpoint returns records sorted by birthdate in JSON format
        //- GET /records/name - This endpoint returns records sorted by last name in JSON format
        #endregion

        public static List<Customer> records = new List<Customer>();

        [HttpPost]
        public IActionResult PostRecord([FromBody] string recordData)
        {
            // Parse the record data
            string[] recordFields;
            if (recordData.Contains("|"))
            {
                recordFields = recordData.Split('|');
            }
            else if (recordData.Contains(","))
            {
                recordFields = recordData.Split(',');
            }
            else if (recordData.Contains(" "))
            {
                recordFields = recordData.Split(' ');
            }
            else
            {
                return BadRequest("Invalid record data format.");
            }

            // Create a new record object
            var newRecord = new Customer(recordFields[0], recordFields[1], recordFields[2], recordFields[3], DateTime.Parse(recordFields[4]));

            // Add the new record to the list of records
            records.Add(newRecord);

            return Ok(new { message = "Record added successfully." });
        }

        [HttpGet]
        [Route("color")]
        public IActionResult GetRecordsByColor()
        {
            // Sort the records by favorite color
            var sortedRecords = records.OrderBy(x => x.FavoriteColor);

            return Ok(sortedRecords);
        }

        [HttpGet]
        [Route("birthdate")]
        public IActionResult GetRecordsByBirthdate()
        {
            // Sort the records by birthdate
            var sortedRecords = records.OrderBy(x => x.DateOfBirth);

            return Ok(sortedRecords);
        }

        [HttpGet]
        [Route("name")]
        public IActionResult GetRecordsByName()
        {
            // Sort the records by last name
            var sortedRecords = records.OrderBy(x => x.LastName);

            return Ok(sortedRecords);
        }
    }
}