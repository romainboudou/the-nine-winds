using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private InputActionReference interactActionRef;
    [SerializeField] private InputShowManager inputShowManager;

    private IInteractable currentInteractable;

    private void Start()
    {
        if (interactActionRef != null)
        {
            interactActionRef.action.performed += OnInteract;
        }
        else
        {
            Debug.LogError("Interact Action Reference is not set in the Inspector!");
        }
    }

    private void OnEnable()
    {
        interactActionRef.action.Disable();
    }

    private void OnDisable()
    {
        interactActionRef.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log($"Proche de {other.name}. Appuyez sur E pour interagir.");

            // Activer l'action d'interaction
            interactActionRef.action.Enable();

            // Montrer le bouton d'interaction
            inputShowManager.SetUpButton(other.transform); // Passer l'objet à suivre au InputShowManager
            inputShowManager.ShowButtonPrompt(); // Afficher le prompt du bouton
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            Debug.Log($"Sorti de la portée de {other.name}.");

            // Désactiver l'action d'interaction
            interactActionRef.action.Disable();

            // Cacher le bouton d'interaction
            inputShowManager.HideButtonPrompt();
        }
    }

    private IEnumerator InteractionRoutine(float interactionDuration, PlayerController playerController)
    {
        if (playerController != null)
        {
            playerController.SetInteracting(true); // Définir l'état d'interaction
        }

        yield return new WaitForSeconds(interactionDuration); // Durée d'interaction

        if (playerController != null)
        {
            playerController.SetInteracting(false); // Fin de l'interaction
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            Debug.Log("currentInteractable is " + currentInteractable);
            currentInteractable.Interact(); // Effectuer l'interaction
            inputShowManager.HideButtonPrompt();

            // Commencer la routine d'interaction avec le joueur
            PlayerController playerController = GetComponent<PlayerController>();
            StartCoroutine(InteractionRoutine(3.5f, playerController));

            // Désactiver l'action après l'interaction
            interactActionRef.action.Disable();
        }
    }
}
