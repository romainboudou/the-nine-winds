using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneChanger : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject firstButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
    
    public void ChangeScene()
    {
        SceneManager.LoadScene("Cinematics");
    }

    public void Quit()
    {
        Application.Quit();
    }
}