/*
 ARPAN NATH
 */

using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Déclenché lorsqu'un objet entre dans la zone 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet est le joueur
        if (collision.CompareTag("Player"))
        {
            // Récupère la santé du joueur
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // Vérifie si le script est bien attaché
            if (playerHealth != null)
            {
                // Provoque la mort par chute
                playerHealth.FallDeath();
            }
        }
    }
}