using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {

        static List<Notes> _notes = new List<Notes> { 
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "First Note", Description = "First Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Second Note", Description = "Second Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Third Note", Description = "Third Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        public NotesController()
        {


        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }


        [HttpPost]
        public IActionResult CreateNote([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest("Note cannot be null");
            }

            _notes.Add(note);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetByOwnerId(Guid id)
        {
            List<Notes> note = _notes.FindAll(note => note.OwnerId == id);
            return Ok(note);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Notes noteToUpdate)
        {
            if (noteToUpdate == null)
            {
                return BadRequest("Note cannot be null");
            }
            int index = _notes.FindIndex(note => note.Id == id);
            if(index == -1)
            {
                return NotFound();
            }
            noteToUpdate.Id = _notes[index].Id;
            _notes[index] = noteToUpdate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var index = _notes.FindIndex(note => note.Id == id);

            if(index == -1)
            {
                return NotFound();
            }

            _notes.RemoveAt(index);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTitleNote(Guid id,[FromBody] string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                return BadRequest("The string cannot be null");
            }

            var index = _notes.FindIndex(note => note.Id == id);

            if (index == -1)
            {
                return NotFound();
            }
            _notes[index].Title = title;
            return Ok(_notes[index]);
        }



        /// <summary>
        /// Returns a list of notes
        /// </summary>
        /// <returns></returns>


    }
}

