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

        private List<NoteChange> noteChanges = new List<NoteChange>();

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
                var change = new NoteChange
                {
                    Id = noteChanges.Count + 1,
                    NoteId = updatedNote.Id,
                    ChangeType = "Updated",
                    Title = updatedNote.Title,
                    Content = updatedNote.Content,
                    ChangeDate = DateTime.Now
                };
                noteChanges.Add(change);

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

            var change = new NoteChange
            {
                Id = noteChanges.Count + 1,
                NoteId = newNote.Id,
                ChangeType = "Added",
                Title = title,
                Content = content,
                ChangeDate = DateTime.Now
            };
            noteChanges.Add(change);

            return Task.CompletedTask;
        }

        public Task DeleteNote(int id)
        {
            var noteToRemove = notes.FirstOrDefault(n => n.Id == id);
            if (noteToRemove != null)
            {
                var change = new NoteChange
                {
                    Id = noteChanges.Count + 1,
                    NoteId = id,
                    ChangeType = "Deleted",
                    Title = noteToRemove.Title,
                    Content = noteToRemove.Content,
                    ChangeDate = DateTime.Now
                };
                noteChanges.Add(change);

                notes.Remove(noteToRemove);
            }
            return Task.CompletedTask;
        }
        public Task<List<Note>> SearchNotesByTitle(string title)
        {
            var result = notes.Where(n => n.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            return Task.FromResult(result);
        }

        public Task<List<NoteChange>> GetNoteChanges()
        {
            return Task.FromResult(noteChanges);
        }
    }
}
