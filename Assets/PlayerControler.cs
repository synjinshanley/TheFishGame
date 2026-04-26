using UnityEngine;
using UnityEngine.InputSystem;
 
 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed    = 5f;
    [SerializeField] private float rotateSpeed  = 0.5f;  // tune this down if too sensitive
    [SerializeField] private float groundCheckRadius = 0.3f;   // match your capsule width
    [SerializeField] private float groundCheckDistance = 0.1f; // how far below feet to check
    [SerializeField] private float jumpForce = 1.0f; 
    [SerializeField] private LayerMask groundLayer;   

    private Animator  _animator;
    private Rigidbody _rb;
    private Vector2   _moveInput;
    private float     _yRotation;
 
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Changed from OnDrawGizmosSelected so it always shows, not just when selected
        Vector3 feetPosition = transform.position + Vector3.down;
        feetPosition.y += 1.0f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feetPosition, groundCheckRadius);
    }
#endif

    void Awake()
    {
        _animator  = GetComponent<Animator>();
        _rb        = GetComponent<Rigidbody>();
        _yRotation = transform.eulerAngles.y;  // start from current rotation
 
 
        // Lock and hide cursor so mouse doesn't leave the window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
        groundLayer = LayerMask.GetMask("Ground");    }
 
 
    private bool IsGrounded()
    {
        // Cast a sphere downward from the player's feet
        Vector3 feetPosition = transform.position + Vector3.down; // adjust to your character height
        feetPosition.y += 1.0f;
        return Physics.CheckSphere(feetPosition, groundCheckRadius, groundLayer) ||
               Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 1.0f, groundLayer);
    }
 
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        Debug.Log($"Grounded?: {IsGrounded()}");
        if (IsGrounded())
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
 
    void OnLook(InputValue value)
    {
        Vector2 delta = value.Get<Vector2>();
        _yRotation += delta.x * rotateSpeed;
        _rb.MoveRotation(Quaternion.Euler(0f, _yRotation, 0f));
    }
 
    void OnFish(InputValue value)
    {
        if (value.isPressed && gameManager.instance.isGameOver == false)
        {
            if (Random.value < 0.5f)
                gameManager.instance.StartMashQTE();
            else
                gameManager.instance.StartMultiQTE();
        }
    }
 
    void FixedUpdate()
    {
        if (_moveInput.sqrMagnitude > 0.01f)
        {
            // Move relative to where the player is facing
            Vector3 move = transform.TransformDirection(
                new Vector3(_moveInput.x, 0f, _moveInput.y).normalized
            );
            _rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);
        }
    }
}