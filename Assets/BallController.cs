using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
   private Rigidbody rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isLaunched = false;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float launchForce = 15f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => moveInput = Vector2.zero;
        controls.Player.Launch.performed += _ => LaunchBall();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        if (!isLaunched)
        {
            MoveBall();
        }
    }

    private void MoveBall()
    {
        // Only move left/right before launching
        Vector3 movement = new Vector3(moveInput.x, 0, 0) * moveSpeed;
        rb.AddForce(movement, ForceMode.Force);
    }

    private void LaunchBall()
    {
        if (!isLaunched)
        {
            isLaunched = true;
            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
        }
    }
}
