/*Models = DTOs. The difference is that here, you can add validations*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    // inherited from CreateCountryDTO
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
    }
}
