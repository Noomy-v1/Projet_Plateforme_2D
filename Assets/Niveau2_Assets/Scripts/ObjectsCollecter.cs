/*
 NOEMIE GIL
 */
using UnityEngine;

public class ObjetsCollecter : MonoBehaviour
{
    [Header("Configuration de l'objet")]
    [Tooltip("Cochez cette case si c'est une pièce. Décochez-la si c'est un champignon.")]
    public bool estUnePiece = true;

    [Header("Valeurs")]
    // Nombre de points accordés (si pièce)
    public int pointsDonnes = 10;
    // Nombre de vies accordées (si champignon)
    public int viesDonnees = 1;

    [Header("Audio")]
    // Son déclenché lors de la collecte
    public AudioClip sonCollecte;

    // Déclenché lorsqu'un objet entre dans la zone 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet est le joueur
        if (collision.CompareTag("Player"))
        {
            // 1. JOUER LE SON
            if (sonCollecte != null)
            {
                // Joue le son à la position actuelle de l'objet
                AudioSource.PlayClipAtPoint(sonCollecte, transform.position);
            }

            // 2. DONNER LA RÉCOMPENSE
            if (estUnePiece == true)
            {
                // Appelle la fonction de score du joueur
                collision.GetComponent<MouvementJoueur>().AjouterScore(pointsDonnes);
            }
            else
            {
                // Appelle la fonction de soin du joueur
                collision.GetComponent<MouvementJoueur>().AjouterVie(viesDonnees);
            }

            // FAIRE DISPARAÎTRE L'OBJET
            Destroy(gameObject);
        }
    }
}