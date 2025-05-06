using UnityEngine;

public class InputShowManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image buttonPromptImage; // Image du bouton à afficher
    [SerializeField] private Sprite xboxButtonSprite;  // Sprite pour le bouton Xbox
    [SerializeField] private Sprite ps4ButtonSprite;   // Sprite pour le bouton PS4
    [SerializeField] private Sprite keyboardButtonSprite;  // Décalage de la position du bouton par rapport à l'objet

    private RectTransform buttonRectTransform;
    private Transform objectToFollow;

    private void Awake()
    {
        buttonRectTransform = buttonPromptImage.GetComponent<RectTransform>();
    }

    // Assigner l'objet à suivre et afficher le bouton
    public void SetUpButton(Transform objectToFollow)
    {
        this.objectToFollow = objectToFollow;
        buttonPromptImage.gameObject.SetActive(true);
    }

    // Afficher l'image du bouton en fonction de l'appareil
    public void ShowButtonPrompt()
    {
        if (objectToFollow == null) return;

        // Vérifier le périphérique utilisé et afficher l'image appropriée
        if (IsUsingXboxController())
        {
            buttonPromptImage.sprite = xboxButtonSprite;
        }
        else if (IsUsingPS4Controller())
        {
            buttonPromptImage.sprite = ps4ButtonSprite;
        }
        else
        {
            buttonPromptImage.sprite = keyboardButtonSprite;  // Afficher pour le clavier
        }
    }

    // Cacher l'image du bouton
    public void HideButtonPrompt()
    {
        buttonPromptImage.gameObject.SetActive(false);  // Cacher l'image du bouton
    }

    private bool IsUsingXboxController()
    {
        return Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0].Contains("Xbox");
    }

    private bool IsUsingPS4Controller()
    {
        return Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0].Contains("PS4");
    }
}
