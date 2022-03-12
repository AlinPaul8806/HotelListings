using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class RequestParams
    {
        // field
        // how many maximul records will you display per page?
        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1; //default page number

        // default page size is 10, the max is 50
        private int _pageSize = 10;

        public int PageSize
        {
            get 
            {
                return _pageSize;
            }
            set 
            {
                // if the client requests a number greater that the max page size, return max page size(50), other, return value
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
