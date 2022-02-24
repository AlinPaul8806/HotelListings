using AutoMapper;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        // inject logger and unit of work:
        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            // create "local" copies of unit of work + logger
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                // map from entity to DTO:
                var results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results); // results is of type CountryDTO
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}.");
                return StatusCode(500, "Internal Server error. Something went wrong."); // 500 is the universal error for internal server error.
            }
        }

        [HttpGet("{id:int}", Name = "GetCountry")] // this is the tempalte for the GET(id)
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Countries" }); // q= the token in lambda
                // map from entity to DTO:
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result); // result is of type CountryDTO
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}.");
                return StatusCode(500, "Internal Server error. Something went wrong."); // 500 is the universal error for internal server error.
            }
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO createCountryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = _mapper.Map<Country>(createCountryDTO);
                await _unitOfWork.Countries.Insert(country);  // insert my object of type country
                await _unitOfWork.Save(); // commit the transaction

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateCountry)}.");
                return StatusCode(500, "Internal Server error. Something went wrong.");
            }
        }


        //[Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO updateCountryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, null);
                if (country == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                    return BadRequest("Submitted data is invalid.");
                }

                _mapper.Map(updateCountryDTO, country); // map the SOURCE and THE DESTINATION
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.Save();

                return NoContent(); //I don't have anything to tell you :)

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        //// this is the tempalte for the DELETE(id) 
        ////Name =... user for other action methods that might call this one
        //[Authorize]
        //[Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}", Name = "DeleteCountry")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, null); // q= the token in lambda

                if (country == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                    return BadRequest();
                }

                await _unitOfWork.Countries.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteCountry)}.");
                return StatusCode(500, "Internal Server error. Something went wrong.");
            }
        }
    }
}
