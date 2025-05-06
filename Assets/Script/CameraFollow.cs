using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; // Référence vers le joueur
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -15); // Décalage de la caméra par rapport au joueur
    [SerializeField] private float smoothSpeed = 0.125f; // Vitesse de lissage

    private void LateUpdate()
    {
        if (player == null) return;

        // Calculer la position de la caméra
        Vector3 desiredPosition = player.position + offset;

        // Lissage de la position de la caméra pour une transition fluide
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}