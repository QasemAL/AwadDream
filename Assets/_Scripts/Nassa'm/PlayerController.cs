using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float deceleration = 2f; 
    public float brakeDeceleration = 5f;
    public Button leftButton;   
    public Button rightButton;  
    public Button breakButton;
   
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float currentSpeed = 0f; 
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isBraking = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isBraking)
        {
            // Apply brake deceleration
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakeDeceleration * Time.deltaTime);
        }
        else if (isMovingLeft)
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
    public void OnBreakButtonPressed()
    {
        isBraking = true; 
    }

    public void OnBreakButtonReleased()
    {
        isBraking = false;
    }
    public float GetCurrentSpeed()
    {
        return math.abs(currentSpeed);
    }
}