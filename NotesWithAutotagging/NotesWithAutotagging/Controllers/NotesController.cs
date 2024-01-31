using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesWithAutotagging.Data;
using NotesWithAutotagging.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NotesWithAutotagging.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class NotesController : Controller
    {
        private readonly NoteContext _context;

        public NotesController(NoteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound(); 
	        }

            return note;
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(string content)
        {
            var note = new Note(content);
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, string content)
        {
            var note = await _context.Notes.FindAsync(id);
            
	    if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                note.Content = content;
                await _context.SaveChangesAsync(); 
	        }
            catch (DbUpdateConcurrencyException)
            {
                throw; 
	        }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);

            if(note == null)
            {
                return NotFound(); 
	        }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

