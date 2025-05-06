using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LoadEndScene();
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene("End"); // Charge la scène "End"
        Debug.Log("Chargement de la scène 'End'.");
    }
}