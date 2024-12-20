using insight.DataContext;
using insight.Models;

namespace insight.Repository
{
    public interface INotesRepository
    {
        public bool Add(Note note);
        public bool Delete(string id);
        public bool Update(Note note);

        public List<Note> GetAll(string userId);
        public Note GetById(string id);
    }
}
