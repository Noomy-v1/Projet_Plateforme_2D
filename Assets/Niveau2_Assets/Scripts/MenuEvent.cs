/*
 NOEMIE GIL
 */
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class MenuEvent : MonoBehaviour
{
    // Références pour l'animation et le mouvement du personnage
    public Animator animJoueur;
    public Transform transformJoueur;

    // Paramètres de la transition
    public float vitesseCourse = 6f;
    public float tempsAvantChargement = 1.5f;

    // Sécurité pour éviter les clics multiples
    private bool enTransition = false;

    // Point d'entrée pour lancer un niveau
    public void LoadLevel(int index)
    {
        if (!enTransition)
        {
            // Démarre la séquence visuelle avant le chargement
            StartCoroutine(SequenceLancement(index));
        }
    }

    // Coroutine gérant la course du personnage puis le changement de scène
    private IEnumerator SequenceLancement(int index)
    {
        enTransition = true;

        // Active l'animation de course
        if (animJoueur != null)
        {
            animJoueur.SetBool("isRun", true);
        }

        // Boucle de déplacement temporel
        float chrono = 0f;
        while (chrono < tempsAvantChargement)
        {
            if (transformJoueur != null)
            {
                // Déplace le personnage vers la droite
                transformJoueur.Translate(Vector3.right * vitesseCourse * Time.deltaTime);
            }

            chrono += Time.deltaTime;
            // Attend la prochaine image (frame)
            yield return null;
        }

        // Charge la scène demandée une fois le délai passé
        SceneManager.LoadScene(index);
    }

    // Ferme l'application
    public void QuitterJeu()
    {
        Application.Quit();

        // Message de confirmation pour l'éditeur Unity
        Debug.Log("Le jeu vient de se fermer !");
    }
}