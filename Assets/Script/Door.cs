using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private GameObject objectToDestroy;  // L'objet à détruire
    [SerializeField] private Animator animator;
    [SerializeField] private int dialogueIndex;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip destructionSound;  // Son de destruction
    private AudioSource audioSource;
    private PlayerController playerController;  // Référence au PlayerController du personnage

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  // Récupère l'AudioSource attaché à l'objet
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre en collision
        playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            StartCoroutine(DisableMovementTemporarily());
        }

        // Joue l'audio si disponible
        if (audioSource != null)
        {
            audioSource.Play();
            Debug.Log("Audio joué !");
        }
        else
        {
            Debug.LogWarning("AudioSource n'est pas assigné !");
        }
        DialogueManager.Instance.ActivateDialogue(dialogueIndex);
    }

    public void Interact()
    {
        if (playerInventory != null && playerInventory.HasKey())  // Vérifie si le joueur a la clé
        {
            Debug.Log("La porte s'ouvre");
            animator.SetTrigger("Touch");
            
            if (objectToDestroy != null)
            {
                // Joue le son de destruction avant de détruire l'objet
                PlayDestructionSound();

                Destroy(objectToDestroy);  // Détruit l'objet assigné dans la scène
                Debug.Log("Objet détruit : " + objectToDestroy.name);
            }
            else
            {
                Debug.LogWarning("Aucun objet à détruire n'est assigné !");
            }

            if (flashEffect != null)
            {
                Debug.Log("Flash !");
                flashEffect.TriggerFlash();  // Active l'effet de flash
            }

            Destroy(gameObject, 2f);  // Détruit la porte après 2 secondes
        }
        else
        {
            Debug.Log("Il vous faut une clé pour ouvrir cette porte.");
        }
    }

    // Méthode pour jouer le son de destruction
    private void PlayDestructionSound()
    {
        if (destructionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destructionSound);  // Joue le son de destruction
            Debug.Log("Son de destruction joué !");
        }
        else
        {
            Debug.LogWarning("Le son de destruction ou l'AudioSource n'est pas assigné.");
        }
    }

    private IEnumerator DisableMovementTemporarily()
    {
        if (playerController != null)
        {
            playerController.SetInteracting(true);  // Empêche le joueur de bouger pendant l'interaction
            yield return new WaitForSeconds(1f);  // Attends 1 seconde
            playerController.SetInteracting(false);  // Permet à nouveau le mouvement
        }
    }
}
