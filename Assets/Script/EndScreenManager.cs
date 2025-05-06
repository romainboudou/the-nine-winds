using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour charger des scènes

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeReturn = 5f;

    private void Start()
    {
        // Lance la coroutine pour retourner au menu principal après un délai
        StartCoroutine(ReturnToMenuAfterDelay());
    }

    private IEnumerator ReturnToMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeReturn); // Attend le délai spécifié
        SceneManager.LoadScene("Menu"); // Charge la scène du menu principal
        Debug.Log("Retour au menu principal.");
    }
}