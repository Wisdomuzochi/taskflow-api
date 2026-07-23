using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services;

public class TaskService
{
    public TaskItem CreerTache(string titre)
    {
        return new TaskItem
        {
            Id = Guid.NewGuid(),
            Titre = titre,
            Statut = TaskItemStatus.AFaire
        };
    }
}