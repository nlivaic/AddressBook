using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Core;
using AddressBook.Core.Services;
using AddressBook.Web.Models;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IAddressBookService _service;
        private readonly HtmlSanitizer _sanitizer;

        public ContactController(IAddressBookService service, HtmlSanitizer sanitizer)
        {
            _service = service;
            _sanitizer = sanitizer;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int pageNr)
        {
            if (pageNr < 1)
            {
                return BadRequest("Invalid data on input");
            }
            IEnumerable<ContactDto> contactsDto = null;
            try
            {
                contactsDto = (await _service
                    .GetAddressBookAsync(pageNr))
                    .Contacts
                    .Select(c => ContactDto.Create(c));
            }
            catch
            {
                return BadRequest();
            }
            return Ok(contactsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]string id)
        {
            if (!Guid.TryParse(id, out var contactId))
                return BadRequest("Id not in proper format");
            ContactDto contactDto = null;
            try
            {
                contactDto = (await _service
                    .GetAddressBookForContactAsync(contactId))
                    .Contacts
                    .Select(c => ContactDto.Create(c))
                    .SingleOrDefault();
            }
            catch
            {
                return BadRequest();
            }
            return contactDto == null ? (IActionResult)NotFound() : Ok(contactDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ContactDto contactDto)
        {
            Guid newId = new Guid();
            contactDto.AddressBookId = (await _service.GetAddressBookIdAsync()).ToString();
            Contact contact = null;
            contactDto.Sanitize(_sanitizer);
            try
            {
                contact = contactDto.Create();
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch { return BadRequest("Invalid data on input."); }
            try
            {
                newId = await _service.AddContactAsync(contact);
            }
            catch (ArgumentException ex) { return BadRequest($"An error happened while saving new contact: {ex.Message}"); }
            catch { return BadRequest("An error happened while saving new contact."); }
            return CreatedAtAction(nameof(Get), new { id = newId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody]ContactDto contactDto)
        {
            contactDto.Id = id;
            Contact contact = null;
            contactDto.Sanitize(_sanitizer);
            try
            {
                contact = contactDto.Create();
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch { return BadRequest("Invalid data on input."); }
            try
            {
                await _service.UpdateContactAsync(contact);
            }
            catch (ArgumentException ex) { return BadRequest($"An error happened while saving new contact: {ex.Message}."); }
            catch { return BadRequest("An error happened while saving new contact."); }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            if (!Guid.TryParse(id, out var contactId))
                return BadRequest("Id not in proper format");
            try
            {
                await _service.RemoveContactAsync(contactId);
            }
            catch (ArgumentException ex) { return BadRequest($"An error happened while deleting contact: {ex.Message}"); }
            catch { return BadRequest("An error happened while deleting contact."); }
            return Ok();
        }
    }
}
