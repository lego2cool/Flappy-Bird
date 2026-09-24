using UnityEngine;

public class Pipe_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float FlyThroughGap = 0f;
    [SerializeField] private float MaxPipeOffsetHeightWhateverThing = 0f;
    [SerializeField] private AllUpgradeModifiers allUpgradeModifiers;

    private float spawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnDrawGizmos()
    {
        Gizmos.color = Color.softGreen;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player != null && player.GameIsPlaying)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval * (1 - allUpgradeModifiers.PipeFrequencyModifier))
            {
                SpawnPipe();
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnPipe()
    {
        float randomOffset = Random.Range(-MaxPipeOffsetHeightWhateverThing, MaxPipeOffsetHeightWhateverThing);
        transform.position = new Vector3(transform.position.x, randomOffset, transform.position.z);
        float currentGap = FlyThroughGap * (1f + (allUpgradeModifiers != null ? allUpgradeModifiers.PipeGapModifier : 0f));
        GameObject upperPipe = Instantiate(pipePrefab, transform.position + Vector3.up * currentGap, Quaternion.identity);
        Instantiate(pipePrefab, transform.position - Vector3.up * currentGap, Quaternion.Euler(180f, 0f, 0f));

        upperPipe.GetComponent<Pipe>().gotExpFromThisPipe = true; // Set the flag to true for the upper pipe to prevent experience gain from it
    }
}
