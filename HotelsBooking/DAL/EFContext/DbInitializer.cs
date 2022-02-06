using System;
using System.Collections.Generic;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EFContext
{
	public class DbInitializer : DropCreateDatabaseAlways<HotelContext>
	{
		protected override void Seed(HotelContext db)
		{
			var hotels = new List<Hotel>()
			{
				new Hotel {Name = "Florida Hotel", Location = "Shcherbakivskogo Street 52", Rating = "three stars"},
				new Hotel {Name = "Goloseevo Hotel", Location = "Goloseevsky Prospect 87", Rating = "four stars"},
				new Hotel {Name = "11 Mirrors Design Hotel", Location = "Bohdan Khmelnitsky Street 34A", Rating = "five stars"},
				new Hotel {Name = "Cityhotel Apartments", Location = "Gogolevskaya Street 8", Rating = "four stars"}
			};

			foreach (var hotel in hotels)
			{
				db.Hotels.Add(hotel);
			}

			var roomTypes = new List<RoomType>()
			{
				new RoomType
				{
					Name = "Suit",
					Description = "Superior rooms, mainly consist of 2 rooms - belong to the luxury category.",
					Price = 1805.45,
					Capacity = 3
				},
				new RoomType
				{
					Name = "De Luxe",
					Description =
						"Superior rooms which mainly bigger than others. Have all necessary appliances with excellent quality.",
					Price = 3202.72,
					Capacity = 4
				},
				new RoomType
				{
					Name = "Standart",
					Description = "Standard single rooms with minimum comfort.",
					Price = 1200.69,
					Capacity = 2
				},
				new RoomType
				{
					Name = "Duplex",
					Description = "Two-story suit room which has three comfortable rooms for living",
					Price = 4500.15,
					Capacity = 6
				}
			};

			foreach (var roomType in roomTypes)
			{
				db.RoomTypes.Add(roomType);
			}

			var clients = new List<Client>()
			{
				new Client {Name = "Sergey Vladimirovich Burenko", PassportId = "NM808123"},
				new Client {Name = "Oksana Yurievna Panatko", PassportId = "KM408923"},
				new Client {Name = "Alexandr Andreevich Shmatko", PassportId = "NM109630"},
				new Client {Name = "Dmitriy Petrovich Kovalenko", PassportId = "EM202089"},
				new Client {Name = "Viktor Viktorovich Tsygankov", PassportId = "MM906906"},
				new Client {Name = "Elena Sergeevna Sinica", PassportId = "NM306963"},
				new Client {Name = "Roman Leonidovich Zaiets", PassportId = "NM509631"},
			};

			foreach (var client in clients)
			{
				db.Clients.Add(client);
			}

			var rooms = new List<Room>()
			{
				new Room { RoomType = roomTypes[0], Hotel = hotels[0]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[0]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[0]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[0]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[0]},

				new Room { RoomType = roomTypes[0], Hotel = hotels[1]},
				new Room { RoomType = roomTypes[0], Hotel = hotels[1]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[1]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[1]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[1]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[1]},

				new Room { RoomType = roomTypes[0], Hotel = hotels[2]},
				new Room { RoomType = roomTypes[0], Hotel = hotels[2]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[2]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[2]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[2]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[2]},
				new Room { RoomType = roomTypes[3], Hotel = hotels[2]},
				new Room { RoomType = roomTypes[3], Hotel = hotels[2]},

				new Room { RoomType = roomTypes[0], Hotel = hotels[3]},
				new Room { RoomType = roomTypes[0], Hotel = hotels[3]},
				new Room { RoomType = roomTypes[0], Hotel = hotels[3]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[3]},
				new Room { RoomType = roomTypes[1], Hotel = hotels[3]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[3]},
				new Room { RoomType = roomTypes[2], Hotel = hotels[3]}
			};

			foreach (var room in rooms)
			{
				db.Rooms.Add(room);
			}

			var reservations = new List<Reservation>()
			{
				new Reservation
				{
					Room = rooms[0],
					Client = clients[0],
					BookingDate = new DateTime(2019, 06, 01),
					BookingDateEnd = new DateTime(2019, 06, 10)
				},
				new Reservation
				{
					Room = rooms[2],
					Client = clients[1],
					BookingDate = new DateTime(2019, 05, 28),
					BookingDateEnd = new DateTime(2019, 06, 5)
				},
				new Reservation
				{
					Room = rooms[5],
					Client = clients[2],
					BookingDate = new DateTime(2019, 06, 01),
					BookingDateEnd = new DateTime(2019, 06, 11)
				},
			};

			foreach (var reservation in reservations)
			{
				db.Reservations.Add(reservation);
			}

			db.SaveChanges();
			base.Seed(db);
		}
	}
}
