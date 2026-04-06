/*
 ARPAN NATH
 */
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    // Composant gérant les animations
    private Animator animator;

    // Puissance du saut
    public float bounceForce = 12f;

    // Éléments audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSound;

    // Initialisation
    void Start()
    {
        // Récupère l'Animator attaché à l'objet
        animator = GetComponent<Animator>();
    }

    // Déclenché quand un objet entre dans la zone (le trampoline)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet est le joueur
        if (collision.CompareTag("Player"))
        {
            // jouer l'animation
            animator.SetTrigger("Jump");

            // jouer le son
            if (audioSource != null && bounceSound != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }

            // faire rebondir le joueur
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            // Applique la force vers le haut si le composant physique existe
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            }
        }
    }
}