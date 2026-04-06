/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class MouvementPlateformeVerticale : MonoBehaviour
{
    // Points de repère pour le trajet haut et bas
    public Transform pointHaut;
    public Transform pointBas;

    // Vitesse de déplacement
    public float vitesse = 2f;

    // Destination actuelle
    private Transform cible;

    // Initialisation
    void Start()
    {
        // La plateforme commence par monter
        cible = pointHaut;
    }

    // Mise à jour à chaque frame
    void Update()
    {
        // Déplacement fluide vers la cible (haut ou bas)
        transform.position = Vector2.MoveTowards(
            transform.position,
            cible.position,
            vitesse * Time.deltaTime
        );

        // Quand on arrive au point (marge de 0.05f), on change de direction
        if (Vector2.Distance(transform.position, cible.position) < 0.05f)
        {
            // Alterne entre les deux points de destination
            if (cible == pointHaut)
            {
                cible = pointBas;
            }
            else
            {
                cible = pointHaut;
            }
        }
    }
}