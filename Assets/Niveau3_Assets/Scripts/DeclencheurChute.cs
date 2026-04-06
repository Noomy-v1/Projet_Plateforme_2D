/*
        RIDI OTOKO
 
 */
using UnityEngine;

public class DeclencheurChute : MonoBehaviour
{
    // L'objet physique qui doit tomber (ex: une plateforme ou une pointe)
    public Rigidbody2D objetQuiTombe;

    // Temps d'attente avant que l'objet ne tombe
    public float delaiAvantChute = 0f;

    // Permet de définir si le piège ne peut servir qu'une seule fois
    public bool declencheUneSeuleFois = true;

    // État interne pour savoir si le piège a déjà été activé
    private bool dejaDeclenche = false;

    // Déclenché lorsqu'un objet entre dans la zone de détection
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger touche par : " + other.name + " | tag = " + other.tag);

        // Sort si le piège est à usage unique et qu'il a déjà servi
        if (dejaDeclenche && declencheUneSeuleFois) return;

        // Vérifie si c'est bien le joueur qui a touché le déclencheur
        if (!other.CompareTag("Player"))
        {
            Debug.Log("Ce n'est pas le joueur.");
            return;
        }

        // Vérifie qu'un objet a bien été assigné dans l'inspecteur
        if (objetQuiTombe == null)
        {
            Debug.Log("Aucun objet a faire tomber assigne.");
            return;
        }

        dejaDeclenche = true;
        Debug.Log("Le piege est declenche.");

        // Lance la chute immédiatement ou après un délai
        if (delaiAvantChute <= 0f)
        {
            FaireTomber();
        }
        else
        {
            // Appelle la méthode après le délai spécifié
            Invoke(nameof(FaireTomber), delaiAvantChute);
        }
    }

    // Méthode qui active la physique pour faire tomber l'objet
    void FaireTomber()
    {
        Debug.Log("Je fais tomber : " + objetQuiTombe.name);

        // Change le type du Rigidbody en Dynamic pour qu'il subisse la gravité
        objetQuiTombe.bodyType = RigidbodyType2D.Dynamic;
    }
}