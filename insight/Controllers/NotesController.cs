using insight.BLL;
using insight.DataContext;
using insight.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace insight.Controllers
{

    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotesController : ControllerBase
    {


        private readonly INotesManager _notesManager;

        public NotesController(INotesManager notesManager)
        {
            _notesManager = notesManager;

        }

        [HttpPost]
        public IActionResult GetAllNotes(ListRequest req)
        {
            try
            {
                var data = _notesManager.GetAll(User.Identity?.Name);
                var filteredData = _notesManager.Filter(data, req);
                return Ok(new GlobalResponse("successfully loaded", true, filteredData));
            }
            catch
            {
                return Ok(new GlobalResponse("Retriving List unsuccessful"));
            }

        }

        [HttpGet]
        public IActionResult GetNote(string id)
        {
            try
            {
                var model = _notesManager.GetById(id);
                if(model == null)
                {
                    return Ok(new GlobalResponse("No Note was found", false));
                }
                if (model.UserId == User.Identity?.Name)
                {
                    return Ok(new GlobalResponse("successfully loaded", true, model));
                }
                else
                {
                    return Ok(new GlobalResponse("Access is unauthorized", false)) ;
                }
            }
            catch
            {
                return Ok(new GlobalResponse("Unable to fetch note", false));
            }


        }

        [HttpPost]
        public IActionResult AddNote([FromBody] NoteModel noteModel)
        {
            try
            {

                if (string.IsNullOrEmpty(noteModel.Content) || string.IsNullOrWhiteSpace(noteModel.Content))
                {
                    return Ok(new GlobalResponse("Note content Cannot be empty or spaces", false));
                }

                if (noteModel.Content.Length > 100)
                {
                    return Ok(new GlobalResponse("Note content exceeds 100 characters.", false));
                }
                _notesManager.Add(noteModel, User.Identity?.Name);
                return Ok(new GlobalResponse("Note Added Successfully"));

            }
            catch
            {
                return Ok(new GlobalResponse("Unable to add note", false));
            }
      
        }


        [HttpPost]
        public IActionResult UpdateNote([FromBody] NoteModel noteModel)
        {
            try
            {

                if (noteModel.Content.Length > 100)
                {
                    return Ok(new GlobalResponse("Note content exceeds 100 characters.", false));
                }
                _notesManager.Update(noteModel, User.Identity?.Name);
                return Ok(new GlobalResponse("Note has been updated successfully"));

            }
            catch
            {
                return Ok(new GlobalResponse("Unable to update note", false));
            }
        }


    }

}
