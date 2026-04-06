/*
 NOEMIE GIL
 */
using UnityEngine;
using System.Collections;

public class PlateformeTombante : MonoBehaviour
{
    [Header("Réglages")]
    // Temps avant que la plateforme ne commence à tomber
    public float delaiAvantChute = 0.2f;
    // Temps avant que la plateforme ne soit masquée
    public float tempsAvantDisparition = 2f;

    // Données de transformation d'origine
    private Vector2 positionInitiale;
    private Quaternion rotationInitiale;

    // Composants internes
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // État d'activation
    private bool estTouche = false;

    // Initialisation
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Sauvegarde de l'état initial pour la réinitialisation
        positionInitiale = transform.position;
        rotationInitiale = transform.rotation;
    }

    // Détection de collision avec le joueur
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le joueur touche la plateforme pour la première fois
        if (collision.gameObject.CompareTag("Player") && !estTouche)
        {
            estTouche = true;
            // Lance la séquence de chute
            StartCoroutine(FaireTomber());
        }
    }

    // Coroutine gérant la chute et la disparition
    private IEnumerator FaireTomber()
    {
        // Attend avant de déclencher la physique
        yield return new WaitForSeconds(delaiAvantChute);

        // Rend la plateforme sensible à la gravité
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Attend que l'objet soit tombé loin avant de le cacher
        yield return new WaitForSeconds(tempsAvantDisparition);

        // Désactive le visuel et les calculs physiques (sans détruire l'objet)
        sprite.enabled = false;
        rb.simulated = false;
    }

    // Remet la plateforme à son état de départ
    public void Reinitialiser()
    {
        // Arrête les chutes en cours
        StopAllCoroutines();

        // Réinitialisation des états
        estTouche = false;
        sprite.enabled = true;

        // Réactive et remet la physique en mode statique (Kinematic)
        rb.simulated = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Repositionne la plateforme à son origine
        transform.position = positionInitiale;
        transform.rotation = rotationInitiale;
    }
}