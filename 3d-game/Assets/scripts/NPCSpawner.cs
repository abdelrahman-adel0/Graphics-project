using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC Setup")]
    public GameObject npcPrefab;
    public int spawnCount = 10;

    [Header("Spawn Area")]
    public float spawnRadius = 20f;
    public float raycastHeight = 50f;

    [Header("Ground")]
    public LayerMask groundLayer;

    private float npcHalfHeight;

    void Start()
    {
        // Cache NPC height from prefab collider (no hardcoding)
        Collider col = npcPrefab.GetComponent<Collider>();

        if (col == null)
        {
            Debug.LogError("NPC Prefab must have a Collider!");
            return;
        }

        npcHalfHeight = col.bounds.extents.y;

        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        int spawned = 0;
        int attempts = 0;
        int maxAttempts = spawnCount * 10;

        while (spawned < spawnCount && attempts < maxAttempts)
        {
            attempts++;

            if (TryGetGroundPoint(out Vector3 spawnPoint))
            {
                Instantiate(npcPrefab, spawnPoint, Quaternion.identity);
                spawned++;
            }
        }
    }

    bool TryGetGroundPoint(out Vector3 result)
    {
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            raycastHeight,
            Random.Range(-spawnRadius, spawnRadius)
        );

        if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, raycastHeight * 2f, groundLayer))
        {
            // Use prefab-based height offset (no magic numbers)
            result = hit.point + Vector3.up * npcHalfHeight;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}