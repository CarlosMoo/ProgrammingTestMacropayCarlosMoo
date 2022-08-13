using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingTestMacropayCarlosMoo.Models;
using System.Text.Json;

namespace ProgrammingTestMacropayCarlosMoo.Controllers
{
    public class ContactsController : MacroPayDefaultController
    {
        private string _connectionString = @"C:\Users\Carlos Moo\source\repos\ProgrammingTestMacropayCarlosMoo\ProgrammingTestMacropayCarlosMoo\Database\fakedatabase.js";
        #region GET /contacts
        [HttpGet()]
        public ActionResult<List<Contacts>> GetAll()
        {
            Request.ContentType = "application/json";
            var request = HttpContext.Request.Query["phrase"];
            if (request.ToString() == null || request.ToString().Length == 0)
            {
                return BadRequest();
            }
            string json = System.IO.File.ReadAllText(_connectionString);
            var listContacts = JsonSerializer.Deserialize<List<Contacts>>(json).AsQueryable().OrderBy(contact => contact.name).ToList();
            var result = listContacts.FindAll(contactresult => contactresult.name.ToUpper().Contains(request.ToString().ToUpper()));
            return result;
        }
        #endregion

        #region GET /contacts/
        [HttpGet("{id}")]
        public ActionResult<Contacts> GetById(string id)
        {
            string json = System.IO.File.ReadAllText(_connectionString);
            var result = JsonSerializer.Deserialize<List<Contacts>>(json).Where(contact => contact.id == id).FirstOrDefault();
            if(result != null)
            {
                return result;
            }
            else
            {
                return NotFound();
            }
        }
        #endregion

        #region DELETE /contacts/
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string json = System.IO.File.ReadAllText(_connectionString);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(json);
            foreach (var contact in contacts)
            {
                if (contact.id == id)
                {
                    var validate = contacts.Remove(contact);
                    TextWriter writeJson = new StreamWriter(_connectionString);
                    writeJson.Write(JsonSerializer.Serialize(contacts));
                    writeJson.Flush();
                    writeJson.Close();

                    if (validate)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            return NotFound();
        }
        #endregion
    }
}
