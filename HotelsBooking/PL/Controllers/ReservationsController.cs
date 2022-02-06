using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
	public class ReservationsController : ApiController
	{
		private readonly IReservationService _reservationsService;

		private readonly IMapper _mapper;

		public ReservationsController(IReservationService service)
		{
			_reservationsService = service;

			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ReservationDTO, ReservationView>();
				cfg.CreateMap<ReservationView, ReservationDTO>();
				cfg.CreateMap<ClientDTO, ClientView>();
				cfg.CreateMap<RoomDTO, RoomView>();
			});

			_mapper = config.CreateMapper();
		}

		[Route("api/reservations/{id:int}")]
		[HttpGet]
		public IHttpActionResult Get(int id)
		{
			var reservation = _reservationsService.GetReservationById(id);

			if (reservation == null)
			{
				return NotFound();
			}
			
			var reservationView = _mapper.Map<ReservationDTO, ReservationView>(reservation);

			return Ok(reservationView);
		}

		[Route("api/reservations")]
		[HttpGet]
		public IHttpActionResult GetAll()
		{
			IEnumerable<ReservationView> reservationsView;
			try
			{
				var reservationsDTO = _reservationsService.GetAllReservations();
				reservationsView = _mapper
					 .Map<IEnumerable<ReservationDTO>, IEnumerable<ReservationView>>(reservationsDTO);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(reservationsView);
		}

		[Route("api/reservations")]
		[HttpPost]
		public IHttpActionResult Add([FromBody] ReservationView reservation)
		{
			var reservationDTO = _mapper.Map<ReservationView, ReservationDTO>(reservation);

			try
			{
				_reservationsService.AddReservation(reservationDTO);
				_reservationsService.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}

		[Route("api/reservations")]
		[HttpPut]
		public IHttpActionResult Update([FromBody] ReservationView reservation)
		{
			var reservationDTO = _mapper.Map<ReservationView, ReservationDTO>(reservation);

			try
			{
				_reservationsService.UpdateReservation(reservationDTO);
				_reservationsService.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}

		[Route("api/reservations/{id:int}")]
		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			try
			{
				if (_reservationsService.DeleteReservation(id))
				{
					_reservationsService.SaveChanges();

					return Ok();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/reservations/date/{bookingDate}/{bookingDateEnd}")]
		[HttpGet]
		public IHttpActionResult GetReservationsByDateRange(DateTime bookingDate, DateTime bookingDateEnd)
		{
			IEnumerable<ReservationView> reservationsView;
			try
			{
				var reservationsDTO = _reservationsService.GetReservationsByDate(bookingDate, bookingDateEnd);

				if (reservationsDTO != null)
				{
					reservationsView = _mapper.Map<IEnumerable<ReservationDTO>, IEnumerable<ReservationView>>(reservationsDTO);

					return Ok(reservationsView);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/reservations/{reservationId:int}/client")]
		[HttpGet]
		public IHttpActionResult GetClientByReservation(int reservationId)
		{
			ClientView clientView;

			try
			{
				var clientDTO = _reservationsService.GetClientByReservation(reservationId);

				if (clientDTO != null)
				{
					clientView = _mapper.Map<ClientDTO, ClientView>(clientDTO);

					return Ok(clientView);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/reservations/{reservationId:int}/room")]
		[HttpGet]
		public IHttpActionResult GetRoomByReservation(int reservationId)
		{
			RoomView roomView;
			try
			{
				var roomDTO = _reservationsService.GetRoomByReservation(reservationId);

				if (roomDTO != null)
				{
					roomView = _mapper.Map<RoomDTO, RoomView>(roomDTO);

					return Ok(roomView);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
