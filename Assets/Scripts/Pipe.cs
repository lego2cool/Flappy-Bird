using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyXPosition = -13f;
    [SerializeField] private ExpManager expManager;
    [SerializeField] private Player player;

    private bool gotExpFromThisPipe = false; // Flag to track if experience has been gained from this pipe

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExpManager expManager = FindAnyObjectByType<ExpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GameIsPlaying)
        {
            MovePipe();
        }

        if (transform.position.x <= player.transform.position.x && !gotExpFromThisPipe)
        {
            expManager.AddExp(1); // Add experience when the pipe is passed
            gotExpFromThisPipe = true; // Set the flag to true to prevent multiple experience gains from the same pipe
        }


        if (transform.position.x < destroyXPosition)
        {
             // Add experience when the pipe is destroyed
            Destroy(gameObject);
        }

    }

    private void MovePipe()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
