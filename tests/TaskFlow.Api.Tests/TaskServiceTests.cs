using TaskFlow.Api.Models;
using TaskFlow.Api.Services;

namespace TaskFlow.Api.Tests;

public class TaskServiceTests
{
    [Fact]
    public void CreerTache_AvecTitreValide_RetourneTacheAvecStatutAFaire()
    {
        // Arrange
        var service = new TaskService();

        // Act
        var tache = service.CreerTache("Corriger le bug de login");

        // Assert
        Assert.Equal("Corriger le bug de login", tache.Titre);
        Assert.Equal(TaskItemStatus.AFaire, tache.Statut);
        Assert.NotEqual(Guid.Empty, tache.Id);
    }

    [Fact]
    public void CreerTache_AvecTitreVide_LeveUneException()
    {
        // Arrange
        var service = new TaskService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => service.CreerTache(""));
    }

    [Fact]
    public void ListerTaches_ApresCreationDeDeuxTaches_RetourneLesDeuxTaches()
    {
        // Arrange
        var service = new TaskService();
        service.CreerTache("Tâche 1");
        service.CreerTache("Tâche 2");

        // Act
        var taches = service.ListerTaches();

        // Assert
        Assert.Equal(2, taches.Count());
    }
   
   [Fact]
   public void ChangerStatut_AvecIdExistant_MetAJourLeStatut()
   {
        // Arrange
        var service = new TaskService();
        var tache = service.CreerTache("Tâche à déplacer");

        // Act
        var tacheModifiee = service.ChangerStatut(tache.Id, TaskItemStatus.EnCours);

        // Assert
        Assert.Equal(TaskItemStatus.EnCours, tacheModifiee!.Statut);
    }

    [Fact]
    public void ChangerStatut_AvecIdInexistant_RetourneNull()
    {
        // Arrange
        var service = new TaskService();

        // Act
        var resultat = service.ChangerStatut(Guid.NewGuid(), TaskItemStatus.EnCours);

        // Assert
        Assert.Null(resultat);
    }  
    
    [Fact]
    public void SupprimerTache_AvecIdExistant_RetourneTrue()
    {
        // Arrange
        var service = new TaskService();
        var tache = service.CreerTache("Tâche à supprimer");

        // Act
        var resultat = service.SupprimerTache(tache.Id);

        // Assert
        Assert.True(resultat);
        Assert.Empty(service.ListerTaches());
    } 

    [Fact]
    public void SupprimerTache_AvecIdInexistant_RetourneFalse()
    {
        // Arrange
        var service = new TaskService();

        // Act
        var resultat = service.SupprimerTache(Guid.NewGuid());

        // Assert
        Assert.False(resultat);
    }
}

