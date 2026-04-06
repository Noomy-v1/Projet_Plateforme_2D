/*
 ARPAN NATH
 */
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    // Quantité de dégâts infligés
    public int damage = 1;

    // Déclenché lors d'une collision 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet touché est le joueur
        if (collision.CompareTag("Player"))
        {
            // Récupère le composant de santé du joueur
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // Vérifie si le composant existe
            if (playerHealth != null)
            {
                // Applique les dégâts
                playerHealth.TakeDamage(damage);
            }
        }
    }
}