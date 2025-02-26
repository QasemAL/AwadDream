using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum LightState { Red, Green }
    public LightState currentState = LightState.Red;
    public SpriteRenderer lightRenderer;
    public Sprite redLightSprite;
    public Sprite greenLightSprite;

    public float redDuration = 3f;
    public float greenDuration = 3f; 

    public int penaltyAmount = 30;

    private PlayerController playerController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        StartCoroutine(TrafficLightCycle()); 
    }

    public bool IsRed()
    {
        return currentState == LightState.Red;
    }

    private System.Collections.IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            currentState = LightState.Red;
            lightRenderer.sprite = redLightSprite;
            yield return new WaitForSeconds(redDuration);

            currentState = LightState.Green;
            lightRenderer.sprite = greenLightSprite;
            yield return new WaitForSeconds(greenDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsRed())
            {
                Debug.Log("Press Space to stop at the traffic light.");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsRed() && playerController.GetCurrentSpeed() == 0f)
            {
                Debug.Log("Player stopped at the traffic light.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsRed())
            {
                GameManger.Instance.LoseMoney(penaltyAmount); 
                Debug.Log("You got a ticket for not stopping at the red light!");
            }
        }
    }
}