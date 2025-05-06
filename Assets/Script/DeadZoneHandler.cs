using UnityEngine;

public class DeadZoneHandler : MonoBehaviour
{
    public Transform spawnPoint; // Point de départ du personnage

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le personnage touche une DeadZone
        if (other.CompareTag("DeadZone"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Replace le personnage au point de départ
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation; // Facultatif, remet aussi l'orientation d'origine
        Debug.Log("Le personnage est revenu au point de départ !");
    }
}