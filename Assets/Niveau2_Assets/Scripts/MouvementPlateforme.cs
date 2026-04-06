/*
 NOEMIE GIL
 */
using UnityEngine;

public class MouvementPlateforme : MonoBehaviour
{
    // Points de repère pour le mouvement vertical
    public Transform pointHaut;
    public Transform pointBas;

    // Vitesse de déplacement
    public float vitesse = 2f;

    // Destination actuelle
    private Transform cibleActuelle;

    // Initialisation au premier rafraîchissement
    void Start()
    {
        // Définit le point bas comme première cible
        cibleActuelle = pointBas;
    }

    // Mise à jour à chaque image
    void Update()
    {
        // Déplace la plateforme vers la cible
        transform.position = Vector2.MoveTowards(transform.position, cibleActuelle.position, vitesse * Time.deltaTime);

        // Vérifie si la cible est atteinte (marge de 0.1f)
        if (Vector2.Distance(transform.position, cibleActuelle.position) < 0.1f)
        {
            // Alterne entre le point bas et le point haut
            if (cibleActuelle == pointBas)
            {
                cibleActuelle = pointHaut;
            }
            else
            {
                cibleActuelle = pointBas;
            }
        }
    }
}