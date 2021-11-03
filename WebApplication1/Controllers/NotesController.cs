using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Defines the <see cref="NotesController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        INoteCollectionService _noteCollectionService;

        public NotesController(INoteCollectionService noteCollectionService)
        {
            _noteCollectionService = noteCollectionService ?? throw new ArgumentNullException(nameof(noteCollectionService));

        }

        /// <summary>
        /// The GetNotes.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            List<Notes> notes = await _noteCollectionService.GetAll();
            return Ok(notes);
        }

        /// <summary>
        /// The CreateNote.
        /// </summary>
        /// <param name="note">The note<see cref="Notes"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest("Note cannot be null");
            }

            if (await _noteCollectionService.Create(note))
            {
                return CreatedAtRoute("GetNoteById", new { id = note.Id.ToString() }, note);
            }
            return NoContent();
        }

        /// <summary>
        /// Return a note find by a specified id.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>.</returns>
        [HttpGet("{id}", Name = "GetNoteById")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var note = await _noteCollectionService.Get(id);

            if (note == null)
            {
                return BadRequest("Note was not found");
            }
            return Ok(note);
        }

        /// <summary>
        /// The UpdateNote.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="noteToUpdate">The noteToUpdate<see cref="Notes"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] Notes noteToUpdate)
        {
            if (noteToUpdate == null)
            {
                return BadRequest("Note cannot be null");
            }

            if (await _noteCollectionService.Update(id, noteToUpdate))
            {
                return Ok(_noteCollectionService.Get(id));
            }

            return NotFound("Note not found!");
        }

        /// <summary>
        /// The DeleteNote.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null!");
            }

            if (await _noteCollectionService.Delete(id))
            {
                return NoContent();
            }

            return NotFound("Note not found!");
        }
    }
}

