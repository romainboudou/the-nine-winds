using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceFinish : MonoBehaviour
{
    [SerializeField] private int dialogueIndex;
    private AudioSource audioSource;
    private PlayerController playerController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            StartCoroutine(DisableMovementTemporarily());
        }
        
        if (audioSource != null)
        {
            audioSource.Play(); // Joue l'audio
            Debug.Log("Audio joué !");
            StartCoroutine(DestroyAfterDelay(3f)); // Démarre une coroutine pour attendre avant de détruire
        }
        else
        {
            Debug.LogWarning("AudioSource n'est pas assigné !");
        }
        
        DialogueManager.Instance.ActivateDialogue(dialogueIndex);
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attente
        Destroy(gameObject); // Détruit le GameObject après le délai
        Debug.Log("GameObject détruit après délai.");
    }
    
    private IEnumerator DisableMovementTemporarily()
    {
        if (playerController != null)
        {
            playerController.SetInteracting(true); 
            yield return new WaitForSeconds(3f); 
            playerController.SetInteracting(false);
        }
    }
}
