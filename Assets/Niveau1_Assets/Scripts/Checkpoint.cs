/*
 ARPAN NATH
 */

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Empêche de réactiver le checkpoint plusieurs fois
    private bool active = false;

    // Déclenché quand un objet entre dans la zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore la suite si déjà activé
        if (active) return;

        // Vérifie si l'objet entrant est le joueur
        if (collision.CompareTag("Player"))
        {
            // Récupère le script du joueur
            MouvementJoueur_lvl1 joueur = collision.GetComponent<MouvementJoueur_lvl1>();

            // Sécurité : confirme que le script a bien été trouvé
            if (joueur != null)
            {
                // Envoie la position du checkpoint au joueur
                joueur.SetCheckpoint(transform.position);

                // Verrouille ce checkpoint
                active = true;
            }

            // Confirmation de test dans la console
            Debug.Log("Checkpoint activé");
        }
    }
}