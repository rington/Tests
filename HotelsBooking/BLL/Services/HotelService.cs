using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork uow)
        {
            _uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelDTO, Hotel>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        public HotelDTO GetHotelById(int hotelId)
        {
            var hotel = _uow.Hotels.Get(hotelId);
            return _mapper.Map<Hotel, HotelDTO>(hotel);
        }

        public IEnumerable<HotelDTO> GetAllHotels()
        {
            var hotels = _uow.Hotels.GetAll();
            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(hotels);
        }
        public void AddHotel(HotelDTO hotel)
        {
            _uow.Hotels.Create(_mapper.Map<HotelDTO, Hotel>(hotel));
        }
        public void UpdateHotel(HotelDTO hotel)
        {
            _uow.Hotels.Update(_mapper.Map<HotelDTO, Hotel>(hotel));
        }

        public bool DeleteHotel(int hotelId)
        {
            return _uow.Hotels.Delete(hotelId);
        }

        public HotelDTO GetHotelByName(string hotelName)
        {
            var hotel = _uow.Hotels.Find(h => h.Name == hotelName).First();
            return _mapper.Map<Hotel, HotelDTO>(hotel);
        }

        public HotelDTO GetHotelByLocation(string location)
        {
            var hotel = _uow.Hotels.Find(h => h.Location == location).First();
            return _mapper.Map<Hotel, HotelDTO>(hotel);
        }

        public IEnumerable<HotelDTO> GetHotelsByRating(string rating)
        {
            var hotel = _uow.Hotels.Find(h => h.Rating == rating);
            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(hotel);
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
