using UnityEngine;
using System.Collections.Generic;

public class UniversalDistanceCulling : MonoBehaviour
{
    public Transform playerTransform; // Le joueur
    public float renderDistance = 10f; // Distance de rendu en unités
    public float updateInterval = 0.5f; // Fréquence des mises à jour (en secondes)

    private float lastUpdateTime = 0f;
    private List<GameObject> allObjects = new List<GameObject>();

    void Start()
    {
        if (playerTransform == null)
        {
            // Si aucune référence au joueur n'est assignée, essaie d'utiliser l'objet taggué "Player"
            playerTransform = GameObject.FindWithTag("Player")?.transform;

            if (playerTransform == null)
            {
                Debug.LogError("Aucun joueur trouvé ! Assignez une référence au joueur dans l'inspecteur.");
                enabled = false; // Désactive le script pour éviter les erreurs.
            }
        }

        // Récupère tous les objets pertinents au démarrage
        allObjects.AddRange(FindObjectsOfType<GameObject>());
    }

    void Update()
    {
        if (Time.time - lastUpdateTime > updateInterval)
        {
            lastUpdateTime = Time.time;
            CullObjects();
        }
    }

    void CullObjects()
    {
        foreach (GameObject obj in allObjects)
        {
            if (obj == null || obj.transform == null) continue;

            // Calcule la distance entre le joueur et l'objet
            float distance = Vector3.Distance(playerTransform.position, obj.transform.position);

            // Affiche la distance pour chaque objet dans la console
            Debug.Log($"Objet : {obj.name} | Distance par rapport au joueur : {distance} | Activé : {obj.activeSelf}");

            // Désactive l'objet si il est trop loin du joueur
            bool shouldActivate = distance <= renderDistance;
            if (obj.activeSelf != shouldActivate)
            {
                obj.SetActive(shouldActivate);
                Debug.Log($"{(shouldActivate ? "Activation" : "Désactivation")} de {obj.name} à une distance de {distance}");
            }
        }
    }
}
