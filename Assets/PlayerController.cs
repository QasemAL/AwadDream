using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float deceleration = 2f; // Rate at which the player slows down
    public Button leftButton;   // Reference to the left arrow button
    public Button rightButton;  // Reference to the right arrow button
    //public GameObject clientPanel; // UI panel for accepting/rejecting clients
    //public Button acceptButton;    // Button to accept the client
    //public Button rejectButton;    // Button to reject the client
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float currentSpeed = 0f; // Current speed of the player
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isInteractingWithClient = false;

    private int money = 100;
    public Text moneyText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isInteractingWithClient)
        {
            currentSpeed = 0;
        }
        else
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        if (isMovingLeft)
        {
            // Accelerate to the left
            currentSpeed = Mathf.MoveTowards(currentSpeed, -moveSpeed, moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = true;
            player.layer = 3;
        }
        else if (isMovingRight)
        {
            // Accelerate to the right
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = false;
            player.layer = 4;
        }
        else
        {
            // Decelerate to a stop
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Move the player based on the current speed
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }

    public void OnLeftButtonPressed()
    {
        isMovingLeft = true;
    }

    public void OnLeftButtonReleased()
    {
        isMovingLeft = false;
    }

    public void OnRightButtonPressed()
    {
        isMovingRight = true;
    }

    public void OnRightButtonReleased()
    {
        isMovingRight = false;
    }

    //public void OnClientClicked()
    //{
    //    isInteractingWithClient = true;
    //    clientPanel.SetActive(true); // Show the accept/reject panel
    //}

    //public void OnAcceptButtonClicked()
    //{
    //    Debug.Log("Client Accepted");
    //    clientPanel.SetActive(false);
    //    isInteractingWithClient = false;
    //}

    //public void OnRejectButtonClicked()
    //{
    //    Debug.Log("Client Rejected");
    //    clientPanel.SetActive(false);
    //    isInteractingWithClient = false;
    //}
    public void LoseMoney(int amount)
    {
        money -= amount;
        if (money < 0) money = 0; // Ensure money doesn't go below 0
        UpdateMoneyUI();
        Debug.Log("Lost $" + amount + ". Current money: $" + money);
    }
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + money;
        }
    }
    public float GetCurrentSpeed()
    {
        return math.abs(currentSpeed);
    }
}