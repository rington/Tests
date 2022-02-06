using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IHotelService : IDisposable
    {
        IEnumerable<HotelDTO> GetHotelsByRating(string rating);

        IEnumerable<HotelDTO> GetAllHotels();

        HotelDTO GetHotelById(int hotelId);

        HotelDTO GetHotelByName(string hotelName);

        HotelDTO GetHotelByLocation(string location);

        void AddHotel(HotelDTO hotel);

        void UpdateHotel(HotelDTO hotel);

        bool DeleteHotel(int hotelId);

        void SaveChanges();
    }
}
