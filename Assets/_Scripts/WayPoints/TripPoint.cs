using System.Collections.Generic;
using UnityEngine;

public class TripPoint : MonoBehaviour
{
    public int TripMoney;
    public GameObject passenger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ReachedTarget gain " + TripMoney);
        GameManger.Instance.ReceivePayment(TripMoney);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInteraction>().GotPassenger = false;
        passenger.SetActive(true);
        passenger.transform.position = transform.position;
        passenger.GetComponent<AiController>().ChangeState(new IdleState());
        GameManger.Instance.AwadVoiceClipSource.Stop();
        Destroy(gameObject);
    }

}
