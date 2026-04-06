# Le survivant - 2D Platformer

Ce projet est un jeu de plateforme 2D réalisé sous **Unity**, mettant l'accent sur la fluidité des contrôles, la variété des obstacles environnementaux et une architecture de code modulaire en **C#**.

## 🎮 Jouer au jeu

Le projet est prêt à être testé ! Vous n'avez pas besoin d'installer Unity pour y jouer.

📥 **[Télécharger la dernière version (Windows/Mac)](https://github.com/Noomy-v1/Projet_Plateforme_2D/releases/download/v1.0.0/LeSurvivant_V1.zip)**

> **Note :** Une fois le fichier `.zip` téléchargé, extrayez tout le contenu du dossier avant de lancer l'exécutable (`.exe` ou `.app`).


## Fonctionnalités Clés

Le projet s'articule autour de trois piliers de développement collaboratif :

### Système de Jeu & Joueur (Développé par Noémie Gil)
* **Contrôleur de personnage complet** : Gestion des déplacements, sauts, escalade (échelles) et système de points de vie.
* **Mécaniques de progression** : Système de checkpoints dynamiques et gestion des niveaux (scènes).
* **Collectables & Score** : Implémentation d'objets interactifs (pièces, bonus) et mise à jour en temps réel de l'UI.
* **Interface Utilisateur (UI)** : Menus de navigation, transitions cinématiques et affichage des statistiques.

### Moteur & Environnement (Développé par Arpan Nath)
* **Physique & Rebond** : Système de trampolines et plateformes mobiles synchronisées.
* **Système de santé global** : Gestion du respawn, zones de mort (Death Zones) et gestionnaire de Game Over.
* **IA de base** : Comportements de patrouille pour ennemis volants (abeilles) et terrestres (slimes).
* **Pièges interactifs** : Détection et chute de pointes (Spikes) basées sur la position du joueur.

### Combat & Interactions (Développé par Ridi Otoko)
* **Système de combat** : Ennemis tireurs, gestion de projectiles (fireballs) et masquage de collisions.
* **Interactions complexes** : Système d'interrupteurs permanents ouvrant des accès (portes) via des événements de script.
* **Pièges de plateforme** : Plateformes "mines" s'effondrant uniquement sous le poids du joueur.
* **IA Avancée** : Patrouilles avec gestion de l'orientation (flip) et détection de cible.

## Technologies Utilisées
* **Moteur** : Unity 2022.3+
* **Langage** : C# (Scripting orienté objet)
* **Interface** : TextMeshPro
* **Physique** : Rigidbody2D, BoxCollider2D, Physics Material 2D

## Structure du Projet
* `/Assets/Scripts` : Contient l'ensemble de la logique documentée.
* `/Assets/Scenes` : Les différents niveaux du jeu, du menu à la victoire.
* `/Assets/Prefabs` : Objets réutilisables (joueur, ennemis, pièges).

## Contrôles
* **Déplacement** : Flèches directionnelles ou `A` / `D`
* **Saut** : `Espace` ou Flèche du haut
* **Grimper (Échelles)** : Flèche du haut / bas
* **Interagir (Portes/Leviers)** : Flèche du haut

## Contexte
Ce projet a été réalisé dans le cadre du cours **Applications de jeux ou simulations** au Cégep. L'objectif était de démontrer notre capacité à travailler en équipe sur un dépôt Git commun et à livrer une expérience de jeu stable et documentée.

---
*Projet réalisé par : Noémie Gil, Arpan Nath & Ridi Otoko.*
