using UnityEngine;

public class Pipe_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float FlyThroughGap = 0f;
    [SerializeField] private float MaxPipeOffsetHeightWhateverThing = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= spawnInterval && FindAnyObjectByType<Player>().GameIsPlaying)
        {
            SpawnPipe();
            spawnInterval = Time.time + 2f; // Reset the spawn interval
        }
    }

    private void SpawnPipe()
    {
        float randomOffset = Random.Range(-MaxPipeOffsetHeightWhateverThing, MaxPipeOffsetHeightWhateverThing);
        transform.position = new Vector3(transform.position.x, randomOffset, transform.position.z);
        Instantiate(pipePrefab, transform.position + Vector3.up * FlyThroughGap, Quaternion.identity);
        Instantiate(pipePrefab, transform.position - Vector3.up * FlyThroughGap, Quaternion.Euler(180f, 0f, 0f));
    }
}
