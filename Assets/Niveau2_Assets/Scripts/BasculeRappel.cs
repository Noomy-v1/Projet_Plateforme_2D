/*
 NOEMIE GIL
 */
using UnityEngine;

public class BasculeRappel : MonoBehaviour
{
    // Puissance pour revenir à l'horizontale
    public float forceDeRappel = 5f;

    // Composant physique de la bascule
    private Rigidbody2D rb;

    // Initialisation
    void Start()
    {
        // Récupère le composant Rigidbody2D attaché
        rb = GetComponent<Rigidbody2D>();
    }

    // Mise à jour de la physique (à intervalles fixes)
    void FixedUpdate()
    {
        // Vérifie si la bascule est inclinée (rotation sur l'axe Z)
        if (transform.rotation.z != 0)
        {
            // Applique une force de rotation inverse pour la redresser
            rb.AddTorque(-transform.rotation.z * forceDeRappel);
        }
    }
}