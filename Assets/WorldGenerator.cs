using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject buildingPrefab;
    public GameObject npcPrefab;

    public int buildingCount = 5;
    public int npcCount = 5;
    public float spacing = 2.0f; 

    public Vector2 buildingStartPosition = new Vector2(-5, 0); 
    public Vector2 npcStartPosition = new Vector2(-5, -2); 

    private void Start()
    {
        SpawnObjects(buildingPrefab, buildingStartPosition, buildingCount);
        SpawnObjects(npcPrefab, npcStartPosition, npcCount);
    }

    private void SpawnObjects(GameObject prefab, Vector2 startPos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = startPos + new Vector2(i * spacing, 0);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector2 buildingEnd = buildingStartPosition + new Vector2((buildingCount - 1) * spacing, 0);
        Gizmos.DrawLine(buildingStartPosition, buildingEnd);

        Vector2 npcEnd = npcStartPosition + new Vector2((npcCount - 1) * spacing, 0);
        Gizmos.DrawLine(npcStartPosition, npcEnd);
    }
}