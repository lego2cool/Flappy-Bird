using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyXPosition = -13f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<Player>().GameIsPlaying)
        {
            MovePipe();
        }


        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
    }

    private void MovePipe()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
