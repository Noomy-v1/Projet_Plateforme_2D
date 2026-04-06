/*
 NOEMIE GIL
 */
using System.Collections;
using UnityEngine;

public class InterrupteurPorte : MonoBehaviour
{
    // Référence à l'animateur de la porte
    public Animator animPorte;

    // Collisionneur de la porte à désactiver
    public Collider2D colliderPorte;

    // Durée avant la fermeture automatique
    public float tempsOuverture = 60f;

    // État actuel du mécanisme
    private bool estActif = false;

    // Pistes audio pour les différents états
    public AudioClip sonSwitch;
    public AudioClip sonOuverture;
    public AudioClip sonFermeture;

    // Composant audio local
    private AudioSource audioSource;

    // Initialisation
    void Start()
    {
        // Récupère l'AudioSource attaché
        audioSource = GetComponent<AudioSource>();
    }

    // Déclenché quand le joueur touche l'interrupteur
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si c'est le joueur et si la minuterie n'est pas déjà lancée
        if (collision.CompareTag("Player") && !estActif)
        {
            // Lance la séquence d'ouverture temporisée
            StartCoroutine(ActiverMinuterie());
        }

        // Joue le son de l'interrupteur
        if (sonSwitch != null)
        {
            audioSource.PlayOneShot(sonSwitch);
        }
    }

    // Coroutine gérant le cycle ouverture/fermeture
    private IEnumerator ActiverMinuterie()
    {
        estActif = true;

        // --- OUVERTURE ---
        if (animPorte != null)
        {
            // Lance l'animation d'ouverture
            animPorte.SetBool("estOuverte", true);
        }

        if (colliderPorte != null)
        {
            // Désactive le mur invisible/collisionneur
            colliderPorte.enabled = false;
        }

        if (sonOuverture != null)
        {
            // Joue le son d'ouverture
            audioSource.PlayOneShot(sonOuverture);
        }

        // --- CHRONOMÈTRE ---
        // Attend la durée définie avant de continuer
        yield return new WaitForSeconds(tempsOuverture);

        // --- FERMETURE ---
        if (animPorte != null)
        {
            // Lance l'animation de fermeture
            animPorte.SetBool("estOuverte", false);
        }

        if (colliderPorte != null)
        {
            // Réactive les collisions de la porte
            colliderPorte.enabled = true;
        }

        if (sonFermeture != null)
        {
            // Joue le son de fermeture
            audioSource.PlayOneShot(sonFermeture);
        }

        // Prêt pour une nouvelle activation
        estActif = false;
    }
}