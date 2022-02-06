using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork uow)
        {
            _uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Room, RoomDTO>();
                cfg.CreateMap<RoomDTO, Room>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        public RoomDTO GetRoomById(int roomId)
        {
            var room = _uow.Rooms.Get(roomId);

            return _mapper.Map<Room, RoomDTO>(room);
        }

        public IEnumerable<RoomDTO> GetRoomsByHotel(int hotelId)
        {
            var rooms = _uow.Rooms.Find(r => r.HotelId == hotelId);
            return _mapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(rooms);

        }

        public IEnumerable<RoomDTO> GetRoomsByRoomType(int roomTypeId)
        {
            var rooms = _uow.Rooms.Find(r => r.RoomTypeId == roomTypeId);
            return _mapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(rooms);
        }
        public void AddRoom(RoomDTO room)
        {
            _uow.Rooms.Create(_mapper.Map<RoomDTO, Room>(room));
        }
        public void UpdateRoom(RoomDTO room)
        {
            _uow.Rooms.Update(_mapper.Map<RoomDTO, Room>(room));
        }

        public bool DeleteRoom(int roomId)
        {
            return _uow.Rooms.Delete(roomId);
        }
        public void SaveChanges()
        {
            _uow.Save();
        }
        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
