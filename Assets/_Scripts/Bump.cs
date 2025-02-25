using UnityEngine;

public class Bump : MonoBehaviour
{
    public float bumpForce = 2f; // Force applied when hitting a bump
    public float jumpForceOnBump = 10f; // Big jump force if the player doesn't slow down
    public int penaltyAmount = 5; // Money lost for not slowing down

    private PlayerController playerController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.GetCurrentSpeed() > playerController.moveSpeed * 0.5f) // If the player is going too fast
            {
                ApplyBigJumpEffect();
                playerController.LoseMoney(penaltyAmount); // Lose money for not slowing down
            }
            else
            {
                ApplyBumpEffect();
            }
        }
    }

    private void ApplyBumpEffect()
    {
        // Small bump effect
        Rigidbody2D playerRb = playerController.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.AddForce(Vector2.up * bumpForce, ForceMode2D.Impulse); // Bounce effect
           
        }
    }

    private void ApplyBigJumpEffect()
    {
        // Big jump effect
        Rigidbody2D playerRb = playerController.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.AddForce(Vector2.up * jumpForceOnBump, ForceMode2D.Impulse); // Big bounce
            
        }
    }
    
}