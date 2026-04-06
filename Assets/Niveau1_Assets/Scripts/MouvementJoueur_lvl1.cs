/*
 ARPAN NATH
 */
using UnityEngine;

public class MouvementJoueur_lvl1 : MonoBehaviour
{
    // Variables de mouvement
    public float speed = 5f;
    public float jumpForce = 10f;

    // Détection du sol
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // Élément visuel (pour l'orientation)
    public Transform visual;

    // Variables de santé
    public int maxHealth = 3;
    private int currentHealth;

    // Lien vers le gestionnaire de fin
    public GameOverManager gameOverManager;

    // Composants internes
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isDead = false;

    // Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;

    // Point de sauvegarde
    private Vector2 positionRespawn;

    // Initialisation
    void Start()
    {
        // Récupère les composants
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Initialise la santé
        currentHealth = maxHealth;
    }

    // Mise à jour à chaque frame
    void Update()
    {
        // Bloque les actions si mort
        if (isDead) return;

        // Récupère l'entrée horizontale (gauche/droite)
        float move = Input.GetAxis("Horizontal");

        // Applique la vitesse horizontale
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Vérifie si le joueur touche le sol
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Met à jour les animations
        animator.SetBool("isRun", move != 0);
        animator.SetBool("isJump", !isGrounded);

        // Oriente le visuel vers la droite
        if (move > 0)
        {
            visual.localScale = new Vector3(
                Mathf.Abs(visual.localScale.x),
                visual.localScale.y,
                visual.localScale.z
            );
        }
        // Oriente le visuel vers la gauche
        else if (move < 0)
        {
            visual.localScale = new Vector3(
                -Mathf.Abs(visual.localScale.x),
                visual.localScale.y,
                visual.localScale.z
            );
        }

        // Déclenche le saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Applique la force vers le haut
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // Joue le son de saut
            audioSource.PlayOneShot(jumpSound);
        }

        // Déclenche l'attaque
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("attack");
        }
    }

    // Gestion des dégâts
    public void TakeDamage()
    {
        if (isDead) return;

        // Réduit la santé
        currentHealth--;

        // Déclenche l'animation de blessure
        animator.SetTrigger("hurt");

        // Vérifie si la santé est épuisée
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Gestion de la mort
    public void Die()
    {
        if (isDead) return;

        // Marque comme mort
        isDead = true;

        // Joue l'animation de mort
        animator.SetTrigger("die");

        // Stoppe tout mouvement
        rb.linearVelocity = Vector2.zero;

        // Affiche l'écran de fin de partie
        gameOverManager.ShowGameOver();
    }

    // Sauvegarde un nouveau point de réapparition
    public void SetCheckpoint(Vector2 nouvellePosition)
    {
        positionRespawn = nouvellePosition;
    }
}