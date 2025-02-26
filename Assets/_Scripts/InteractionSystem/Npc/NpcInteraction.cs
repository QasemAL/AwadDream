using UnityEngine;

public class NpcInteraction : MonoBehaviour, IInteractable
{
    public bool canInteractWith = false;
    public float MaxTargetTripRange = 30f;
    public GameObject PointPreFab;
    public LayerMask wallLayer; 

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInteraction>().GotPassenger = true;

        Vector2 spawnPosition = Vector2.zero;
        int maxAttempts = 10; 
        bool validPosition = false;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = transform.position.x + Random.Range(-MaxTargetTripRange, MaxTargetTripRange);
            spawnPosition = new Vector2(randomX, transform.position.y);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, spawnPosition - (Vector2)transform.position, Vector2.Distance(transform.position, spawnPosition), wallLayer);

            if (!hit.collider) 
            {
                validPosition = true;
                break;
            }
        }

        if (!validPosition)
        {
            Debug.LogWarning("No valid trip point found!");
            return; 
        }

        var point = Instantiate(PointPreFab, spawnPosition, Quaternion.identity);
        point.GetComponent<TripPoint>().TripMoney = Random.Range(4, 21);

        Destroy(gameObject);
    }
}
