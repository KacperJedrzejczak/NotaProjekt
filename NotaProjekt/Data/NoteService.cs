using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorNotatnik.Data
{
    public class NoteService
    {
        private List<Note> notes = new List<Note>
        {
            new Note { Id = 1, Title = "Tytuł = Test", Content = "Zawatość = Test" },
        };

        public Task<List<Note>> GetNotes()
        {
            return Task.FromResult(notes);
        }

        public Task<Note> GetNoteById(int id)
        {
            return Task.FromResult(notes.FirstOrDefault(n => n.Id == id));
        }

        public Task UpdateNote(Note note)
        {
            var existingNote = notes.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote != null)
            {
                existingNote.Title = note.Title;
                existingNote.Content = note.Content;
            }
            return Task.CompletedTask;
        }

        public Task AddNote(string title, string content)
        {
            var newNote = new Note
            {
                Id = notes.Count + 1, // ID nowej notatki
                Title = title,
                Content = content
            };
            notes.Add(newNote);
            return Task.CompletedTask;
        }

        public Task DeleteNote(int id)
        {
            var noteToRemove = notes.FirstOrDefault(n => n.Id == id);
            if (noteToRemove != null)
            {
                notes.Remove(noteToRemove);
            }
            return Task.CompletedTask;
        }
    }
}
