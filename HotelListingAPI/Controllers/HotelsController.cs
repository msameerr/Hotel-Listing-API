using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingApi.Data;
using HotelListingApi.Core.Contracts;
using AutoMapper;
using HotelListingApi.Core.Models.Hotel;
using HotelListingApi.Core.Models;

namespace HotelListingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            var records = _mapper.Map<List<HotelDto>>(hotels);
            return records;
        }



        // GET: api/Hotels/GetPaged/StartIndex=0&PageSize=10
        [HttpGet("GetPaged")]
        public async Task<ActionResult<PageResult<HotelDto>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            var PagedhotelsResult = await _hotelRepository.GetPagedAll<HotelDto>(queryParameters);
            return PagedhotelsResult;
        }


        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<HotelDto>(hotel);
            return record;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto updateHotelDto)
        {
            if (id != updateHotelDto.HotelId)
            {
                return BadRequest();
            }

            var hotel = await _hotelRepository.GetAsync(id);
            if(hotel == null)
            {
                return NotFound();
            }
            _mapper.Map(updateHotelDto, hotel);

            try
            {
                await _hotelRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _hotelRepository.Exists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotelDto)
        {
            var record = _mapper.Map<Hotel>(createHotelDto);
            await _hotelRepository.AddAsync(record);

            return CreatedAtAction("GetHotel", new { id = record.HotelId }, record);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelRepository.DeleteAsync(id);

            return NoContent();
        }

        //private bool HotelExists(int id)
        //{
        //    return _context.Hotels.Any(e => e.HotelId == id);
        //}
    }
}
