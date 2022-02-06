using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public RoomTypeService(IUnitOfWork uow)
        {
            _uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoomType, RoomTypeDTO>();
                cfg.CreateMap<RoomTypeDTO, RoomType>();
            });

            _mapper = mapperConfig.CreateMapper();
        }
        public RoomTypeDTO GetRoomTypeById(int roomTypeId)
        {
            var roomType = _uow.RoomTypes.Get(roomTypeId);

            return _mapper.Map<RoomType, RoomTypeDTO>(roomType);
        }

        public IEnumerable<RoomTypeDTO> GetRoomTypesByPrice(double minPrice, double maxPrice)
        {
            var roomTypes = _uow.RoomTypes.Find(rt => rt.Price >= minPrice && rt.Price <= maxPrice);
            return _mapper.Map<IEnumerable<RoomType>, IEnumerable<RoomTypeDTO>>(roomTypes);

        }
        public IEnumerable<RoomTypeDTO> GetAllRoomTypes()
        {
            var roomTypes = _uow.RoomTypes.GetAll();

            return _mapper.Map<IEnumerable<RoomType>, IEnumerable<RoomTypeDTO>>(roomTypes);
        }

        public void AddRoomType(RoomTypeDTO roomType)
        {
            _uow.RoomTypes.Create(_mapper.Map<RoomTypeDTO, RoomType>(roomType));
        }
        public void UpdateRoomType(RoomTypeDTO roomType)
        {
            _uow.RoomTypes.Update(_mapper.Map<RoomTypeDTO, RoomType>(roomType));
        }

        public bool DeleteRoomType(int roomTypeId)
        {
            return _uow.RoomTypes.Delete(roomTypeId);
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
