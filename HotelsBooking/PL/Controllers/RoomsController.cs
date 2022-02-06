using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    public class RoomsController : ApiController
    {
        private readonly IRoomService _roomsService;

        private readonly IMapper _mapper;

        public RoomsController(IRoomService service)
        {
            _roomsService = service;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoomDTO, RoomView>();
                cfg.CreateMap<RoomView, RoomDTO>();
            });

            _mapper = config.CreateMapper();
        }

        [Route("api/rooms/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var room = _roomsService.GetRoomById(id);

            if (room == null)
            {
                return NotFound();
            }

            var roomView = _mapper.Map<RoomDTO, RoomView>(room);

            return Ok(roomView);
        }
        
        [Route("api/rooms")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]RoomView room)
        {
            var roomDTO = _mapper.Map<RoomView, RoomDTO>(room);

            _roomsService.AddRoom(roomDTO);

            _roomsService.SaveChanges();

            return Ok();
        }

        [Route("api/rooms")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]RoomView room)
        {
            var roomDTO = _mapper.Map<RoomView, RoomDTO>(room);

            _roomsService.UpdateRoom(roomDTO);

            _roomsService.SaveChanges();

            return Ok();
        }

        [Route("api/rooms/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (_roomsService.DeleteRoom(id))
            {
                _roomsService.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        [Route("api/rooms/hotel/{hotelId:int}")]
        [HttpGet]
        public IEnumerable<RoomView> GetRoomsByHotel(int hotelId)
        {
            var rooms = _roomsService.GetRoomsByHotel(hotelId);

            return _mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomView>>(rooms);
        }

        [Route("api/rooms/roomType/{roomTypeId:int}")]
        [HttpGet]
        public IEnumerable<RoomView> GetRoomsByRoomType(int roomTypeId)
        {
            var rooms = _roomsService.GetRoomsByRoomType(roomTypeId);

            return _mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomView>>(rooms);
        }
    }
}
