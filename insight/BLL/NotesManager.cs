using insight.Models;
using insight.Repository;
using System.Collections.Generic;

namespace insight.BLL
{
    public class NotesManager
    {

        NotesRepository _notesRepository = new NotesRepository();

        public bool Add(NoteModel noteModel, string userId)
        {
            var note = new Note();
            note.Id = Guid.NewGuid().ToString();
            note.UserId = userId;
            note.Type = noteModel.Type;
            note.Content = noteModel.Content;
            note.Reminder = noteModel.Reminder;
            note.DueDate = noteModel.DueDate;
            note.IsCompleted = noteModel.IsCompleted;
            note.BookmarkName = noteModel.BookmarkName;
            note.BookmarkUrl = noteModel.BookmarkUrl;
            note.IsCompleted = noteModel.IsCompleted;

            return _notesRepository.Add(note);
        }

        public List<Note> GetAll(string userId)
        {

            return _notesRepository.GetAll(userId);
        }
        public List<Note> Filter(List<Note> notes, ListRequest req)
        {
            if(notes.Count == 0) { return notes; }
            List<Note> filteredNotes = new List<Note>();
            if (req.Today)
            {
                filteredNotes= notes.Where(x => (x.Type == StoredVariable.Reminder && x.Reminder.HasValue?x.Reminder.Value.Date == DateTime.Today.Date:false )            
                || (x.Type == StoredVariable.ToDo && x.DueDate.HasValue ? x.DueDate.Value.Date == DateTime.Today.Date : false)
                ).ToList();
            }
            if (req.ThisWeek)
            {
                var currentDate = DateTime.Now;
                var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
                var endOfWeek = startOfWeek.AddDays(6);
                filteredNotes = notes.Where(x => (x.Type == StoredVariable.Reminder && x.Reminder.HasValue ? (x.Reminder.Value.Date >= startOfWeek && x.Reminder.Value.Date <= endOfWeek) : false)
              || (x.Type == StoredVariable.ToDo && x.DueDate.HasValue ? (x.DueDate.Value.Date >= startOfWeek && x.DueDate.Value.Date <= endOfWeek) : false)
               ).ToList();
            }
            if (req.ThisMonth)
            {
                filteredNotes = notes.Where(x => (x.Type == StoredVariable.Reminder && x.Reminder.HasValue ? (x.Reminder.Value.Month == DateTime.Now.Month && x.Reminder.Value.Year == DateTime.Now.Year) : false)
             || (x.Type == StoredVariable.ToDo && x.DueDate.HasValue ? (x.DueDate.Value.Month == DateTime.Now.Month && x.DueDate.Value.Year == DateTime.Now.Year) : false)
              ).ToList();
            }
            if (req.All)
            {
                filteredNotes = notes;
            }
            return filteredNotes;
        }
        public bool Update(NoteModel noteModel, string userId)
        {
            var note = new Note();
            note.Id = noteModel.Id;
            note.Type = noteModel.Type;
            note.Type = noteModel.Type;
            note.Content = noteModel.Content;
            note.Reminder = noteModel.Reminder;
            note.DueDate = noteModel.DueDate;
            note.IsCompleted = noteModel.IsCompleted;
            note.BookmarkName = noteModel.BookmarkName;
            note.BookmarkUrl = noteModel.BookmarkUrl;
            note.IsCompleted = noteModel.IsCompleted;
            return _notesRepository.Update(note);
        }


        public Note GetById(string id)
        {
            return _notesRepository.GetById(id);

        }


        public bool Delete(string id)
        {
            return _notesRepository.Delete(id);
        }

    }
}
