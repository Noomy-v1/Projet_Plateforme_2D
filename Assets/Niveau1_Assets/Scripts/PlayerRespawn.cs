/*
 ARPAN NATH
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    // Position de réapparition sauvegardée
    private Vector3 checkpointPosition;

    // Composant physique du joueur
    private Rigidbody2D rb;

    // Initialisation
    private void Start()
    {
        // Sauvegarde la position de départ comme premier point de contrôle
        checkpointPosition = transform.position;

        // Récupère le composant Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    // Définit un nouveau point de contrôle
    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        // Met à jour la position de sauvegarde
        checkpointPosition = newCheckpoint;
    }

    // Réinitialise le joueur au dernier point de contrôle
    public void Respawn()
    {
        // Téléporte le joueur à la position sauvegardée
        transform.position = checkpointPosition;

        // Vérifie si le composant physique existe
        if (rb != null)
        {
            // Annule la vitesse (évite de garder l'élan de la chute)
            rb.linearVelocity = Vector2.zero;
        }
    }
}