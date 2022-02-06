using NUnit.Framework;
using Moq;
using BLL.Interfaces;
using BLL.DTO;
using System;
using PL.Controllers;
using System.Web.Http.Results;
using PL.Models;
using System.Collections.Generic;

namespace PL.Tests
{
	[TestFixture]
	public class ReservationControllerTests
	{
		private Mock<IReservationService> _reservationStub;

		public ReservationDTO GetReservationInstance()
		{
			return new ReservationDTO()
			{
				Id = 1,
				RoomId = 2,
				ClientId = 3,
				BookingDate = new DateTime(2022, 01, 01),
				BookingDateEnd = new DateTime(2022, 01, 03)
			};
		}

		public List<ReservationDTO> GetReservationInstanceList()
		{
			return new List<ReservationDTO>()
			{
				GetReservationInstance(),
				new ReservationDTO()
				{
					Id = 2,
					RoomId = 1,
					ClientId = 2,
					BookingDate = new DateTime(2022, 01, 01),
					BookingDateEnd = new DateTime(2022, 01, 04)
				}
			};
		}

		[SetUp]
		public void Setup()
		{
			// Reservation stub
			_reservationStub = new Mock<IReservationService>();
		}

		[Test]
		public void Get_ShouldReturnsReservationInfo_WhenITryToGetReservationWithIdEquals1()
		{
			//Arrange
			_reservationStub
				.Setup(r => r.GetReservationById(1))
				.Returns(GetReservationInstance());

			var reservationController = new ReservationsController(_reservationStub.Object);
			
			//Act
			var actionResult = reservationController.Get(1);
			var response = actionResult as OkNegotiatedContentResult<ReservationView>;
			
			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(3, response.Content.ClientId);
			Assert.AreEqual(2, response.Content.RoomId);
			Assert.AreEqual(1, response.Content.Id);
		}

		[Test]
		public void Get_ShouldReturnsNotFoundResult_WhenITryToGetReservationWhichISNotExist()
		{
			//Arrange
			_reservationStub
				.Setup(r => r.GetReservationById(999))
				.Returns((ReservationDTO)null);

			var reservationController = new ReservationsController(_reservationStub.Object);
			
			//Act
			var actionResult = reservationController.Get(999);

			//Assert
			Assert.IsNotNull(actionResult);
			Assert.IsInstanceOf(typeof(NotFoundResult), actionResult);
		}

		[Test]
		public void GetAll_ShouldReturnsAllExistingReservations_WhenITryToGetAllReservations()
		{
			//Arrange
			_reservationStub
				.Setup(r => r.GetAllReservations())
				.Returns(GetReservationInstanceList());

			var reservationController = new ReservationsController(_reservationStub.Object);
			
			//Act
			var actionResult = reservationController.GetAll();
			var response = actionResult as OkNegotiatedContentResult<IEnumerable<ReservationView>>;
			var responseContent = response.Content as List<ReservationView>;
			
			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(2, responseContent.Count);
		}

		[Test]
		public void GetAll_ShouldReturnsBadRequest_WhenSomeExceptionOccuredDuringRequest()
		{
			//Arrange
			const string ERROR_MESSAGE = "Some error occured during this request.";

			_reservationStub
				.Setup(r => r.GetAllReservations())
				.Throws(new Exception(ERROR_MESSAGE));

			var reservationController = new ReservationsController(_reservationStub.Object);

			//Act
			var actionResult = reservationController.GetAll();
			var response = actionResult as BadRequestErrorMessageResult;

			//Assert
			Assert.IsNotNull(response);
			Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), response);
			Assert.AreEqual(ERROR_MESSAGE, response.Message);
		}

		[Test]
		public void Add_ShouldReturnsOkResult_WhenISuccessfullyAddNewReservation()
		{
			//Arrange
			var unexistingReservationInstance = new ReservationDTO()
			{
				Id = 999,
				RoomId = 999,
				ClientId = 999,
				BookingDate = new DateTime(2022, 01, 01),
				BookingDateEnd = new DateTime(2022, 01, 04)
			};

			var unexistingMappedReservationInstance = new ReservationView()
			{
				Id = 999,
				RoomId = 999,
				ClientId = 999,
				BookingDate = new DateTime(2022, 01, 01),
				BookingDateEnd = new DateTime(2022, 01, 04)
			};
			_reservationStub.Setup(r => r.AddReservation(unexistingReservationInstance));

			var reservationController = new ReservationsController(_reservationStub.Object);
			
			//Act
			var actionResult = reservationController.Add(unexistingMappedReservationInstance);

			//Assert
			Assert.IsNotNull(actionResult);
			Assert.IsInstanceOf(typeof(OkResult), actionResult);
		}

		[Test]
		public void Delete_ShouldReturnOkResult_WhenISuccessfullyDeleteExistingElement()
		{
			//Arrange
			_reservationStub
				.Setup(r => r.DeleteReservation(GetReservationInstance().Id))
				.Returns(true);

			var reservationController = new ReservationsController(_reservationStub.Object);

			//Act
			var actionResult = reservationController.Delete(GetReservationInstance().Id);

			//Assert
			Assert.IsInstanceOf(typeof(OkResult), actionResult);
		}

		[Test]
		public void Delete_ShouldReturnsNotFound_WhenITryToDeleteNonExistentElement()
		{
			//Arrange
			_reservationStub
				.Setup(r => r.DeleteReservation(999))
				.Returns(false);

			var reservationController = new ReservationsController(_reservationStub.Object);

			//Act
			var actionResult = reservationController.Delete(999);

			//Assert
			Assert.IsInstanceOf(typeof(NotFoundResult), actionResult);
		}

		[Test]
		public void GetReservationByDate_ShouldReturnsReservation_WhenITryToGetReservationWithExistingDateRange()
		{
			//Arrange
			var bookingDate = GetReservationInstance().BookingDate.AddDays(-2);
			var bookingDateEnd = GetReservationInstance().BookingDateEnd.AddDays(2);

			_reservationStub
				.Setup(r => r.GetReservationsByDate(bookingDate, bookingDateEnd))
				.Returns(GetReservationInstanceList);

			var reservationController = new ReservationsController(_reservationStub.Object);

			//Act
			var actionResult = reservationController.GetReservationsByDateRange(bookingDate, bookingDateEnd);
			var response = actionResult as OkNegotiatedContentResult<IEnumerable<ReservationView>>;
			var responseContent = response.Content as List<ReservationView>;
			var firstReservation = responseContent[0];
			var secondReservation = responseContent[1];

			//Assert
			Assert.IsNotNull(actionResult);
			Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<IEnumerable<ReservationView>>), actionResult);
			Assert.Greater(responseContent.Count, 0);
			Assert.IsTrue(firstReservation.BookingDate >= bookingDate);
			Assert.IsTrue(firstReservation.BookingDateEnd <= bookingDateEnd);
			Assert.IsTrue(secondReservation.BookingDate >= bookingDate);
			Assert.IsTrue(secondReservation.BookingDateEnd <= bookingDateEnd);
		}

		[Test]
		public void GetReservationByDate_ShouldReturnsNotFound_WhenITryToGetReservationWithUnexistingDateRange()
		{
			//Arrange
			var bookingDate = GetReservationInstance().BookingDate.AddYears(-1);
			var bookingDateEnd = GetReservationInstance().BookingDateEnd.AddYears(-1);

			_reservationStub
				.Setup(r => r.GetReservationsByDate(bookingDate, bookingDateEnd))
				.Returns((IEnumerable<ReservationDTO>)null);

			var reservationController = new ReservationsController(_reservationStub.Object);

			//Act
			var actionResult = reservationController.GetReservationsByDateRange(bookingDate, bookingDateEnd);

			//Assert
			Assert.IsNotNull(actionResult);
			Assert.IsInstanceOf(typeof(NotFoundResult), actionResult);
		}

		[Test]
		public void GetClientByReservation_ShouldReturnBadRequest_WhenSomeErrorOccuredDuringRequest()
		{
			//Arrange
			const string ERROR_MESSAGE = "Some error occured during this request.";

			_reservationStub
				.Setup(r => r.GetClientByReservation(999))
				.Throws(new Exception(ERROR_MESSAGE));

			var reservationController = new ReservationsController(_reservationStub.Object);

			//Act
			var actionResult = reservationController.GetClientByReservation(999);
			var response = actionResult as BadRequestErrorMessageResult;

			//Assert
			Assert.IsNotNull(actionResult);
			Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), actionResult);
			Assert.AreEqual(ERROR_MESSAGE, response.Message);
		}

		[Test]
		public void GetRoomByReservation_ShouldReturnsRoom_WhenIPassExistingReservationId()
		{
			//Arrange
			var roomInstance = new RoomDTO()
			{
				Id = 1,
				RoomTypeId = 1,
				HotelId = 1
			};

			_reservationStub
				.Setup(r => r.GetRoomByReservation(GetReservationInstance().Id))
				.Returns(roomInstance);

			var reservationController = new ReservationsController(_reservationStub.Object);

			//Act
			var actionResult = reservationController.GetRoomByReservation(GetReservationInstance().Id);
			var response = actionResult as OkNegotiatedContentResult<RoomView>;
			var responseContent = response.Content;

			//Assert
			Assert.IsNotNull(actionResult);
			Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<RoomView>), actionResult);
			Assert.IsNotNull(responseContent);
			Assert.AreEqual(1, responseContent.Id);
			Assert.AreEqual(1, responseContent.HotelId);
			Assert.AreEqual(1, responseContent.RoomTypeId);
		}
	}
}