using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Si vous utilisez une barre de progression

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar; // Barre de progression (optionnel)
    public TextMeshProUGUI progressText;  // Texte pour afficher le pourcentage (optionnel)

    void Start()
    {
        LoadScene("Game");
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
                progressBar.value = progress; 
            if (progressText != null)
                progressText.text = (progress * 100).ToString("F0") + "%";

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
