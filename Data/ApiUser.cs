using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    // inherits from Microsoft.AspNetCore.Identity.EntityFrameworkCore IdentityUser class, where you have all the neccesary properties
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
    }
}
