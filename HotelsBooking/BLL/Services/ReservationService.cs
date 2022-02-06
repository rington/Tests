using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork uow)
        {
            _uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Reservation, ReservationDTO>();
                cfg.CreateMap<ReservationDTO, Reservation>();
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<Room, RoomDTO>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        public ReservationDTO GetReservationById(int reservationId)
        {
            var reservation = _uow.Reservations.Get(reservationId);
            
            return _mapper.Map<Reservation, ReservationDTO>(reservation);
        }

        public IEnumerable<ReservationDTO> GetAllReservations()
        {
            var reservations = _uow.Reservations.GetAll();
            
            return _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationDTO>>(reservations);
        }

        public void AddReservation(ReservationDTO reservation)
        {
            _uow.Reservations.Create(_mapper.Map<ReservationDTO, Reservation>(reservation));
        }

        public void UpdateReservation(ReservationDTO reservation)
        {
			_uow.Reservations.Update(_mapper.Map<ReservationDTO, Reservation>(reservation));
        }

        public bool DeleteReservation(int reservationId)
        {
			return _uow.Reservations.Delete(reservationId);
        }

        public IEnumerable<ReservationDTO> GetReservationsByDate(DateTime bookingDate, DateTime bookingDateEnd)
        {
            var reservations =
                _uow.Reservations.Find(r => r.BookingDate >= bookingDate && r.BookingDateEnd <= bookingDateEnd);

            return _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationDTO>>(reservations);
        }

        public ClientDTO GetClientByReservation(int reservationId)
        {
            var client = _uow.Reservations.GetClientByReservation(reservationId);

            return _mapper.Map<Client, ClientDTO>(client);
        }

        public RoomDTO GetRoomByReservation(int reservationId)
        {
            var room = _uow.Reservations.GetRoomByReservation(reservationId);

            return _mapper.Map<Room, RoomDTO>(room);
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
