/*
 ARPAN NATH
 */
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    // Vitesse de déplacement
    public float speed = 2f;

    // Distance maximale de patrouille
    public float distance = 5f;

    // Position initiale de l'abeille
    private Vector3 startPosition;

    // Sens de déplacement (1 = droite, -1 = gauche)
    private int direction = 1;

    // Composant visuel de l'abeille
    private SpriteRenderer sprite;

    // Initialisation au lancement
    void Start()
    {
        // Sauvegarde la position de départ
        startPosition = transform.position;

        // Récupère le composant SpriteRenderer
        sprite = GetComponent<SpriteRenderer>();
    }

    // Mise à jour à chaque frame
    void Update()
    {
        // Déplace l'abeille horizontalement
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Vérifie si la distance limite est atteinte
        if (Vector2.Distance(startPosition, transform.position) >= distance)
        {
            // Inverse la direction
            direction *= -1;
        }

        // Oriente le sprite selon la direction
        if (direction > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}