/*
 ARPAN NATH
 */
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Points de vie initiaux
    public int health = 3;

    // Références aux images UI des cœurs
    public Image heart1;
    public Image heart2;
    public Image heart3;

    // Éléments audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;

    // Composants et gestionnaires
    private Animator animator;
    private bool isDead = false;
    private GameOverManager gameOverManager;
    private PlayerRespawn playerRespawn;

    // Initialisation
    void Start()
    {
        // Récupère les composants locaux et globaux
        animator = GetComponent<Animator>();
        gameOverManager = Object.FindFirstObjectByType<GameOverManager>();
        playerRespawn = GetComponent<PlayerRespawn>();

        // Affiche la santé initiale
        UpdateHearts();
    }

    // Applique les dégâts standard
    public void TakeDamage(int damage)
    {
        // Ignore si déjà mort
        if (isDead) return;

        // Réduit la santé
        health -= damage;

        // Joue l'animation de blessure
        if (animator != null)
        {
            animator.SetTrigger("hurt");
        }

        // Joue le son de blessure
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // Actualise l'interface
        UpdateHearts();

        // Vérifie si la santé est à zéro
        if (health <= 0)
        {
            Die();
        }
    }

    // Gère la mort spécifique par chute dans le vide
    public void FallDeath()
    {
        if (isDead) return;

        // Retire un seul point de vie
        health -= 1;

        // Joue le son
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // Actualise l'interface
        UpdateHearts();

        // Vérifie si mort définitive, sinon réapparition
        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (playerRespawn != null)
            {
                playerRespawn.Respawn();
            }
        }
    }

    // Actualise la visibilité des cœurs dans l'UI
    void UpdateHearts()
    {
        heart1.enabled = health >= 1;
        heart2.enabled = health >= 2;
        heart3.enabled = health >= 3;
    }

    // Gère la mort définitive
    void Die()
    {
        if (isDead) return;

        // Marque l'état mort
        isDead = true;

        // Joue l'animation
        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        // Trace dans la console
        Debug.Log("Le joueur est mort");

        // Affiche l'écran de fin
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }

    // Détecte les dégâts continus sur les piques
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si le joueur touche un objet tagué "Spike"
        if (collision.gameObject.CompareTag("Spike"))
        {
            // Inflige 1 point de dégât
            TakeDamage(1);
        }
    }
}