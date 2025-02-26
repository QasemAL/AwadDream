using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public List<GameObject> buildingPrefabs;
    public List<GameObject> npcPrefabs;
    public GameObject wallPrefab; // Wall prefab reference

    public int buildingCount = 5;
    public int npcCount = 5;

    public float buildingSpacing = 2.0f;
    public float npcSpacing = 2.0f;

    public Vector2 buildingStartPosition = new Vector2(-5, 0);
    public Vector2 npcStartPosition = new Vector2(-5, -2);

    private void Start()
    {
        SpawnObjectsWithWalls(buildingPrefabs, buildingStartPosition, buildingCount, buildingSpacing,true);
        SpawnObjectsWithWalls(npcPrefabs, npcStartPosition, npcCount, npcSpacing,false);
    }

    private void SpawnObjectsWithWalls(List<GameObject> prefabs, Vector2 startPos, int count, float spacing ,bool isbuilding)
    {
        if (prefabs.Count == 0)
        {
            Debug.LogWarning("No prefabs assigned in the list!");
            return;
        }

        Vector2 endPos = startPos + new Vector2((count - 1) * spacing, 0);

        // Spawn start wall
        if (wallPrefab != null && isbuilding)
        {
            Instantiate(wallPrefab, startPos + new Vector2(-spacing, 0), Quaternion.identity);
        }

        // Spawn objects in between
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = startPos + new Vector2(i * spacing, 0);
            GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Count)];
           var spawned = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            if (isbuilding)
            {
                GameManger.Instance.Buildings.Add(spawned);
            }
        }

        // Spawn end wall
        if (wallPrefab != null && isbuilding)
        {
            Instantiate(wallPrefab, endPos + new Vector2(spacing, 0), Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 buildingEnd = buildingStartPosition + new Vector2((buildingCount - 1) * buildingSpacing, 0);
        Gizmos.DrawLine(buildingStartPosition, buildingEnd);

        Gizmos.color = Color.red;
        Vector2 npcEnd = npcStartPosition + new Vector2((npcCount - 1) * npcSpacing, 0);
        Gizmos.DrawLine(npcStartPosition, npcEnd);
    }
}
