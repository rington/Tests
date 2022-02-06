using System;
using BLL.Infrastructure;
using BLL.Interfaces;
using Ninject;

namespace API
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new HotelModule());
            var hotelService = kernel.Get<IHotelService>();
            Console.WriteLine($"Hotel by name: ");
            Console.WriteLine(
                    $"{hotelService.GetHotelByName("Goloseevo Hotel").Id,5}{hotelService.GetHotelByName("Goloseevo Hotel").Name,20}{hotelService.GetHotelByName("Goloseevo Hotel").Location, 30}{hotelService.GetHotelByName("Goloseevo Hotel").Rating,40}");
            
            Console.WriteLine();
           /* var hotelService = kernel.Get<IHotelService>();
            Console.WriteLine($"Hotels info: ");
            foreach (var hotel in hotelService.GetAllHotels())
            {
                Console.WriteLine(
                    $"{hotel.Id,5}{hotel.Name,20}{hotel.Location,30}{hotel.Rating,40}");
            }
            var reservationService = kernel.Get<IReservationService>();
            Console.WriteLine($"Get client by reservation Id: ");
            Console.WriteLine(
                    $"{reservationService.GetClientByReservation(1).Id,5}{reservationService.GetClientByReservation(1).Name,30}{reservationService.GetClientByReservation(1).PassportId,30}");
            Console.WriteLine($"Get room by reservation Id: ");
            Console.WriteLine(
                    $"{reservationService.GetRoomByReservation(1).Id,5}{reservationService.GetRoomByReservation(1).HotelId,20}{ reservationService.GetRoomByReservation(1).RoomTypeId,25}");*/
            Console.ReadKey();
        }
    }
}
