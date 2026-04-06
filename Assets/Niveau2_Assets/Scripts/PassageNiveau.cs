/*
 NOEMIE GIL
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassageNiveau : MonoBehaviour
{
    // Nom de la scène à charger
    public string nomDeLaSceneSuivante;

    // État de présence du joueur devant la porte
    private bool joueurDevantPorte = false;

    // Mise à jour à chaque frame
    void Update()
    {
        // Vérifie si le joueur est présent et s'il appuie vers le haut
        if (joueurDevantPorte && Input.GetAxisRaw("Vertical") > 0)
        {
            PasserAuNiveauSuivant();
        }
    }

    // Gère le changement de scène
    void PasserAuNiveauSuivant()
    {
        SceneManager.LoadScene(nomDeLaSceneSuivante);
    }

    // Déclenché quand le joueur entre dans la zone de la porte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            joueurDevantPorte = true;
        }
    }

    // Déclenché quand le joueur sort de la zone de la porte
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            joueurDevantPorte = false;
        }
    }
}