using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject objectActivate;  // L'objet à activer/désactiver
    [SerializeField] private FlashEffect flashEffect;    // Effet de flash
    [SerializeField] private Animator animator;          // L'Animator du levier
    [SerializeField] private AudioClip leverSound;       // Son à jouer quand le levier est activé/désactivé
    private AudioSource audioSource;                     // AudioSource pour jouer le son

    private bool activated;                              // Statut du levier
    private Animator hisAnimator;                        // Animator de l'objet à activer/désactiver
    
    private void Awake()
    {
        hisAnimator = GetComponent<Animator>();          // Récupère l'Animator du levier
        audioSource = GetComponent<AudioSource>();       // Récupère l'AudioSource pour jouer les sons
    }

    public void Interact()
    {
        if (activated == false)
        {
            animator.SetTrigger("Touch");                // Déclenche l'animation du levier
            hisAnimator.SetBool("Activated", true);     // Active l'animation de l'objet
            objectActivate.SetActive(true);              // Active l'objet lié
            activated = true;
            Debug.Log("Levier Activated");

            // Jouer le son lorsque le levier est activé
            PlayLeverSound();
        }
        else
        {
            hisAnimator.SetBool("Activated", false);    // Désactive l'animation de l'objet
            objectActivate.SetActive(false);             // Désactive l'objet lié
            activated = false;
            Debug.Log("Levier Desactivated");

            // Jouer le son lorsque le levier est désactivé
            PlayLeverSound();
        }
        
        if (flashEffect != null)
        {
            Debug.Log("Flash !");
            flashEffect.TriggerFlash();                   // Active l'effet de flash si défini
        }
    }

    // Méthode pour jouer le son du levier
    private void PlayLeverSound()
    {
        if (leverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(leverSound);         // Joue le son
        }
    }
}
