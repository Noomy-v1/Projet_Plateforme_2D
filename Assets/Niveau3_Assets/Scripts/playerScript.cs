/*
        RIDI OTOKO
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    [Header("Stats joueur")]
    // État de santé et points
    public int vies = 6;
    public int score = 0;
    private bool estVivant = true;

    [Header("Deplacement")]
    // Paramètres de mobilité
    public float vitesse = 5f;
    public float forceSaut = 3.8f;

    [Header("Detection du sol")]
    // Variables pour la vérification physique du sol
    public Transform groundCheck;
    public float rayonSol = 0.12f;
    public LayerMask coucheSol;

    [Header("Checkpoint")]
    // Gestion de la réapparition
    private Vector3 checkpointPosition;
    private bool checkpointActif = false;
    private Vector3 positionInitiale;

    [Header("Audio")]
    // Bibliothèque de sons
    public AudioClip sonMort;
    public AudioClip sonPiece;
    public AudioClip sonToucheBalle;
    public AudioClip sonSaut;

    private AudioSource audioSource;
    private int viesMax;

    // Composants et variables de contrôle
    private Rigidbody2D rb;
    private Animator anim;
    private float mouvementX;
    private bool auSol = false;
    private int direction = 1;
    private Vector3 tailleInitiale;

    // --- INTERFACE (UI) ---
    public TextMeshProUGUI texteScore;
    public GameObject[] tableauCoeurs;
    public GameOverManager gameOverManager;

    // Initialisation
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tailleInitiale = transform.localScale;

        viesMax = vies;
        positionInitiale = transform.position;
        checkpointPosition = positionInitiale;

        // Configuration de la source audio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;

        // Recherche automatique des coeurs si non assignés
        if (tableauCoeurs == null || tableauCoeurs.Length == 0)
        {
            tableauCoeurs = GameObject.FindGameObjectsWithTag("CoeurUI");
            System.Array.Sort(tableauCoeurs, (a, b) => a.name.CompareTo(b.name));
        }

        MettreAJourCoeurs();
    }

    // Mise à jour logique
    void Update()
    {
        if (!estVivant) return;

        // Détection du sol par cercle de collision
        auSol = Physics2D.OverlapCircle(groundCheck.position, rayonSol, coucheSol);
        mouvementX = Input.GetAxisRaw("Horizontal");

        // Gestion du flip du personnage
        if (mouvementX != 0)
        {
            direction = mouvementX > 0 ? 1 : -1;
            transform.localScale = new Vector3(Mathf.Abs(tailleInitiale.x) * direction, tailleInitiale.y, tailleInitiale.z);
        }

        // Mise à jour des paramètres de l'animateur
        anim.SetBool("isRun", auSol && mouvementX != 0);
        anim.SetBool("isJump", !auSol);

        // Commande de saut
        if (Input.GetButtonDown("Jump") && auSol)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forceSaut);
            JouerSon(sonSaut);
        }
    }

    // Mise à jour physique
    void FixedUpdate()
    {
        if (!estVivant) return;
        // Applique la vitesse horizontale
        rb.linearVelocity = new Vector2(mouvementX * vitesse, rb.linearVelocity.y);
    }

    // Détection de collision physique
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!estVivant) return;

        // Vérification des zones mortelles
        if (collision.gameObject.CompareTag("Pic") || collision.gameObject.CompareTag("Deadly"))
        {
            PerdreVie();
        }
    }

    // Détection de trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!estVivant) return;

        // Collecte des pièces
        if (collision.gameObject.CompareTag("Piece"))
        {
            AjouterScore(1);
            Destroy(collision.gameObject);
            return;
        }

        // Zones mortelles en trigger
        if (collision.gameObject.CompareTag("Pic") || collision.gameObject.CompareTag("Deadly"))
        {
            PerdreVie();
        }
    }

    // --- SYSTÈME DE JEU ---

    public void AjouterScore(int points)
    {
        score += points;
        JouerSon(sonPiece);
        if (texteScore != null) texteScore.text = "Score : " + score;
    }

    // Gère la perte de santé et la transition vers la mort
    public void PerdreVie()
    {
        if (!estVivant) return;

        estVivant = false;
        vies -= 1;
        MettreAJourCoeurs();
        JouerSon(sonMort);

        anim.SetTrigger("die");
        rb.linearVelocity = Vector2.zero;

        // Déclenche la suite après un délai de 2 secondes
        if (vies > 0)
        {
            Invoke("RespawnAuCheckpoint", 2f);
        }
        else
        {
            Invoke("GameOver", 2f);
        }
    }

    // Rafraîchit l'affichage des coeurs dans l'UI
    public void MettreAJourCoeurs()
    {
        for (int i = 0; i < tableauCoeurs.Length; i++)
        {
            tableauCoeurs[i].SetActive(i < vies);
        }
    }

    // Replage le joueur au dernier point de sauvegarde
    private void RespawnAuCheckpoint()
    {
        transform.position = checkpointActif ? checkpointPosition : positionInitiale;

        estVivant = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Réinitialisation de l'état d'animation
        anim.ResetTrigger("die");
        anim.Play("Idle");
        anim.SetBool("isRun", false);
        anim.SetBool("isJump", false);
    }

    // Réinitialisation totale suite à la perte de toutes les vies
    private void GameOver()
    {
        transform.position = positionInitiale;
        checkpointPosition = positionInitiale;
        checkpointActif = false;
        vies = viesMax;
        estVivant = true;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        anim.ResetTrigger("die");
        anim.Play("Idle");
        anim.SetBool("isRun", false);
        anim.SetBool("isJump", false);

        MettreAJourCoeurs();

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }

    // Enregistre un nouveau point de spawn
    public void SetCheckpoint(Vector3 nouvellePosition)
    {
        checkpointPosition = nouvellePosition;
        checkpointActif = true;
    }

    // Utilitaire pour jouer un clip audio une seule fois
    private void JouerSon(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Dessine le rayon de détection du sol dans l'éditeur
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, rayonSol);
        }
    }
}