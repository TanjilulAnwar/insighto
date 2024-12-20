using insight.Models;
using insight.Repository;
using System.Collections.Generic;

namespace insight.BLL
{
    public interface INotesManager
    {
        public bool Add(NoteModel noteModel, string userId);
        public List<Note> GetAll(string userId);
        public List<Note> Filter(List<Note> notes, ListRequest req);
        public bool Update(NoteModel noteModel, string userId);
        public Note GetById(string id);
        public bool Delete(string id);

    }
}
