using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class UpdateCountryDTO : CreateCountryDTO
    {
        public IList<CreateHotelDTO> Hotels { get; set; } //hotels in this country
    }
}
