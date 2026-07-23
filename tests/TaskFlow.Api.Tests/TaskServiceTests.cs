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
}

