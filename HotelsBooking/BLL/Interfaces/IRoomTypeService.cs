using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IRoomTypeService : IDisposable
    {
        IEnumerable<RoomTypeDTO> GetAllRoomTypes();

        IEnumerable<RoomTypeDTO> GetRoomTypesByPrice(double minPrice, double maxPrice);

        RoomTypeDTO GetRoomTypeById(int roomTypeId);

        void AddRoomType(RoomTypeDTO roomType);

        void UpdateRoomType(RoomTypeDTO roomType);

        bool DeleteRoomType(int roomTypeId);

        void SaveChanges();
    }
}
