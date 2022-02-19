using HotelListing.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    // inherited from CreateHotelDTO
    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }

        public Country Country { get; set; }
    }
}
