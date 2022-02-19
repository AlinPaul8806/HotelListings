using AutoMapper;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        // inject logger and unit of work:
        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            // create "local" copies of unit of work + logger
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                // map from entity to DTO:
                var results = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(results); // results is of type CountryDTO
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotels)}.");
                return StatusCode(500, "Internal Server error. Something went wrong."); // 500 is the universal error for internal server error.
            }
        }

        [HttpGet("{id:int}")] // this is the tempalte for the GET(id)
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id, new List<string> { "Country" }); // q= the token in lambda
                // map from entity to DTO:
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(result); // result is of type CountryDTO
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotel)}.");
                return StatusCode(500, "Internal Server error. Something went wrong."); // 500 is the universal error for internal server error.
            }
        }

    }
}
