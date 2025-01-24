namespace TasksAPI.Models;

public class Task(int Id, string? Title, string? Description, bool? IsCompleted, DateTime CreatedAt)
{
    public int Id { get; set; } = Id;

    public string? Title { get; set; } = Title;

    public string? Description { get; set; } = Description;

    public bool? IsCompleted { get; set; } = IsCompleted;

    public DateTime CreatedAt { get; set; } = CreatedAt;
}
