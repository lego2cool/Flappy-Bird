using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PlayerBody;
    public float flapForce = 5f;
    public float maxFallSpeed = 10f;

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
        if (!GameIsPlaying)
        {
            return;
        }

        if (flap != null && flap.triggered)
        {
            FlapForMe();
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
        PlayerBody.linearVelocity = Vector2.zero; // Reset the vertical velocity before applying the flap force
        PlayerBody.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
    }
}
