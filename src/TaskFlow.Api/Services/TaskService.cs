using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services;

public class TaskService
{
    private readonly List<TaskItem> _taches = new();

    public TaskItem CreerTache(string titre)
    {
        if (string.IsNullOrWhiteSpace(titre))
        {
            throw new ArgumentException("Le titre est obligatoire.", nameof(titre));
        }

        var tache = new TaskItem
        {
            Id = Guid.NewGuid(),
            Titre = titre,
            Statut = TaskItemStatus.AFaire
        };

        _taches.Add(tache);

        return tache;
    }

    public IEnumerable<TaskItem> ListerTaches()
    {
        return _taches;
    }

    public TaskItem? ChangerStatut(Guid id, TaskItemStatus nouveauStatut)
    {
        var tache = _taches.FirstOrDefault(t => t.Id == id);

        if (tache is null)
        {
            return null;
        }

        tache.Statut = nouveauStatut;

        return tache;
    }

    public bool SupprimerTache(Guid id)
    {
        var tache = _taches.FirstOrDefault(t => t.Id == id);

        if (tache is null)
        {
          return false;
        }

        _taches.Remove(tache);

       return true;
    }
}