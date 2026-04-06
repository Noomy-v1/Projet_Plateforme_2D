/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    // Référence vers le script de la porte à ouvrir
    public Porte porte;

    // Visuel de l'interrupteur une fois enclenché
    public Sprite spriteActive;

    // Composant pour l'affichage du sprite et état interne
    private SpriteRenderer sr;
    private bool active = false;

    // Initialisation
    void Start()
    {
        // Récupère le SpriteRenderer sur cet objet
        sr = GetComponent<SpriteRenderer>();
    }

    // Déclenché quand le joueur entre dans la zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sort si déjà activé
        if (active) return;

        // Vérifie si c'est le joueur qui touche l'interrupteur
        if (collision.CompareTag("Player"))
        {
            active = true;

            // changer le sprite de l'interrupteur pour le visuel "activé"
            if (spriteActive != null)
            {
                sr.sprite = spriteActive;
            }

            // Appelle la méthode d'ouverture sur la porte associée
            if (porte != null)
            {
                porte.Ouvrir();
            }

            // Désactive le déclencheur pour empêcher toute nouvelle interaction
            GetComponent<Collider2D>().enabled = false;

            Debug.Log("Interrupteur activé");
        }
    }
}