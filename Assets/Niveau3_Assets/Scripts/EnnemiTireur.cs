/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class EnnemiTireur : MonoBehaviour
{
    // Le préfabriqué du projectile à tirer
    public GameObject projectile;

    // Vitesse de déplacement du projectile
    public float vitesseProjectile = 3f;

    // Emplacement précis d'où sort le projectile
    public Transform pointTir;

    // Référence au propre collisionneur de l'ennemi
    private Collider2D colliderEnnemi;

    // Initialisation
    void Start()
    {
        // Récupère le collider pour éviter de se tirer dessus
        colliderEnnemi = GetComponent<Collider2D>();
    }

    // Méthode pour créer et lancer le projectile
    public void LancerProjectile()
    {
        // Définit la position d'apparition (par défaut celle de l'ennemi)
        Vector3 positionSpawn = transform.position;

        // Si un point de tir est assigné, on utilise sa position
        if (pointTir != null)
        {
            positionSpawn = pointTir.position;
        }

        // Crée une instance du projectile dans la scène
        GameObject p = Instantiate(projectile, positionSpawn, Quaternion.identity);

        // Accède au composant physique du projectile
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Propulse le projectile vers la gauche
            rb.linearVelocity = Vector2.left * vitesseProjectile;
        }

        // --- GESTION DES COLLISIONS ---
        // Récupère le collider du nouveau projectile
        Collider2D colliderProjectile = p.GetComponent<Collider2D>();

        // Empêche le projectile de heurter l'ennemi qui vient de le lancer
        if (colliderProjectile != null && colliderEnnemi != null)
        {
            Physics2D.IgnoreCollision(colliderProjectile, colliderEnnemi);
        }
    }
}