using UnityEngine;

public class TripPoint : MonoBehaviour
{
    public int TripMoney;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ReachedTarget gain " + TripMoney);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInteraction>().GotPassenger = false;
        Destroy(gameObject);
    }

}
