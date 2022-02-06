using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IReservationService : IDisposable
    {
        IEnumerable<ReservationDTO> GetAllReservations();

        IEnumerable<ReservationDTO> GetReservationsByDate(DateTime bookingDate, DateTime bookingDateEnd);

        ClientDTO GetClientByReservation(int reservationId);

        RoomDTO GetRoomByReservation(int reservationId);

        ReservationDTO GetReservationById(int reservationId);

        void AddReservation(ReservationDTO reservation);

        void UpdateReservation(ReservationDTO reservation);

        bool DeleteReservation(int reservationId);
  
        void SaveChanges();
    }
}
