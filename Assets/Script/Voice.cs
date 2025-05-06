using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour
{
    [SerializeField] private int dialogueIndex;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log(audioSource);
        if (audioSource != null)
        {
            audioSource.Play(); // Joue l'audio
            Debug.Log("Audio joué !");
            StartCoroutine(DestroyAfterDelay(3f)); 
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
}