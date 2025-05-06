using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject objectActivate;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private Animator animator;
    public void Interact()
    {
        animator.SetTrigger("Interact");
        if (playerInventory != null)
        {
            playerInventory.CollectKey();
            
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
        
        Destroy(gameObject, 4f);
    }
}
