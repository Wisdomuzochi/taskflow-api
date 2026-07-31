# Suivi du projet — TaskFlow API

Journal de chiffrage : comparaison entre l'estimation initiale de chaque ticket et le
temps réellement passé, dans l'objectif de m'entraîner au chiffrage de tâches (compétence
attendue en alternance : "participer au chiffrage et à l'analyse des tâches").

| Ticket | Description                                      | Estimé | Réel (approx.) | Écart / Notes |
|--------|---------------------------------------------------|--------|-----------------|----------------|
| #1     | Initialiser la solution TaskFlow                  | 15 min | —               | Setup SSH GitHub imprévu, non compté dans l'estimation initiale |
| #2     | Créer une tâche (TDD)                             | 30 min | —               | Collision de namespace TaskStatus → TaskItemStatus, bonne découverte |
| #3     | Exposer la création via POST /api/tasks           | 30-40 min | —            | Plusieurs erreurs de compilation (imports manquants, version de package incompatible, fichier Program.cs supprimé accidentellement) — sous-estimé |
| #4     | Lister les tâches (GET) + stockage en mémoire     | 30 min | —               | Bug Scoped vs Singleton découvert et corrigé — bonne leçon sur le cycle de vie DI |
| #5     | PUT (changer statut) + DELETE                     | 40 min | —               | S'est déroulé sans accroc majeur, les réflexes commencent à être acquis |

## Reste à faire (Projet 1)

- [x] CRUD complet (Create, Read, Update, Delete)
- [x] Tests unitaires + tests d'intégration
- [x] Documentation technique (ce fichier + README)
- [ ] CI/CD (GitHub Actions ou Azure DevOps) — reporté au Projet 2, discipline transversale
- [ ] SonarQube — reporté au Projet 2

## Enseignements clés à retenir pour la suite (Projet 2 — MiniDoc)

- Toujours vérifier les `using` en premier réflexe face à une erreur CS0246
- Attention aux collisions de noms avec le namespace `System` (CS0104)
- Bien choisir le cycle de vie DI (`Scoped`/`Singleton`/`Transient`) selon si l'état doit
  persister entre requêtes ou non — question à se reposer systématiquement pour chaque
  service enregistré
- Toujours vérifier la compatibilité de version d'un package NuGet avec le `TargetFramework`
  du projet avant de l'ajouter