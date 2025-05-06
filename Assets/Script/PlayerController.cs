using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 10f; // Vitesse de marche
    [SerializeField] private float runSpeed = 20f;  // Vitesse de course
    [SerializeField] private float rotationSpeed = 10f; // Vitesse de rotation
    [SerializeField] private float runThreshold = 0.5f; // Inclinaison minimum pour courir
    [SerializeField] private float jumpForce = 10f; // Force de saut
    [SerializeField] private float gravityMultiplier = 2f;
    
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 50f; // Vitesse du dash
    [SerializeField] private float dashDuration = 0.2f; // Durée du dash en secondes
    [SerializeField] private float dashCooldown = 1f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveActionRef;
    [SerializeField] private InputActionReference jumpActionRef; // Référence pour l'action de saut
    [SerializeField] private InputActionReference dashActionRef;
    
    private Vector2 moveInput;
    private Rigidbody rb;
    private Animator animator;
    private bool canJump;
    private bool isGrounded = true; 
    private bool canDash; // Si le dash est débloqué
    private bool isDashing; // Si le personnage est en train de dasher
    private bool isInteracting = false;
    private Vector3 dashDirection;
    private bool dashOnCooldown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        moveActionRef.action.performed += OnMovePerformed;
        moveActionRef.action.canceled += OnMoveCanceled;
        jumpActionRef.action.performed += OnJumpPerformed;
        dashActionRef.action.performed += OnDashPerformed;
    }

    private void OnEnable()
    {
        moveActionRef.action.Enable();
        jumpActionRef.action.Enable();
        dashActionRef.action.Enable();
    }

    private void OnDisable()
    {
        moveActionRef.action.Disable();
        jumpActionRef.action.Disable();
        dashActionRef.action.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        Stop();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (canJump && isGrounded)
        {
            if (isInteracting) return; 
            Jump();
        }
    }
    
    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (canDash && !isDashing && !dashOnCooldown) // Vérifie que le dash est débloqué et non en cours
        {
            if (isInteracting) return; 
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); 
        isGrounded = false;
    }
    
    private IEnumerator Dash()
    {
        isDashing = true;
        dashOnCooldown = true;

        dashDirection = new Vector3(moveInput.x, 0, 0).normalized;

        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward;
        }

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }

    private void Update()
    {
        if (isInteracting) return; 
        if (isDashing) return; 

        Vector3 moveVector = new Vector3(moveInput.x, 0, 0);
        float inputMagnitude = moveInput.magnitude;

        if (inputMagnitude > 0.01f)
        {
            bool isRunning = inputMagnitude >= runThreshold;
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            animator.SetBool("isWalking", !isRunning);
            animator.SetBool("isRunning", isRunning);

            rb.velocity = new Vector3(moveVector.x * currentSpeed, rb.velocity.y, 0);

            if (moveVector != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Stop();
        }
        
        if (!isGrounded)
        {
            rb.velocity += Vector3.up * -9.81f * gravityMultiplier * Time.deltaTime; 
        }
    }


    private void Stop()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    public void UnlockJump()
    {
        canJump = true; 
        Debug.Log("Saut débloqué !");
    }
    
    public void UnlockDash()
    {
        canDash = true;
        Debug.Log("Dash débloqué !");
    }

    public void SetInteracting(bool interacting)
    {
        isInteracting = interacting;
        if (interacting)
        {
            rb.velocity = Vector3.zero; // Stoppe tout mouvement
        }
    }

}
