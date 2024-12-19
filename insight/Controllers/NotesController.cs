using insight.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace insight.Controllers
{
    [Authorize]
    public class NotesController : ControllerBase
    {
        [HttpGet("/notes")] // Get all notes for a user
        public IActionResult GetNotes()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }
            return Ok(DataStore.Notes.Where(note => note.UserId == userId));
        }

        [HttpPost("/notes")] // Add a new note
        public IActionResult AddNote([FromBody] Note note)
        {
            if (note.Content.Length > 100)
            {
                return BadRequest("Note content exceeds 100 characters.");
            }

            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }

            note.UserId = userId;
            DataStore.Notes.Add(note);
            return Ok(note);
        }
    }

}
