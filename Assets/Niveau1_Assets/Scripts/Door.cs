/*
 ARPAN NATH
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Nom de la scène suivante à charger
    public string nextSceneName;

    // Nombre de pièces requises
    public int requiredCoins = 10;

    // Déclenché lors d'une collision 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet est le joueur
        if (collision.CompareTag("Player"))
        {
            // Vérifie si le gestionnaire existe et si le score est suffisant
            if (ScoreManager.instance != null && ScoreManager.instance.score >= requiredCoins)
            {
                // Charge la nouvelle scène
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                // Affiche un message d'avertissement dans la console
                Debug.Log("Tu dois ramasser 10 coins avant d'ouvrir la porte.");
            }
        }
    }
}