using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Image flashImage; // Référence à l'image du flash
    [SerializeField] private float flashDuration = 1f; // Durée du flash en secondes
    [SerializeField] private float fadeDuration = 0.5f; // Temps de fade-in et fade-out

    private void Start()
    {
        // Assurez-vous que l'image est initialement invisible
        if (flashImage != null)
        {
            flashImage.color = new Color(1, 1, 1, 0); // Transparence totale
        }
    }

    public void TriggerFlash()
    {
        // Commencez une coroutine pour afficher le flash
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        if (flashImage != null)
        {
            // Fade-in (passer de transparent à opaque)
            float timeElapsed = 0f;
            while (timeElapsed < fadeDuration)
            {
                flashImage.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timeElapsed / fadeDuration));
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            flashImage.color = new Color(1, 1, 1, 1); // Assurez-vous que l'alpha est à 1 à la fin

            // Attendez un moment (durée du flash)
            yield return new WaitForSeconds(flashDuration);

            // Fade-out (passer de opaque à transparent)
            timeElapsed = 0f;
            while (timeElapsed < fadeDuration)
            {
                flashImage.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, timeElapsed / fadeDuration));
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            flashImage.color = new Color(1, 1, 1, 0); // L'image est complètement transparente
        }
    }
}