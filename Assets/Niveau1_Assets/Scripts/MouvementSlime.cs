/*
 ARPAN NATH
 */
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    // Vitesse de déplacement
    public float speed = 2f;

    // Détection du sol (vide devant)
    public Transform groundCheck;
    public float groundCheckDistance = 0.5f;

    // Détection d'un obstacle/mur
    public Transform wallCheck;
    public float wallCheckDistance = 0.2f;

    // Calque de collision (sol et murs)
    public LayerMask groundLayer;

    // Composants internes
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Direction actuelle (-1 = gauche, 1 = droite)
    private int direction = -1;

    // Initialisation
    void Start()
    {
        // Récupère les composants
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Mise à jour physique (à intervalles réguliers)
    void FixedUpdate()
    {
        // Applique la vitesse horizontale constante
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        // Lance un rayon vers le bas pour vérifier la présence de sol
        bool hasGroundAhead = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        // Lance un rayon horizontalement pour vérifier la présence d'un mur
        bool hitWall = Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * direction,
            wallCheckDistance,
            groundLayer
        );

        // Fait demi-tour si un bord ou un mur est détecté
        if (!hasGroundAhead || hitWall)
        {
            TurnAround();
        }

        // Oriente le visuel selon la direction
        sprite.flipX = direction > 0;
    }

    // Gère le changement de direction
    void TurnAround()
    {
        // Inverse la valeur de direction
        direction *= -1;

        // Inverse la position locale en X du détecteur de sol
        Vector3 groundPos = groundCheck.localPosition;
        groundPos.x *= -1;
        groundCheck.localPosition = groundPos;

        // Inverse la position locale en X du détecteur de mur
        Vector3 wallPos = wallCheck.localPosition;
        wallPos.x *= -1;
        wallCheck.localPosition = wallPos;
    }

    // Dessine des repères visuels dans l'éditeur Unity (pour le débogage)
    void OnDrawGizmosSelected()
    {
        // Dessine la ligne de détection du sol en rouge
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                groundCheck.position,
                groundCheck.position + Vector3.down * groundCheckDistance
            );
        }

        // Dessine la ligne de détection de mur en bleu
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Vector3 dir = Vector3.right * direction;
            Gizmos.DrawLine(
                wallCheck.position,
                wallCheck.position + dir * wallCheckDistance
            );
        }
    }
}