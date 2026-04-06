/*
 ARPAN NATH
 */
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // Premier point de passage
    [SerializeField] private Transform pointA;

    // Deuxième point de passage
    [SerializeField] private Transform pointB;

    // Vitesse de déplacement
    [SerializeField] private float speed = 2f;

    // Vitesse de rotation (en degrés par seconde)
    [SerializeField] private float rotationSpeed = 300f;

    // Distance minimale pour considérer la cible comme atteinte
    [SerializeField] private float stoppingDistance = 0.05f;

    // Cible actuelle du déplacement
    private Transform currentTarget;

    // Initialisation
    private void Start()
    {
        // Définit le point B comme première destination
        currentTarget = pointB;
    }

    // Mise à jour à chaque frame
    private void Update()
    {
        // Fait tourner l'objet en continu sur l'axe Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Déplace progressivement l'objet vers sa cible
        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget.position,
            speed * Time.deltaTime
        );

        // Vérifie si la distance avec la cible est inférieure ou égale à la distance d'arrêt
        if (Vector2.Distance(transform.position, currentTarget.position) <= stoppingDistance)
        {
            // Alterne la destination entre le point A et le point B
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }
    }
}