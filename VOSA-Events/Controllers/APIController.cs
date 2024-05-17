using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using VOSA_Events.Data;
using VOSA_Events.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VOSA_Events.Controllers
{
    [Route("/api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext _database;

        public APIController(AppDbContext database)
        {
            _database = database;
        }

        // En sökning oavsett innehåll ger tillbaka alla event som innehåller något från sökningen. 
        [HttpGet("GetEvent")]
        public ActionResult<List<EventDto>> GetEvents([FromQuery] string? searchInput)
        {
            IQueryable<Event> query = _database.Events;
            // Tom sökning ska ge alla resultat, annars filtrera
            if (!string.IsNullOrEmpty(searchInput))
            {
                query = query.Where(e => e.Name.Contains(searchInput));
            }

            List<EventDto> result = query.Select(e => new EventDto()
            {
                Name = e.Name,
                Price = e.Price,
                Description = e.Description,
                City = e.City,
                Date = e.Date,
                TicketQuantity = e.TicketQuantity,
                ImagePath = e.ImagePath,
                CategoryName = e.Category.Name,
                AdminName = e.AdminAccount.Name
            }).ToList();

            return Ok(result);

        }

        //Låter oss/admin skapa ett event
        [HttpPost("NewEvent")]
        public ActionResult<Event> CreateEvent(EventDto eventData)
        {
            Category? categoryQuery = _database.Categories.Where(c => c.Name.Contains(eventData.CategoryName)).FirstOrDefault();

            if (categoryQuery == null)
            {
                return NotFound();
            }

            Account? adminQuery = _database.Accounts.Where(a => a.Name.Contains(eventData.AdminName)).FirstOrDefault();

            if (adminQuery == null)
            {
                return NotFound();
            }


            Event newEvent = new Event()
            {
                Name = eventData.Name,
                Price = eventData.Price,
                Description = eventData.Description,
                City = eventData.City,
                Date = eventData.Date,
                TicketQuantity = eventData.TicketQuantity,
                ImagePath = eventData.ImagePath,
                Category = categoryQuery,
                AdminAccount = adminQuery
            };

            _database.Events.Add(newEvent);
            _database.SaveChanges();

            return Ok();
        }

        [HttpPost("AddCategory")]
        public ActionResult<Category> AddCategory(CategoryDto categoryData)
        {
            if (categoryData == null || string.IsNullOrWhiteSpace(categoryData.Name))
            {
                return BadRequest(new { message = "Category name cannot be null or empty." });
            }

            Category newCategory = new Category
            {
                Name = categoryData.Name
            };

            _database.Categories.Add(newCategory);
            _database.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.ID }, newCategory);
        }

        [HttpGet("GetCategoryById/{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = _database.Categories.Find(id);
            if (category == null)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            return Ok(category);
        }

        [HttpPut("UpdateEvent/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto eventDto)
        {
            if (id != eventDto.ID)
            {
                return BadRequest();
            }

            var eventToUpdate = await _database.Events.FirstOrDefaultAsync(e => e.ID == id);
            if (eventToUpdate == null)
            {
                return NotFound();
            }

            eventToUpdate.Name = eventDto.Name;
            eventToUpdate.Price = eventDto.Price;
            eventToUpdate.Description = eventDto.Description;
            eventToUpdate.City = eventDto.City;
            eventToUpdate.Date = eventDto.Date;
            eventToUpdate.TicketQuantity = eventDto.TicketQuantity;
            eventToUpdate.ImagePath = eventDto.ImagePath;

            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

			
        [HttpDelete("DeleteEvent/{id}")]
        public ActionResult DeleteEvent(int id)
        {
            var eventToDelete = _database.Events.Find(id);

            if (eventToDelete == null)
            {
                return NotFound();
            }

            _database.Events.Remove(eventToDelete);
            _database.SaveChanges();

            return Ok();
        }
    }

}