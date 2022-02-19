using AutoMapper;
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

        [HttpGet("{id:int}")] // this is the tempalte for the GET(id)
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels"}); // q= the token in lambda
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
    }
}
