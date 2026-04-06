/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class EnnemiPatrouille : MonoBehaviour
{
    // Points de repère pour le trajet
    public Transform pointA;
    public Transform pointB;

    // Vitesse de déplacement de l'ennemi
    public float vitesse = 2f;

    // Cible actuelle et stockage de l'échelle d'origine pour le flip
    private Transform cible;
    private Vector3 tailleInitiale;

    // Initialisation
    void Start()
    {
        // Définit la première destination
        cible = pointB;

        // Sauvegarde la taille initiale pour pouvoir inverser le sprite sans le déformer
        tailleInitiale = transform.localScale;
    }

    // Mise à jour à chaque frame
    void Update()
    {
        // Déplace l'ennemi vers la cible actuelle
        transform.position = Vector2.MoveTowards(
            transform.position,
            cible.position,
            vitesse * Time.deltaTime
        );

        // Vérifie si l'ennemi est arrivé au point (marge de 0.05f)
        if (Vector2.Distance(transform.position, cible.position) < 0.05f)
        {
            // Alterne entre le point A et le point B
            cible = (cible == pointA) ? pointB : pointA;
        }

        // --- GESTION DE L'ORIENTATION (FLIP) ---
        // Si la cible est à droite
        if (cible.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(tailleInitiale.x),
                tailleInitiale.y,
                tailleInitiale.z
            );
        }
        // Si la cible est à gauche
        else
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(tailleInitiale.x),
                tailleInitiale.y,
                tailleInitiale.z
            );
        }
    }
}