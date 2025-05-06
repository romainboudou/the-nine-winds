using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public float normalGravity = 9.8f; // Gravité normale
    public float fallingGravityMultiplier = 2f; // Multiplicateur de gravité pendant la chute

    private Rigidbody rb;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.useGravity = false; // On gère la gravité manuellement
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < -1) // Vérifie si le personnage tombe
        {
            // Applique une gravité plus forte quand il tombe
            rb.AddForce(Vector3.down * normalGravity * fallingGravityMultiplier, ForceMode.Acceleration);
            animator.SetBool("Falling", true);
        }
        else
        {
            // Applique la gravité normale (si nécessaire)
            rb.AddForce(Vector3.down * normalGravity, ForceMode.Acceleration);
            animator.SetBool("Falling", false);
        }
    }
}