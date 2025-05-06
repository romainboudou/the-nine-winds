using UnityEngine;

public class Sagesse : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject objectToDestroy;
    [SerializeField] private GameObject objectActivate;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private UnityEngine.UI.Image menuImage;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private int dialogueIndex; 

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        Debug.Log("Sagesse ramassée !");
        animator.SetTrigger("Interact");

        if (playerController != null)
        {
            playerController.UnlockJump();
        }

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            Debug.Log("Objet détruit : " + objectToDestroy.name);
        }
        else
        {
            Debug.LogWarning("Aucun objet à détruire n'est assigné !");
        }

        if (objectActivate != null)
        {
            objectActivate.SetActive(true);
        }

        if (flashEffect != null)
        {
            Debug.Log("Flash !");
            flashEffect.TriggerFlash();
        }

        if (menuImage != null && newSprite != null)
        {
            menuImage.sprite = newSprite;
            Debug.Log("Image du menu modifiée !");
        }
        else
        {
            Debug.LogWarning("MenuImage ou NewSprite n'est pas assigné !");
        }

        if (audioSource != null)
        {
            audioSource.Play(); 
            Debug.Log("Audio joué !");
        }
        else
        {
            Debug.LogWarning("AudioSource n'est pas assigné !");
        }

        // Appeler le DialogueManager pour activer un dialogue
        DialogueManager.Instance.ActivateDialogue(dialogueIndex);

        Destroy(gameObject, 4f);
    }
}
