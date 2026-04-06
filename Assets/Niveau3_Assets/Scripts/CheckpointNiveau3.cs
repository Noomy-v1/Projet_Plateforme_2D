/*
        RIDI OTOKO
 
 */
using UnityEngine;

public class CheckpointNiveau3 : MonoBehaviour
{
    // État d'activation du checkpoint (pour éviter les répétitions)
    private bool active = false;

    // Déclenché lorsqu'un objet traverse la zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sort si le checkpoint est déjà actif
        if (active) return;

        // Vérifie si l'objet entrant est le joueur
        if (collision.CompareTag("Player"))
        {
            // Récupère la référence au script du joueur
            playerScript joueur = collision.GetComponent<playerScript>();

            if (joueur != null)
            {
                // Enregistre la position actuelle comme point de réapparition
                joueur.SetCheckpoint(transform.position);
                // Marque le checkpoint comme utilisé
                active = true;
            }

            // Message de confirmation dans la console
            Debug.Log("Checkpoint activé");
        }
    }
}