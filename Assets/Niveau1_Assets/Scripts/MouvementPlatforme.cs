/*
 ARPAN NATH
 */
using UnityEngine;

public class MouvementPlatforme : MonoBehaviour
{
    // Premier point de passage
    public Transform pointA;

    // Deuxième point de passage
    public Transform pointB;

    // Vitesse de déplacement
    public float speed = 2f;

    // Cible actuelle de la plateforme
    private Transform target;

    // Initialisation
    private void Start()
    {
        // Définit le point B comme première destination
        target = pointB;
    }

    // Mise à jour à chaque frame
    private void Update()
    {
        // Déplace progressivement la plateforme vers la cible
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Vérifie si la plateforme est presque arrivée (marge de 0.05f)
        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            // Alterne la destination entre le point A et le point B
            target = target == pointA ? pointB : pointA;
        }
    }
}