/*
 NOEMIE GIL
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;

public class MouvementJoueur : MonoBehaviour
{
    // Statistiques de base
    public int vies = 3;
    public int score = 0;
    private bool estVivant = true;

    // Paramètres de mouvement
    public float vitesse = 5f;
    public float forceSaut = 8f;

    // Composants et variables de mouvement
    private Rigidbody2D rb;
    private Animator anim;
    private float mouvementX;
    private bool auSol = false;
    private int direction = 1;

    // Stockage de la taille pour le flip
    private Vector3 tailleInitiale;

    // Gestion de la fin de partie
    public GameOverManager gameOverManager;

    // Gestion des checkpoints
    public static Vector2 positionCheckpoint;
    public static bool aToucheCheckpoint = false;
    private Vector2 positionDeDepart;

    // Variables pour l'échelle
    public float vitesseGrimpe = 5f;
    private bool surEchelle = false;
    private float graviteInitiale;

    // Ressources audio
    public AudioClip sonPerteVie;
    public AudioClip sonSaut;
    private AudioSource audioSource;

    // Interface (Score)
    public TextMeshProUGUI texteScore;

    // Interface (Vies)
    public GameObject[] tableauCoeurs;
    public int viesMax = 6;

    // Initialisation
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tailleInitiale = transform.localScale;

        // Configuration du point de spawn
        positionDeDepart = transform.position;
        if (aToucheCheckpoint)
        {
            transform.position = positionCheckpoint;
        }

        graviteInitiale = rb.gravityScale;

        audioSource = GetComponent<AudioSource>();

        // Affichage initial du score
        if (texteScore != null)
        {
            texteScore.text = "Score : " + score;
        }

        MettreAJourCoeurs();
    }

    // Boucle de mise à jour
    void Update()
    {
        // Bloque le contrôle si mort
        if (estVivant == false) return;

        // Lecture des entrées clavier
        mouvementX = Input.GetAxisRaw("Horizontal");

        // Gestion de l'orientation et des animations de course
        if (mouvementX != 0)
        {
            direction = mouvementX > 0 ? 1 : -1;
            transform.localScale = new Vector3(Mathf.Abs(tailleInitiale.x) * direction, tailleInitiale.y, tailleInitiale.z);

            if (!anim.GetBool("isJump"))
            {
                anim.SetBool("isRun", true);
            }
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        // Logique de saut
        if (Input.GetButtonDown("Jump") && auSol)
        {
            auSol = false;
            anim.SetBool("isJump", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forceSaut);

            if (sonSaut != null)
            {
                audioSource.PlayOneShot(sonSaut);
            }
        }

        // Vérification de chute hors limite
        if (transform.position.y < -45f)
        {
            PerdreVie();
        }

        // Gestion de la montée à l'échelle
        if (surEchelle)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            rb.gravityScale = 0;

            rb.linearVelocity = new Vector2(horizontal * (vitesse / 2), vertical * vitesseGrimpe);
        }
        else
        {
            rb.gravityScale = graviteInitiale;
        }
    }

    // Détection continue du sol
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sol") ||
            collision.gameObject.CompareTag("Plateforme") ||
            collision.gameObject.CompareTag("Pont"))
        {
            auSol = true;
        }
    }

    // Physique (mouvement horizontal)
    void FixedUpdate()
    {
        if (estVivant == false || surEchelle) return;
        rb.linearVelocity = new Vector2(mouvementX * vitesse, rb.linearVelocity.y);
    }

    // Entrée en collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            auSol = true;
            anim.SetBool("isJump", false);
        }
        else if (collision.gameObject.CompareTag("Pic"))
        {
            PerdreVie();
        }
    }

    // Entrée dans un trigger (Ennemi ou Échelle)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            PerdreVie();
        }

        if (collision.CompareTag("Echelle"))
        {
            surEchelle = true;
        }
    }

    // Sortie d'un trigger (Échelle)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Echelle"))
        {
            surEchelle = false;
        }
    }

    // Sortie de collision (Sol)
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            auSol = false;
        }
    }

    // Système de retrait de PV et respawn
    public void PerdreVie()
    {
        if (!estVivant) return;

        vies -= 1;
        MettreAJourCoeurs();

        if (sonPerteVie != null)
        {
            audioSource.PlayOneShot(sonPerteVie);
        }

        if (vies > 0)
        {
            rb.linearVelocity = Vector2.zero;

            if (aToucheCheckpoint)
            {
                transform.position = positionCheckpoint;
            }
            else
            {
                transform.position = positionDeDepart;
            }
        }
        else
        {
            Mourir();
        }

        // Récupère et réinitialise les plateformes du pont
        PlateformeTombante[] toutesLesPlateformes = Object.FindObjectsByType<PlateformeTombante>(FindObjectsSortMode.None);

        foreach (PlateformeTombante p in toutesLesPlateformes)
        {
            p.Reinitialiser();
        }
    }

    // Procédure de mort définitive
    private void Mourir()
    {
        estVivant = false;
        anim.SetTrigger("die");
        rb.linearVelocity = Vector2.zero;

        aToucheCheckpoint = false;

        gameOverManager.ShowGameOver();
    }

    // Recharge la scène
    private void RechargerNiveau()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Ajoute des points au score
    public void AjouterScore(int points)
    {
        score += points;
        if (texteScore != null)
        {
            texteScore.text = "Score : " + score;
        }
    }

    // Ajoute des PV
    public void AjouterVie(int montant)
    {
        vies += montant;

        if (vies > viesMax)
        {
            vies = viesMax;
        }

        MettreAJourCoeurs();
    }

    // Met à jour l'UI des coeurs
    public void MettreAJourCoeurs()
    {
        if (tableauCoeurs == null || tableauCoeurs.Length == 0) return;

        for (int i = 0; i < tableauCoeurs.Length; i++)
        {
            if (i < vies)
            {
                tableauCoeurs[i].SetActive(true);
            }
            else
            {
                tableauCoeurs[i].SetActive(false);
            }
        }
    }
}