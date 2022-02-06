using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IRoomService : IDisposable
    {
        IEnumerable<RoomDTO> GetRoomsByRoomType(int roomTypeId);

        IEnumerable<RoomDTO> GetRoomsByHotel(int hotelId);

        RoomDTO GetRoomById(int roomId);

        void AddRoom(RoomDTO room);

        void UpdateRoom(RoomDTO room);

        bool DeleteRoom(int roomId);
        
        void SaveChanges();
    }
}
