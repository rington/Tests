using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    public class RoomTypesController : ApiController
    {
        private readonly IRoomTypeService _roomTypesService;

        private readonly IMapper _mapper;

        public RoomTypesController(IRoomTypeService service)
        {
            _roomTypesService = service;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoomTypeDTO, RoomTypeView>();
                cfg.CreateMap<RoomTypeView, RoomTypeDTO>();
            });

            _mapper = config.CreateMapper();
        }

        [Route("api/roomTypes/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var roomType = _roomTypesService.GetRoomTypeById(id);

            if (roomType == null)
            {
                return NotFound();
            }

            var roomTypeView = _mapper.Map<RoomTypeDTO, RoomTypeView>(roomType);

            return Ok(roomTypeView);
        }

        [Route("api/roomTypes")]
        [HttpGet]
        public IEnumerable<RoomTypeView> GetAll()
        {
            var roomTypes = _roomTypesService.GetAllRoomTypes();

            return _mapper.Map<IEnumerable<RoomTypeDTO>, IEnumerable<RoomTypeView>>(roomTypes);
        }

        [Route("api/roomTypes/price/{minPrice}/{maxPrice}")]
        [HttpGet]
        public IEnumerable<RoomTypeView> GetRoomTypeByPrice(double minPrice, double maxPrice)
        {
            var roomTypes = _roomTypesService.GetRoomTypesByPrice(minPrice, maxPrice);

            return _mapper.Map<IEnumerable<RoomTypeDTO>, IEnumerable<RoomTypeView>>(roomTypes);
        }

        [Route("api/roomTypes")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]RoomTypeView roomType)
        {
            var roomTypeDTO = _mapper.Map<RoomTypeView, RoomTypeDTO>(roomType);

            _roomTypesService.AddRoomType(roomTypeDTO);

            _roomTypesService.SaveChanges();

            return Ok();
        }

        [Route("api/roomTypes")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]RoomTypeView roomType)
        {
            var roomTypeDTO = _mapper.Map<RoomTypeView, RoomTypeDTO>(roomType);

            _roomTypesService.UpdateRoomType(roomTypeDTO);

            _roomTypesService.SaveChanges();

            return Ok();
        }

        [Route("api/roomTypes/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (_roomTypesService.DeleteRoomType(id))
            {
                _roomTypesService.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
    }
}
