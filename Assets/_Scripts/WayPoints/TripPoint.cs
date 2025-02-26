using System.Collections.Generic;
using UnityEngine;

public class TripPoint : MonoBehaviour
{
    public int TripMoney;
    public GameObject passenger;
    public AudioClip leavingClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManger.Instance.ReceivePayment(TripMoney);
        GameManger.Instance.ClientImage.enabled = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInteraction>().GotPassenger = false;
        passenger.SetActive(true);
        passenger.transform.position = transform.position;
        passenger.GetComponent<AiController>().ChangeState(new IdleState());
        GameManger.Instance.AwadVoiceClipSource.Stop();
        if(leavingClip != null)
        GameManger.Instance.AwadVoiceClipSource.PlayOneShot(leavingClip);
        Destroy(gameObject);
    }

}
