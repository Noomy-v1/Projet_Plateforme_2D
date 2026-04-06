/*
 ARPAN NATH
 */
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    // Référence au script de la pointe associée
    public SpikeTombante spike;

    // Déclenché lorsqu'un objet entre dans la zone 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet qui entre est le joueur
        if (collision.CompareTag("Player"))
        {
            // Appelle la méthode pour faire tomber la pointe
            spike.Drop();
        }
    }
}