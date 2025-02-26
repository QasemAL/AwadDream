using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum LightState { Red, Green }
    public LightState currentState = LightState.Red;
    public SpriteRenderer lightRenderer;
    public Sprite redLightSprite;
    public Sprite greenLightSprite;

    public float redDuration = 3f; // Duration of the red light
    public float greenDuration = 3f; // Duration of the green light

    public int penaltyAmount = 30;

    private PlayerController playerController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        StartCoroutine(TrafficLightCycle()); // Start the traffic light cycle
    }

    public bool IsRed()
    {
        return currentState == LightState.Red;
    }

    private System.Collections.IEnumerator TrafficLightCycle()
    {
        while (true) // Infinite loop for the traffic light cycle
        {
            // Red light
            currentState = LightState.Red;
            lightRenderer.sprite = redLightSprite;
            Debug.Log("Traffic light is now RED.");
            yield return new WaitForSeconds(redDuration);

            // Green light
            currentState = LightState.Green;
            lightRenderer.sprite = greenLightSprite;
            Debug.Log("Traffic light is now GREEN.");
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
               // playerController.LoseMoney(0); // No penalty for stopping
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
                GameManger.Instance.LoseMoney(penaltyAmount); // Penalty for not stopping
                Debug.Log("You got a ticket for not stopping at the red light!");
            }
        }
    }
}