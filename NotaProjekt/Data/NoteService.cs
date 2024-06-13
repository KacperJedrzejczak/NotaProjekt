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
                var noteChange = new NoteChange
                {
                    Id = noteChanges.Count + 1, 
                    NoteId = existingNote.Id,
                    Title = existingNote.Title,                   
                    Content = existingNote.Content,                  
                    Timestamp = DateTime.Now,
                    ChangeType = "update"
                };

                existingNote.Title = updatedNote.Title;
                existingNote.Content = updatedNote.Content;

                noteChanges.Add(noteChange);
            }
            return Task.CompletedTask;
        }

        public Task AddNote(string title, string content)
        {
            var newNote = new NoteBuilder()
                .WithId(notes.Count + 1)
                .WithTitle(title)
                .WithContent(content)
                .Build();

            notes.Add(newNote);

            // Dodanie do archiwum
            var noteChange = new NoteChange
            {
                Id = noteChanges.Count + 1,
                NoteId = newNote.Id,
                Title = newNote.Title,
                Content = newNote.Content,
                Timestamp = DateTime.Now,
                ChangeType = "create"
            };
            noteChanges.Add(noteChange);

            return Task.CompletedTask;
        }

        public Task DeleteNote(int id)
        {
            var noteToRemove = notes.FirstOrDefault(n => n.Id == id);
            if (noteToRemove != null)
            {
                // Dodanie do archiwum
                var noteChange = new NoteChange
                {
                    Id = noteChanges.Count + 1,
                    NoteId = noteToRemove.Id,
                    Title = noteToRemove.Title,       
                    Content = noteToRemove.Content,            
                    Timestamp = DateTime.Now,
                    ChangeType = "delete"
                };
                noteChanges.Add(noteChange);

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
