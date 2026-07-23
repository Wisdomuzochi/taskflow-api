using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services;

public class TaskService
{
    public TaskItem CreerTache(string titre)
    {
        if (string.IsNullOrWhiteSpace(titre))
        {
            throw new ArgumentException("Le titre est obligatoire.", nameof(titre));
        }

        return new TaskItem
        {
            Id = Guid.NewGuid(),
            Titre = titre,
            Statut = TaskItemStatus.AFaire
        };
    }
}