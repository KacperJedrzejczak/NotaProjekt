namespace BlazorNotatnik.Data
{
    public class NoteService
    {
        private List<Note> notes = new List<Note>
        {
            new Note { Id = 1, Title = "Zakupy", Content = "Mleko, jajka mąka" },
            new Note { Id = 2, Title = "Przedszkole", Content = "Zawieźć kota na 14:00, sala 102" },
            new Note { Id = 3, Title = "Weterynarz", Content = "Zawieźć dziecko na 10:00, sala 120" },
        };

        public Task<List<Note>> GetNotes()
        {
            return Task.FromResult(notes);
        }

        public Task<Note> GetNoteById(int id)
        {
            var note = notes.FirstOrDefault(n => n.Id == id);
            return Task.FromResult(note);
        }

        public Task UpdateNote(Note updatedNote)
        {
            var existingNote = notes.FirstOrDefault(n => n.Id == updatedNote.Id);
            if (existingNote != null)
            {
                existingNote.Title = updatedNote.Title;
                existingNote.Content = updatedNote.Content;
            }
            return Task.CompletedTask;
        }

        public Task AddNote(string title, string content)
        {
            var newNote = new Note
            {
                Id = notes.Count + 1, 
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
        public Task<List<Note>> SearchNotesByTitle(string title)
        {
            var result = notes.Where(n => n.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            return Task.FromResult(result);
        }
    }
}
