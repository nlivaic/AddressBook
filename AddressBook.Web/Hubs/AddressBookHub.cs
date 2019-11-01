using System;
using System.Threading.Tasks;
using AddressBook.Web.Models;
using Microsoft.AspNetCore.SignalR;

namespace AddressBook.Web
{
    public class AddressBookHub : Hub
    {
        public AddressBookHub()
        {

        }

        public async Task UpdateContact()
        {
            await Clients.All.SendAsync("updateContact", new ContactDto { Name = "Spookville" });
        }
    }
}