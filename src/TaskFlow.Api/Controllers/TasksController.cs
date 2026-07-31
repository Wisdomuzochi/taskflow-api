using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Models;
using TaskFlow.Api.Services;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    public class CreerTacheRequete
    {
        public string Titre { get; set; } = string.Empty;
    }

    [HttpPost]
    public IActionResult CreerTache([FromBody] CreerTacheRequete requete)
    {
        try
        {
            var tache = _taskService.CreerTache(requete.Titre);
            return Created($"/api/tasks/{tache.Id}", tache);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult ListerTaches()
    {
        var taches = _taskService.ListerTaches();
        return Ok(taches);
    }

    public class ChangerStatutRequete
    {
      public TaskItemStatus NouveauStatut { get; set; }

    }

    [HttpPut("{id}")]
    public IActionResult ChangerStatut(Guid id, [FromBody] ChangerStatutRequete requete)
    {
        var tache = _taskService.ChangerStatut(id, requete.NouveauStatut);

        if (tache is null)
        {
            return NotFound();
        }

        return Ok(tache);
    } 
}