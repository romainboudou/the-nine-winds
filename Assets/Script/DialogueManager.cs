using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject[] dialogues; // Tableau des dialogues

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Il y a déjà une instance de DialogueManager dans la scène !");
            Destroy(gameObject);
        }
    }

    public void ActivateDialogue(int index)
    {
        if (index < 0 || index >= dialogues.Length)
        {
            Debug.LogWarning("Index de dialogue invalide !");
            return;
        }

        // Désactiver tous les dialogues
        foreach (var dialogue in dialogues)
        {
            if (dialogue != null)
                dialogue.SetActive(false);
        }

        // Activer le dialogue sélectionné
        if (dialogues[index] != null)
        {
            dialogues[index].SetActive(true);
            Debug.Log($"Dialogue {index} activé.");

            // Désactiver automatiquement après 4 secondes
            StartCoroutine(DeactivateAfterTime(dialogues[index], 4f));
        }
        else
        {
            Debug.LogWarning($"Le dialogue à l'index {index} est null.");
        }
    }

    private System.Collections.IEnumerator DeactivateAfterTime(GameObject dialogue, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (dialogue != null)
        {
            dialogue.SetActive(false);
            Debug.Log("Dialogue désactivé automatiquement.");
        }
    }
}