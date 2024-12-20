using insight.DataContext;
using insight.Models;

namespace insight.Repository
{
    public class NotesRepository:INotesRepository
    {

        public bool Add(Note note)
        {
            DataStore.Notes.Add(note);
            return true;
        }
        public bool Delete(string id)
        {
            Note note = DataStore.Notes.FirstOrDefault(c => c.Id == id);
            DataStore.Notes.Remove(note);
            return true;
        }
        public bool Update(Note note)
        {
            Note thisNote = DataStore.Notes.FirstOrDefault(c => c.Id == note.Id);
            if (note != null)
            {
                thisNote.Type = note.Type;
                thisNote.Content = note.Content;
                thisNote.Reminder = note.Reminder;
                thisNote.DueDate = note.DueDate;    
                thisNote.BookmarkName = note.BookmarkName;
                thisNote.BookmarkUrl = note.BookmarkUrl;
                thisNote.IsCompleted = note.IsCompleted;

            }
            else {  return false; }

            return true;
        }

        public List<Note> GetAll(string userId)
        {
            return DataStore.Notes.Where(note => note.UserId == userId).ToList();
        }
        public Note GetById(string id)
        {
            return DataStore.Notes.FirstOrDefault((c => c.Id == id));
        }
    }
}
