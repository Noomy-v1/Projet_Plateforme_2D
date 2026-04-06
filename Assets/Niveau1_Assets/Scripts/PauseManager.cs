/*
 ARPAN NATH
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Panneau UI de pause
    public GameObject pausePanel;

    // Nom de la scène du menu principal
    public string menuSceneName = "Menu";

    // État actuel du jeu (en pause ou non)
    private bool isPaused = false;

    // Mise à jour à chaque frame
    void Update()
    {
        // Détecte l'appui sur la touche Échap (Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alterne entre la pause et la reprise
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Met le jeu en pause
    public void PauseGame()
    {
        // Affiche le panneau de pause
        pausePanel.SetActive(true);

        // Arrête l'écoulement du temps
        Time.timeScale = 0f;

        // Met à jour l'état
        isPaused = true;
    }

    // Reprend la partie
    public void ResumeGame()
    {
        // Masque le panneau de pause
        pausePanel.SetActive(false);

        // Rétablit l'écoulement du temps
        Time.timeScale = 1f;

        // Met à jour l'état
        isPaused = false;
    }

    // Retourne au menu principal
    public void GoToMenu()
    {
        // Rétablit le temps avant de changer de scène (évite les bugs)
        Time.timeScale = 1f;

        // Charge la scène du menu
        SceneManager.LoadScene(menuSceneName);
    }
}