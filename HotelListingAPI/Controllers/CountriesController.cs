﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingApi.Data;
using HotelListingApi.Core.Models.Country;
using AutoMapper;
using HotelListingApi.Core.Contracts;
using SQLitePCL;
using Microsoft.AspNetCore.Authorization;
using HotelListingApi.Core.Exceptions;
using HotelListingApi.Core.Models;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(ICountriesRepository countriesRepository, IMapper mapper, ILogger<CountriesController> logger)
        {
            _countriesRepository = countriesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Countries
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {

            var countries = await _countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);

            return records;
            
        }

        // GET: api/Countries/GetPaged/StartIndex=0&PageSize=10
        [HttpGet("GetPaged")]
        public async Task<ActionResult<PageResult<GetCountryDto>>> GetPagedCountries([FromQuery] QueryParameters 
            queryParameters)
        {

            var PagedCountriesResult = await _countriesRepository.GetPagedAll<GetCountryDto>(queryParameters);
            return PagedCountriesResult;

        }

         
        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                //_logger.LogWarning($"No Record Found in {nameof(GetCountry)} with Id : {id}");
                throw new NotFoundException(nameof(GetCountry), id);
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return countryDto;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }

            var country = await _countriesRepository.GetAsync(id);
            if(country == null)
            {
                throw new NotFoundException(nameof(GetCountries), id);
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _countriesRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto CreateCountrydto)
        {
            var country = _mapper.Map<Country>(CreateCountrydto);

            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        //public async Task<bool> CountryExists(int id)
        //{
        //    return await _countriesRepository.Exists(id);
        //}
    }
}
