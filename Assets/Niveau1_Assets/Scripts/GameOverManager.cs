/*
 ARPAN NATH
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Panneau UI de fin de jeu
    public GameObject gameOverPanel;

    // Nom de la scène du menu principal
    public string menuSceneName = "MenuPrincipal";

    // Source audio pour l'effet de fin
    [SerializeField] private AudioSource audioSource;

    // Musique de fin de jeu
    [SerializeField] private AudioClip gameOverMusic;

    // Musique de fond du niveau
    [SerializeField] private AudioSource backgroundMusic;

    // Affiche l'écran de fin de jeu
    public void ShowGameOver()
    {
        // Active l'interface de fin
        gameOverPanel.SetActive(true);

        // Arrête la musique de fond
        backgroundMusic.Stop();

        // Joue la musique de défaite une seule fois
        audioSource.PlayOneShot(gameOverMusic);

        // Met le jeu en pause
        Time.timeScale = 0f;
    }

    // Relance le niveau actuel
    public void Retry()
    {
        // Rétablit l'écoulement du temps
        Time.timeScale = 1f;

        // Recharge la scène active
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Retourne au menu principal
    public void GoToMenu()
    {
        // Rétablit l'écoulement du temps
        Time.timeScale = 1f;

        // Charge la scène du menu
        SceneManager.LoadScene(menuSceneName);
    }
}