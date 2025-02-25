using UnityEngine;

public class NpcInteraction : MonoBehaviour, IInteractable
{
    public bool canInteractWith = false;
    public float MaxTargetTripRange = 10f;
    public GameObject PointPreFab;

    public Vector3 TargetTripPoint;

    public void Interact()
    {
        Debug.Log("Npc Interaction");
        GameObject  player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInteraction>().GotPassenger = true;
        float randomX = transform.position.x + Random.Range(-MaxTargetTripRange, MaxTargetTripRange);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y); 
        var point = Instantiate(PointPreFab, spawnPosition, Quaternion.identity);

        point.GetComponent<TripPoint>().TripMoney = Random.Range(4, 21); 

        Destroy(gameObject);
    }
}
