namespace TaskFlow.Api.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public TaskItemStatus Statut { get; set; }
}