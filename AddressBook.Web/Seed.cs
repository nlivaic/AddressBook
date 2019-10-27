using System;
using System.Collections.Generic;
using System.Linq;
using AddressBook.Core;
using AddressBook.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Web
{
    public static class Seed
    {
        public static IWebHost EnsurePopulated(this IWebHost webHost)
        {
            using (var scope = webHost.Services.GetService<IServiceScopeFactory>().CreateScope())
            using (var ctx = scope.ServiceProvider.GetRequiredService<AddressBookDbContext>())
            {
                ctx.Database.Migrate();
                if (!ctx.AddressBooks.Any())
                {
                    ctx.AddressBooks.Add(
                        new Core.AddressBook(
                            new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                            new List<Contact> {
                                new Contact(
                                    new Guid("ded445d8-6401-47d1-b068-702872ba97bd"),
                                    "Name 1",
                                    new Address(
                                        "First Street",
                                        "2",
                                        "New York",
                                        "USA"),
                                    new DateTime(1977, 1, 1),
                                    new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                                    new List<TelephoneNumber>() {
                                        new TelephoneNumber(new Guid("ded445d8-6401-47d1-b068-702872ba97bd"), "392234165"),
                                        new TelephoneNumber(new Guid("ded445d8-6401-47d1-b068-702872ba97bd"), "827137551"),
                                        new TelephoneNumber(new Guid("ded445d8-6401-47d1-b068-702872ba97bd"), "343471358")
                                    }
                                ),
                                new Contact (
                                    new Guid("7507da05-e022-4b0e-b364-7425424fcab8"),
                                    "Name 2",
                                    new Address(
                                        "Second Street",
                                        "4",
                                        "New Jersey",
                                        "USA"),
                                    new DateTime(1967, 1, 1),
                                    new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                                    new List<TelephoneNumber>() {
                                        new TelephoneNumber(new Guid("7507da05-e022-4b0e-b364-7425424fcab8"), "576618730"),
                                        new TelephoneNumber(new Guid("7507da05-e022-4b0e-b364-7425424fcab8"), "979741254"),
                                        new TelephoneNumber(new Guid("7507da05-e022-4b0e-b364-7425424fcab8"), "420433518")
                                    }
                                ),
                                new Contact (
                                    new Guid("8f9ebc89-1b17-4787-af4a-d57bb49847a6"),
                                    "Name 3",
                                    new Address(
                                        "Third Street",
                                        "8",
                                        "Chicago",
                                        "USA"),
                                    new DateTime(1952, 1, 1),
                                    new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                                    new List<TelephoneNumber>() {
                                        new TelephoneNumber(new Guid("8f9ebc89-1b17-4787-af4a-d57bb49847a6"), "240672469"),
                                        new TelephoneNumber(new Guid("8f9ebc89-1b17-4787-af4a-d57bb49847a6"), "763677894"),
                                        new TelephoneNumber(new Guid("8f9ebc89-1b17-4787-af4a-d57bb49847a6"), "920082904")
                                    }
                                ),

                                new Contact (
                                    new Guid("ed015b66-901d-431a-a6bd-6e9fbc6aeedd"),
                                    "Name 4",
                                    new Address(
                                        "Fourth Street",
                                        "8",
                                        "Seattle",
                                        "USA"),
                                    new DateTime(1912, 1, 1),
                                    new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                                    new List<TelephoneNumber>() {
                                        new TelephoneNumber(new Guid("ed015b66-901d-431a-a6bd-6e9fbc6aeedd"), "355378167"),
                                        new TelephoneNumber(new Guid("ed015b66-901d-431a-a6bd-6e9fbc6aeedd"), "584693100"),
                                        new TelephoneNumber(new Guid("ed015b66-901d-431a-a6bd-6e9fbc6aeedd"), "497382705")
                                    }
                                ),
                                new Contact (
                                    new Guid("b489182d-8aee-4908-8845-2987fa87c544"),
                                    "Name 5",
                                    new Address(
                                        "Fifth Street",
                                        "32",
                                        "Denver",
                                        "USA"),
                                    new DateTime(1998, 1, 1),
                                    new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                                    new List<TelephoneNumber>() {
                                        new TelephoneNumber(new Guid("b489182d-8aee-4908-8845-2987fa87c544"), "917127566"),
                                        new TelephoneNumber(new Guid("b489182d-8aee-4908-8845-2987fa87c544"), "753883073"),
                                        new TelephoneNumber(new Guid("b489182d-8aee-4908-8845-2987fa87c544"), "662047222")
                                    }
                                ),
                                new Contact (
                                    new Guid("c50be1f5-a004-4d8d-999d-5a9b0dadc65a"),
                                    "Name 6",
                                    new Address(
                                        "Sixth Street",
                                        "64",
                                        "San Francisco",
                                        "USA"),
                                    new DateTime(1984, 1, 1),
                                    new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                                    new List<TelephoneNumber>() {
                                        new TelephoneNumber(new Guid("c50be1f5-a004-4d8d-999d-5a9b0dadc65a"), "6157609510"),
                                        new TelephoneNumber(new Guid("c50be1f5-a004-4d8d-999d-5a9b0dadc65a"), "7930999390"),
                                        new TelephoneNumber(new Guid("c50be1f5-a004-4d8d-999d-5a9b0dadc65a"), "8749141882")
                                    }
                                ),
                                new Contact (
                                    new Guid("840203ab-cc77-4837-82fa-8b0ca337fff7"),
                                    "Name 7",
                                    new Address(
                                        "Seventh Street",
                                        "128",
                                        "San Francisco",
                                        "USA"),
                                    new DateTime(1984, 1, 1),
                                    new Guid("f916643f-2753-4633-9a9e-50a862e08267"),
                                    new List<TelephoneNumber>() {
                                        new TelephoneNumber(new Guid("840203ab-cc77-4837-82fa-8b0ca337fff7"), "4043839479"),
                                        new TelephoneNumber(new Guid("840203ab-cc77-4837-82fa-8b0ca337fff7"), "6554177381"),
                                        new TelephoneNumber(new Guid("840203ab-cc77-4837-82fa-8b0ca337fff7"), "0981254542")
                                    }
                                )
                            }
                        )
                    );
                    ctx.SaveChanges();
                }
            }
            return webHost;
        }
    }
}