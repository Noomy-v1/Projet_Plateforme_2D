/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class Porte : MonoBehaviour
{
    // État interne pour savoir si la porte a déjà été franchie
    private bool estOuverte = false;

    // Méthode appelée par l'interrupteur pour libérer le passage
    public void Ouvrir()
    {
        // Sécurité pour ne pas exécuter le code plusieurs fois
        if (estOuverte) return;

        estOuverte = true;

        // --- SUPPRESSION VISUELLE ---
        // Récupère le composant qui affiche l'image de la porte
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            // Détruit uniquement le composant de rendu (l'objet reste présent)
            Destroy(sr);
        }

        // --- SUPPRESSION PHYSIQUE ---
        // Récupère le collisionneur qui bloque le joueur
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            // Détruit le collisionneur pour permettre le passage
            Destroy(col);
        }

        Debug.Log("Porte ouverte");
    }
}