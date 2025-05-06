using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject[] canvases; // Liste des canvases avec leur groupement
    [SerializeField] private GameObject[] firstButtons; // Bouton par défaut pour chaque canvas

    [Header("Audio Settings")]
    [SerializeField] private AudioClip menuOpenSound;
    [SerializeField] private AudioClip menuCloseSound;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference pauseActionRef;

    private AudioSource audioSource;
    private bool isPaused;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        pauseActionRef.action.performed += OnPausePerformed;
    }

    private void OnEnable()
    {
        pauseActionRef.action.Enable();
    }

    private void OnDisable()
    {
        pauseActionRef.action.Disable();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        PlaySound(menuCloseSound);
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        PlaySound(menuOpenSound);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        // Définir le bouton du premier canvas par défaut
        SwitchCanvas(0);

        isPaused = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void SwitchCanvas(int index)
    {
        if (index < 0 || index >= canvases.Length || index >= firstButtons.Length)
        {
            Debug.LogWarning("Invalid canvas or first button index");
            return;
        }

        // Désactiver tous les canvases
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].gameObject.SetActive(i == index);
        }

        // Activer le bouton par défaut du canvas
        EventSystem.current.SetSelectedGameObject(firstButtons[index]);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
