public class NoteBuilder
{
    private Note _note;

    public NoteBuilder()
    {
        _note = new Note();
    }

    public NoteBuilder WithId(int id)
    {
        _note.Id = id;
        return this;
    }

    public NoteBuilder WithTitle(string title)
    {
        _note.Title = title;
        return this;
    }

    public NoteBuilder WithContent(string content)
    {
        _note.Content = content;
        return this;
    }

    public Note Build()
    {
        return _note;
    }
}
