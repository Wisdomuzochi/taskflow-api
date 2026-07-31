# TaskFlow API

API REST ASP.NET Core simulant un tableau Kanban (créer, lister, déplacer, supprimer des tâches).

Projet d'entraînement personnel, réalisé en préparation d'une alternance Développeur .NET
chez MAF (équipe DOC, application GED). L'objectif n'est pas le Kanban en lui-même, mais
la maîtrise des fondamentaux .NET : structure de solution, TDD, injection de dépendances,
API REST, tests d'intégration.

## Stack technique

- .NET 8 (SDK 8.0.129)
- ASP.NET Core Web API
- xUnit (tests unitaires et d'intégration)
- Swagger / OpenAPI

## Architecture

taskflow-api/
├── src/
│ └── TaskFlow.Api/
│ ├── Controllers/ # Points d'entrée HTTP
│ ├── Services/ # Logique métier
│ ├── Models/ # Entités du domaine
│ └── Program.cs # Point d'entrée, configuration DI
└── tests/
└── TaskFlow.Api.Tests/
├── TaskServiceTests.cs # Tests unitaires
└── TasksControllerTests.cs # Tests d'intégration

Le stockage est actuellement en mémoire (`AddSingleton<TaskService>`), sans base de données —
choix volontaire pour ce projet d'entraînement, focalisé sur les fondamentaux REST/DI/TDD.

## Lancer le projet

```bash
dotnet build
dotnet run --project src/TaskFlow.Api
```

L'API est accessible sur le port indiqué au démarrage, avec Swagger UI disponible en
environnement de développement.

## Lancer les tests

```bash
dotnet test
```

14 tests (8 unitaires, 6 d'intégration), tous écrits en TDD (Red → Green → Refactor).

## Endpoints disponibles

| Méthode | Route              | Description                        |
|---------|---------------------|-------------------------------------|
| POST    | /api/tasks          | Créer une tâche                     |
| GET     | /api/tasks          | Lister toutes les tâches            |
| PUT     | /api/tasks/{id}     | Changer le statut d'une tâche       |
| DELETE  | /api/tasks/{id}     | Supprimer une tâche                 |

## Décisions techniques notables

- **`TaskItemStatus` plutôt que `TaskStatus`** : collision de nom évitée avec
  `System.Threading.Tasks.TaskStatus` du framework .NET (erreur CS0104).
- **`AddSingleton` plutôt que `AddScoped`** pour `TaskService` : nécessaire tant que le
  stockage est en mémoire, pour que les données persistent entre les requêtes HTTP.
  Ce choix sera réévalué lors du passage à une vraie base de données (Entity Framework Core),
  où `AddScoped` redeviendra la norme.

  