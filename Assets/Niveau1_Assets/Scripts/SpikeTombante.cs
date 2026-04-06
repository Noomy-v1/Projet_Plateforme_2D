/*
 ARPAN NATH
 */
using UnityEngine;

public class SpikeTombante : MonoBehaviour
{
    // Composant physique de la pointe
    private Rigidbody2D rb;

    // Détecteur de collision
    private Collider2D spikeCollider;

    // État de la chute (tombée ou non)
    private bool hasFallen = false;

    // Initialisation
    void Start()
    {
        // Récupère les composants attachés
        rb = GetComponent<Rigidbody2D>();
        spikeCollider = GetComponent<Collider2D>();

        // Désactive la gravité (maintient en l'air)
        rb.gravityScale = 0;

        // Désactive les collisions initialement
        spikeCollider.enabled = false;
    }

    // Déclenche la chute
    public void Drop()
    {
        // Vérifie si la pointe n'est pas déjà tombée
        if (!hasFallen)
        {
            // Marque comme tombée
            hasFallen = true;

            // Active les collisions pour blesser le joueur
            spikeCollider.enabled = true;

            // Active la gravité avec une force accrue (tombe vite)
            rb.gravityScale = 3;
        }
    }
}