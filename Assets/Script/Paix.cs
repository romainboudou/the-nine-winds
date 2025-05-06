using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Nécessaire pour manipuler les éléments UI

public class Paix : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController playerController; // Référence au PlayerController
    [SerializeField] private FlashEffect flashEffect; // Effet de flash
    [SerializeField] private GameObject objectToDestroy; // Objet à détruire
    [SerializeField] private Image menuImage; // Image du menu à modifier
    [SerializeField] private Sprite newSprite; // Nouveau sprite pour l'image
    [SerializeField] private Animator animator;
    [SerializeField] private int dialogueIndex;
    
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Interact()
    {
        Debug.Log("Paix ramassé !");
        animator.SetTrigger("Interact");
        
        // Débloquer une capacité (Dash dans ce cas)
        if (playerController != null)
        {
            playerController.UnlockDash();
        }
        
        // Détruire un objet dans la scène
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            Debug.Log("Objet détruit : " + objectToDestroy.name);
        }
        else
        {
            Debug.LogWarning("Aucun objet à détruire n'est assigné !");
        }
        
        // Activer l'effet de flash
        if (flashEffect != null)
        {
            Debug.Log("Flash !");
            flashEffect.TriggerFlash();
        }
        
        // Modifier l'image du menu
        if (menuImage != null && newSprite != null)
        {
            menuImage.sprite = newSprite; // Change le sprite
            Debug.Log("Image du menu modifiée !");
        }
        else
        {
            Debug.LogWarning("MenuImage ou NewSprite n'est pas assigné !");
        }
        
        if (audioSource != null)
        {
            audioSource.Play(); // Joue l'audio
            Debug.Log("Audio joué !");
        }
        else
        {
            Debug.LogWarning("AudioSource n'est pas assigné !");
        }
        DialogueManager.Instance.ActivateDialogue(dialogueIndex);

        // Détruire l'objet après interaction
        Destroy(gameObject, 4f);
    }
}