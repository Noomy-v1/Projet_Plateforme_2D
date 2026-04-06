/*
 ARPAN NATH
 */
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Instance globale unique (Singleton) pour un accès facile
    public static ScoreManager instance;

    // Score actuel du joueur
    public int score = 0;

    // Référence au composant texte de l'interface utilisateur
    public TextMeshProUGUI scoreText;

    // Appelé avant la méthode Start()
    private void Awake()
    {
        // Assigne cette instance pour permettre l'accès global
        instance = this;
    }

    // Initialisation
    private void Start()
    {
        // Affiche le score initial (0) au démarrage
        UpdateScoreText();
    }

    // Ajoute des points au score total
    public void AddScore(int value)
    {
        // Incrémente la valeur
        score += value;

        // Met à jour l'affichage
        UpdateScoreText();
    }

    // Actualise le texte visible dans l'UI
    void UpdateScoreText()
    {
        // Formate et affiche le score (ex: "x10")
        scoreText.text = "x" + score;
    }
}