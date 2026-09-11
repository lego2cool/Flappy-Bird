using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PlayerBody;
    [SerializeField] private Animator PlayerAnimator;
    public float flapForce = 5f;
    public float maxFallSpeed = 10f;
    public float maxRotationAngle = -45f;
    public float rotationSpeed = 100f;
    public bool GameIsPlaying = true;


    private InputAction flap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flap = InputSystem.actions.FindAction("Player/Jump");

        if (flap == null)
        {
            Debug.LogError("Could not find the Player/Jump input action.");
            return;
        }

        flap.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float currentZ = transform.eulerAngles.z;

         // Convert 0-360 scale to -180 to 180 scale (330 becomes -30)
        if (currentZ > 180f) currentZ -= 360f;

        if (!GameIsPlaying)
        {
            return;
        }

        if (flap != null && flap.triggered)
        {
            FlapForMe();
        }

        if (currentZ > -30f)
        {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);

        }

        if (PlayerBody.linearVelocity.y < -maxFallSpeed)
        {
            // Player is falling too fast, reset the vertical velocity
            PlayerBody.linearVelocity = new Vector2(PlayerBody.linearVelocity.x, -maxFallSpeed);
        }
    }

    private void OnDestroy()
    {
        flap?.Disable();
    }

    private void FlapForMe()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 30f); // Rotate the player upwards when flapping
        PlayerBody.linearVelocity = Vector2.zero; // Reset the vertical velocity before applying the flap force
        PlayerBody.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TopGround")
        {
            return; // Ignore collision with the top ground
        }
        
        GameIsPlaying = false;
        Debug.Log("Game Over!");
        PlayerAnimator.enabled = false;

        if (collision.gameObject.name == "Pipe")
        {
            collision.collider.enabled = false; // Disable the collider to prevent further collisions
        }
    }
}
