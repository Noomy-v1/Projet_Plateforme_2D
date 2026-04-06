/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    // Déclenché lors d'un impact physique avec un autre objet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le projectile touche le joueur
        if (collision.gameObject.CompareTag("Player"))
        {
            // Récupère le script de mouvement du joueur pour interagir avec sa santé
            MouvementJoueur joueur = collision.gameObject.GetComponent<MouvementJoueur>();

            if (joueur != null)
            {
                // Appelle la fonction qui retire une vie
                joueur.PerdreVie();
            }

            // Détruit la boule de feu après l'impact
            Destroy(gameObject);
            return;
        }

        // Si le projectile touche n'importe quel autre objet physique (sol, mur, etc.)
        Destroy(gameObject);
    }
}