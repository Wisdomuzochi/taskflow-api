using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Tests;

public class TasksControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TasksControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostTasks_AvecTitreValide_Retourne201EtLaTache()
    {
        // Arrange
        var requete = new { Titre = "Corriger le bug de login" };

        // Act
        var reponse = await _client.PostAsJsonAsync("/api/tasks", requete);

        // Assert
        Assert.Equal(HttpStatusCode.Created, reponse.StatusCode);

        var tache = await reponse.Content.ReadFromJsonAsync<TaskItem>();
        Assert.NotNull(tache);
        Assert.Equal("Corriger le bug de login", tache!.Titre);
    }

    [Fact]
    public async Task PostTasks_AvecTitreVide_Retourne400()
    {
        // Arrange
        var requete = new { Titre = "" };

        // Act
        var reponse = await _client.PostAsJsonAsync("/api/tasks", requete);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, reponse.StatusCode);
    }

    [Fact]
    public async Task GetTasks_ApresUnPost_RetourneLaTacheCree()
    {
        // Arrange
        var requete = new { Titre = "Tâche à retrouver" };
        await _client.PostAsJsonAsync("/api/tasks", requete);

        // Act
        var reponse = await _client.GetAsync("/api/tasks");

        // Assert
        Assert.Equal(HttpStatusCode.OK, reponse.StatusCode);

        var taches = await reponse.Content.ReadFromJsonAsync<List<TaskItem>>();
        Assert.NotNull(taches);
        Assert.Contains(taches!, t => t.Titre == "Tâche à retrouver");
    }
    
    [Fact]
    public async Task PutTasks_AvecIdExistant_ChangeLeStatut()
   {
        // Arrange
        var creation = new { Titre = "Tâche à modifier" };
        var reponseCreation = await _client.PostAsJsonAsync("/api/tasks", creation);
        var tacheCreee = await reponseCreation.Content.ReadFromJsonAsync<TaskItem>();

        var requeteModif = new { NouveauStatut = TaskItemStatus.EnCours };

        // Act
        var reponse = await _client.PutAsJsonAsync($"/api/tasks/{tacheCreee!.Id}", requeteModif);

        // Assert
        Assert.Equal(HttpStatusCode.OK, reponse.StatusCode);
        var tacheModifiee = await reponse.Content.ReadFromJsonAsync<TaskItem>();
        Assert.Equal(TaskItemStatus.EnCours, tacheModifiee!.Statut);
    }

    [Fact]
    public async Task PutTasks_AvecIdInexistant_Retourne404()
    {
        // Arrange
        var requeteModif = new { NouveauStatut = TaskItemStatus.EnCours };

        // Act
        var reponse = await _client.PutAsJsonAsync($"/api/tasks/{Guid.NewGuid()}", requeteModif);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, reponse.StatusCode);
    }

    [Fact]
    public async Task DeleteTasks_AvecIdExistant_Retourne204()
    {
        // Arrange
        var creation = new { Titre = "Tâche à supprimer" };
        var reponseCreation = await _client.PostAsJsonAsync("/api/tasks", creation);
        var tacheCreee = await reponseCreation.Content.ReadFromJsonAsync<TaskItem>();

        // Act
        var reponse = await _client.DeleteAsync($"/api/tasks/{tacheCreee!.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, reponse.StatusCode);
    }

    [Fact]
    public async Task DeleteTasks_AvecIdInexistant_Retourne404()
    {
        // Act
        var reponse = await _client.DeleteAsync($"/api/tasks/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, reponse.StatusCode);
    }
}