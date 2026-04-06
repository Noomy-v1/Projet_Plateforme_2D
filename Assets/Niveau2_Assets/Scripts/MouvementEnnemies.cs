/*
 NOEMIE GIL
 */
using UnityEngine;

public class MouvementEnnemies : MonoBehaviour
{
    // Points de repère pour la patrouille
    public Transform pointA;
    public Transform pointB;

    // Vitesse de déplacement
    public float vitesse = 2f;

    // Destination actuelle et composant visuel
    private Transform cibleActuelle;
    private SpriteRenderer spriteRenderer;

    // Initialisation
    void Start()
    {
        // Définit la première cible
        cibleActuelle = pointA;

        // Récupère le SpriteRenderer dans l'objet ou ses enfants
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Mise à jour à chaque frame
    void Update()
    {
        // --- DÉPLACEMENT ---
        // Déplace l'ennemi vers la cible actuelle
        transform.position = Vector2.MoveTowards(transform.position, cibleActuelle.position, vitesse * Time.deltaTime);

        // --- CHANGEMENT DE DIRECTION ---
        // Vérifie si la cible est atteinte
        if (Vector2.Distance(transform.position, cibleActuelle.position) < 0.1f)
        {
            if (cibleActuelle == pointA)
            {
                // Change vers le point B et oriente le sprite
                cibleActuelle = pointB;
                if (spriteRenderer != null) spriteRenderer.flipX = true;
            }
            else
            {
                // Change vers le point A et oriente le sprite
                cibleActuelle = pointA;
                if (spriteRenderer != null) spriteRenderer.flipX = false;
            }
        }
    }

    // --- COLLISION AVEC LE JOUEUR ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si l'objet touché est le joueur
        if (collision.gameObject.CompareTag("Player"))
        {
            // Appelle la méthode pour retirer une vie au joueur
            collision.gameObject.GetComponent<MouvementJoueur>().PerdreVie();
        }
    }
}