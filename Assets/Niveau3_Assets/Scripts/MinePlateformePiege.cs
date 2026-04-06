/*
        RIDI OTOKO 
 
 */
using UnityEngine;

public class MinePlateformePiege : MonoBehaviour
{
    [Header("Réglages")]
    [Tooltip("Temps avant que la plateforme ne soit supprimée après avoir commencé à tomber")]
    public float tempsAvantDestruction = 0.5f;
    public float graviteChute = 5f;

    private Rigidbody2D rb;
    private bool estEnTrainDeTomber = false;

    // Initialisation
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Configuration initiale : statique et sans gravité
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0;
        }
        else
        {
            // Alerte si le composant indispensable est oublié
            Debug.LogError("Il manque un Rigidbody2D sur " + gameObject.name);
        }
    }

    // Détection de l'impact avec le joueur
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si c'est le joueur et si la plateforme est encore stable
        if (collision.gameObject.CompareTag("Player") && !estEnTrainDeTomber)
        {
            // Vérifie si le joueur touche le haut de la plateforme (normale de collision vers le bas)
            if (collision.contacts[0].normal.y < -0.5f)
            {
                DeclencherChute();
            }
        }
    }

    // Active la chute physique
    void DeclencherChute()
    {
        estEnTrainDeTomber = true;

        // Rend l'objet dynamique pour qu'il réagisse à la gravité
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = graviteChute;

        // Supprime l'objet de la scène après le délai défini
        Destroy(gameObject, tempsAvantDestruction);
    }

    // Sécurité lors de la désactivation ou destruction
    private void OnDisable()
    {
        // Si le joueur était "parenté" à la plateforme (pour ne pas glisser), 
        // on le libère pour éviter qu'il ne disparaisse avec elle.
        if (transform.childCount > 0)
        {
            transform.DetachChildren();
        }
    }
}