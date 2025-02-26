using UnityEngine;

public class Bump : MonoBehaviour
{
    public float bumpForce = 2f; 
    public float jumpForceOnBump = 5f; //if the player doesn't slow down
    public int penaltyAmount = 5; 

    private PlayerController playerController;
    private CarVibration carVibration;
    public LevelManager levelManager;
    public AudioClip AudioClip;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        carVibration = FindFirstObjectByType<CarVibration>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.GetCurrentSpeed() > playerController.moveSpeed * 0.7f) 
            {
                GameManger.Instance.AwadVoiceClipSource.Stop();
                GameManger.Instance.AwadVoiceClipSource.PlayOneShot(AudioClip);
                ApplyBigJumpEffect();
                GameManger.Instance.LoseMoney(penaltyAmount); 
            }
            else
            {
                ApplyBumpEffect();
            }
        }
    }

    private void ApplyBumpEffect()
    {
        carVibration.ApplyBumpEffectImg(bumpForce);
        Rigidbody2D playerRb = playerController.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.AddForce(Vector2.up * bumpForce, ForceMode2D.Impulse); 
           
        }

    }

    private void ApplyBigJumpEffect()
    {
        levelManager.IncrementFastBumps();
        carVibration.ApplyBumpEffectImg(30f);
        Rigidbody2D playerRb = playerController.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.AddForce(Vector2.up * jumpForceOnBump, ForceMode2D.Impulse); 
            
        }
    }
    
}