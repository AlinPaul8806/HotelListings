using AutoMapper;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Authorization;
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
                return Ok(results); // results is of type CountryDTOf
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotels)}.");
                return StatusCode(500, "Internal Server error. Something went wrong."); // 500 is the universal error for internal server error.
            }
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetHotel")] // this is the tempalte for the GET(id) //Name =... user for other action methods that might call this one
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO createHotelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(createHotelDTO);
                await _unitOfWork.Hotels.Insert(hotel);  // insert my object of type hotel
                await _unitOfWork.Save(); // commit the transaction

                return CreatedAtRoute("GetHotel", new { id = hotel.Id}, hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateHotel)}.");
                return StatusCode(500, "Internal Server error. Something went wrong.");
            }

        }

    }
}
