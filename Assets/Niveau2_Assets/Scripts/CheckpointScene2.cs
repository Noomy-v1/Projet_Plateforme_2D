/*
    NOEMIE GIL
 */
using UnityEngine;

public class CheckpointScene2 : MonoBehaviour
{
    // Son joué au passage du checkpoint
    public AudioClip sonCheckpoint;

    // Composant audio local
    private AudioSource audioSource;

    // Système de particules pour l'effet visuel
    public ParticleSystem confettis;

    // Initialisation
    void Start()
    {
        // Récupère le composant AudioSource attaché
        audioSource = GetComponent<AudioSource>();
    }

    // Déclenché lorsqu'un objet traverse la zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet entrant est le joueur
        if (collision.gameObject.CompareTag("Player"))
        {
            // Met à jour la position de réapparition dans le script du joueur
            MouvementJoueur.positionCheckpoint = transform.position;

            // Marque le checkpoint comme validé
            MouvementJoueur.aToucheCheckpoint = true;

            //Debug.Log("Checkpoint sauvegardé !");
        }

        // Vérifie si une piste audio est assignée
        if (sonCheckpoint != null)
        {
            // Joue le son de manière indépendante à la position actuelle
            AudioSource.PlayClipAtPoint(sonCheckpoint, transform.position);
        }

        // Vérifie si un système de particules est assigné
        if (confettis != null)
        {
            // Déclenche l'animation des confettis
            confettis.Play();
        }
    }
}