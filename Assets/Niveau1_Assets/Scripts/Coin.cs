/*
    ARPAN NATH
 */

using UnityEngine;

public class Coin : MonoBehaviour
{
    // Valeur de la pièce
    public int value = 1;

    // Son joué à la collecte
    [SerializeField] private AudioClip coinSound;

    // Déclenché lors d'une collision 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet en collision est le joueur
        if (collision.CompareTag("Player"))
        {
            // Ajoute la valeur au score global
            ScoreManager.instance.AddScore(value);

            // Joue le son à la position de la pièce
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

            // Détruit la pièce
            Destroy(gameObject);
        }
    }
}