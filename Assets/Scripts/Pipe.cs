using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyXPosition = -13f;
    [SerializeField] private int expGainedFromPipe = 10; // X position where experience is gained
    private Player player;
    private ExpManager expManager;

    public bool gotExpFromThisPipe = false; // Flag to track if experience has been gained from this pipe

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player>(); 
        expManager = player.expManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.GameIsPlaying)
        {
            MovePipe();
        }

        if (transform.position.x <= player.transform.position.x && !gotExpFromThisPipe)
        {
            expManager.AddExp(expGainedFromPipe); // Add experience when the pipe is passed
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
