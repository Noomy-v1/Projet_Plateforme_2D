/*
 ARPAN NATH
 */
using UnityEngine;

public class MouvementMaskDude : MonoBehaviour
{
    // Distance de patrouille
    public float distance = 3f;

    // Vitesse de déplacement
    public float speed = 2f;

    // Points de repère pour la patrouille
    private Vector3 pointA;
    private Vector3 pointB;

    // Destination actuelle
    private Vector3 cible;

    // Initialisation
    void Start()
    {
        // Définit le point de départ
        pointA = transform.position;

        // Calcule le point d'arrivée (vers la droite)
        pointB = transform.position + Vector3.right * distance;

        // Fixe la première destination
        cible = pointB;
    }

    // Mise à jour à chaque frame
    void Update()
    {
        // Déplace progressivement l'objet vers la cible
        transform.position = Vector2.MoveTowards(transform.position, cible, speed * Time.deltaTime);

        // Oriente le visuel vers la droite
        if (cible.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // Oriente le visuel vers la gauche
        else if (cible.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Vérifie si la cible est atteinte (marge d'erreur de 0.1f)
        if (Vector2.Distance(transform.position, cible) < 0.1f)
        {
            // Alterne la destination entre le point A et le point B
            if (cible == pointA)
            {
                cible = pointB;
            }
            else
            {
                cible = pointA;
            }
        }
    }
}