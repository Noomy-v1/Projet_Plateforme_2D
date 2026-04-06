/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class EnnemiVolant : MonoBehaviour
{
    // Points de repère pour le mouvement vertical (haut et bas)
    public Transform pointHaut;
    public Transform pointBas;

    // Vitesse de vol
    public float vitesse = 2f;

    // Destination actuelle du mouvement
    private Transform cible;

    // Initialisation
    void Start()
    {
        // Le vampire commence par aller vers le haut
        cible = pointHaut;
    }

    // Mise à jour à chaque frame
    void Update()
    {
        // Déplace l'ennemi vers sa cible actuelle
        transform.position = Vector2.MoveTowards(
            transform.position,
            cible.position,
            vitesse * Time.deltaTime
        );

        // Vérifie si la cible est atteinte (marge de 0.05f)
        if (Vector2.Distance(transform.position, cible.position) < 0.05f)
        {
            // Alterne entre le point haut et le point bas
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