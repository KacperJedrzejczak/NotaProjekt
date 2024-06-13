using System;

namespace BlazorNotatnik.Data
{
    public class NoteChange
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string ChangeType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
