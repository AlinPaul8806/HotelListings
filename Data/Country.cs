using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public class Country
    {
        // if you will name it Id, EF will automatically assume that it can use it as Primary Key
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
