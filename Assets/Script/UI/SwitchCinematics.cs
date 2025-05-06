using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchCinematics : MonoBehaviour
{
    [SerializeField] private InputActionReference skipActionRef;

    public GameObject[] cinematics;
    public float switchInterval = 11.5f;

    private int currentCinematicsIndex;

    void Start()
    {
        skipActionRef.action.performed += OnSkipPerformed;
        Debug.Log("SKIP");
        for (int i = 0; i < cinematics.Length; i++)
        {
            cinematics[i].SetActive(i == 0);
            Debug.Log(i);
        }
        StartCoroutine(SwitchCanvasRoutine());
    }

    private void OnEnable()
    {
        skipActionRef.action.Enable();
    }

    private void OnDisable()
    {
        skipActionRef.action.Disable();
    }

    private void OnSkipPerformed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("LoadingScene");
        OnDisable();
    }

    private IEnumerator SwitchCanvasRoutine()
    {
        while (currentCinematicsIndex < cinematics.Length)
        {
            yield return new WaitForSeconds(switchInterval); 

            cinematics[currentCinematicsIndex].SetActive(false);

            currentCinematicsIndex++;

            if (currentCinematicsIndex < cinematics.Length)
            {
                cinematics[currentCinematicsIndex].SetActive(true);
            }

            if (currentCinematicsIndex == 2)
            {
                switchInterval = 14.5f;
            }
        }

        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
