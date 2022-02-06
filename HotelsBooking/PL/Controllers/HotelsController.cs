using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    public class HotelsController : ApiController
    {
        private readonly IHotelService _hotelsService;

        private readonly IMapper _mapper;

        public HotelsController(IHotelService service)
        {
            _hotelsService = service;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, HotelView>();
                cfg.CreateMap<HotelView, HotelDTO>();
            });

            _mapper = config.CreateMapper();
        }

        [Route("api/hotels/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var hotel = _hotelsService.GetHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelView = _mapper.Map<HotelDTO, HotelView>(hotel);

            return Ok(hotelView);
        }

        [Route("api/hotels")]
        [HttpGet]
        public IEnumerable<HotelView> GetAll()
        {
            var hotels = _hotelsService.GetAllHotels();

            return _mapper.Map<IEnumerable<HotelDTO>, IEnumerable<HotelView>>(hotels);
        }

        [Route("api/hotels")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]HotelView hotel)
        {
            var hotelDTO = _mapper.Map<HotelView, HotelDTO>(hotel);

            _hotelsService.AddHotel(hotelDTO);

            _hotelsService.SaveChanges();

            return Ok();
        }

        [Route("api/hotels")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]HotelView hotel)
        {
            var hotelDTO = _mapper.Map<HotelView, HotelDTO>(hotel);

            _hotelsService.UpdateHotel(hotelDTO);

            _hotelsService.SaveChanges();

            return Ok();
        }

        [Route("api/hotels/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (_hotelsService.DeleteHotel(id))
            {
                _hotelsService.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        [Route("api/hotels/name/{name}")]
        [HttpGet]
        public HotelView GetHotelByName(string name)
        {
            var hotels = _hotelsService.GetHotelByName(name);

            return _mapper.Map<HotelDTO, HotelView>(hotels);
        }

        [Route("api/hotels/location/{location}")]
        [HttpGet]
        public HotelView GetHotelByLocation(string location)
        {
            var hotels = _hotelsService.GetHotelByLocation(location);

            return _mapper.Map<HotelDTO, HotelView>(hotels);
        }

        [Route("api/hotels/rating/{rating}")]
        [HttpGet]
        public IEnumerable<HotelView> GetHotelByRating(string rating)
        {
            var hotels = _hotelsService.GetHotelsByRating(rating);

            return _mapper.Map<IEnumerable<HotelDTO>, IEnumerable<HotelView>>(hotels);
        }
    }
}
