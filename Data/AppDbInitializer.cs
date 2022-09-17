using Booking_Hotel.Models;
using Booking_Hotel.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Booking_Hotel.Data
{
    public class AppDbInitializer
    {
        //Data seeding is the process of populating a database with an initial set of data.
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var sevicesScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = sevicesScope.ServiceProvider.GetService<AppDbContext>();


                context.Database.EnsureCreated();
                //Branches
                if (!context.Branches.Any())
                {
                    context.Branches.AddRange(new List<Branch>()
                    {
                        new Branch()
                        {
                            Name = "Branch 1",
                            Location = "Cairo/egypt",
                            City = "Cairo"
                        },
                        new Branch()
                        {
                            Name = "Branch 2",
                            Location = "Cairo/egypt",
                            City = "Cairo"
                        },
                        new Branch()
                        {
                            Name = "Branch 3",
                            Location = "Cairo/egypt",
                            City = "Cairo"
                        },
                        new Branch()
                        {
                            Name = "Branch 4",
                            Location = "Cairo/egypt",
                            City = "Cairo"
                        },
                        new Branch()
                        {
                            Name = "Branch 5",
                            Location = "Cairo/egypt",
                            City = "Cairo"
                        },
                    });
                    context.SaveChanges();
                }                
                //RoomType
                if (!context.RoomTypes.Any())
                {
                    context.RoomTypes.AddRange(new List<RoomType>()
                    {
                        new RoomType()
                        {
                            TypeRoom = TypeOfRoom.Single,
                            Capacity = 2,
                        },
                        new RoomType()
                        {
                            TypeRoom = TypeOfRoom.Double,
                            Capacity = 3,
                        },
                        new RoomType()
                        {
                            TypeRoom = TypeOfRoom.Suite,
                            Capacity = 2,
                        },
                       new RoomType()
                        {
                            TypeRoom = TypeOfRoom.Suite,
                            Capacity = 1,
                        },
                        new RoomType()
                        {
                            TypeRoom =  TypeOfRoom.Single,
                            Capacity = 2,
                        },
                    });
                    context.SaveChanges();
                }
                //Rooms
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(new List<Room>()
                    {
                        new Room()
                        {
                            Price = 200,
                            Status = StatusRoom.Available,
                            Branch_Id=1,
                            RoomType_Id=1
                        },
                        new Room()
                        {
                            Price = 200,
                            Status = StatusRoom.Available,
                            Branch_Id=2,
                            RoomType_Id=2
                        },
                        new Room()
                        {
                            Price = 200,
                            Status = StatusRoom.Available,
                            Branch_Id=3,
                            RoomType_Id=3
                        },
                        new Room()
                        {
                            Price = 200,
                            Status = StatusRoom.Booked,
                            Branch_Id=4,
                            RoomType_Id=4
                        },
                        new Room()
                        {
                            Price = 200,
                            Status = StatusRoom.Booked,
                            Branch_Id=5,
                            RoomType_Id=5
                        },
                        new Room()
                        {
                            Price = 200,
                            Status =StatusRoom.Booked,
                            Branch_Id=1,
                            RoomType_Id=1
                        },
                        new Room()
                        {
                            Price = 200,
                            Status = StatusRoom.Available,
                            Branch_Id=2,
                            RoomType_Id=2
                        },

                    });
                    context.SaveChanges();
                }
                //Reservation
                if (!context.Reservations.Any())
                {
                    context.Reservations.AddRange(new List<Reservation>()
                    {
                        new Reservation()
                        {
                            Date = DateTime.Parse("2022-06-11"),
                            Status =true,
                            TotalPrice=1,
                            Guest_Id="84e67ee9-3e91-45f1-8b8f-87d902f49585"
                        },
                    });
                    context.SaveChanges();
                }
                //ReservationRoom
                if (!context.ReservationRooms.Any())
                {
                    context.ReservationRooms.AddRange(new List<ReservationRoom>()
                    {
                        new ReservationRoom()
                        {
                            NumberOfDays=5,
                            DateIn = DateTime.Parse("2022-06-11"),
                            DateOut=DateTime.Parse("2022-06-15"),
                            TotalPriceForOneRoom=600,
                            Reservation_Id=2,
                            Room_Id=5
                        },
                    });
                    context.SaveChanges();
                }
                //TempGuestRooms
                if (!context.TempGuestRooms.Any())
                {
                    context.TempGuestRooms.AddRange(new List<TempGuestRooms>()
                    {
                        new TempGuestRooms()
                        {
                            NumberOfDays=5,
                            DateIn = DateTime.Parse("2022-06-11"),
                            DateOut=DateTime.Parse("2022-06-15"),
                            GuestId ="84e67ee9-3e91-45f1-8b8f-87d902f49585"
                        },
                    });
                    context.SaveChanges();
                }

            }

        }

        //public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager,
        //   RoleManager<IdentityRole> roleManager)
        //{
        //    // Seed Basic User
        //    var defaultUser = new IdentityUser
        //    {
        //        UserName = "dcmsbasicuser@gmail.com",
        //        Email = "dcmsbasicuser@gmail.com",
        //        EmailConfirmed = true,
        //    };
        //    if (userManager.Users.All(u => u.Id != defaultUser.Id))
        //    {
        //        var user = await userManager.FindByEmailAsync(defaultUser.Email);
        //        if (user == null)
        //        {
        //            await userManager.CreateAsync(defaultUser, "100%Pa$$word");
        //            await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
        //        }
        //    }
        //}
    }
}
