using UnityEngine;

public class CarVibration : MonoBehaviour
{
    public float vibrationIntensity = 0.1f; 
    public float vibrationSpeed = 10f; 
    public float upDownAmplitude = 0.2f; 
    public float upDownSpeed = 1f; 

    private Vector3 originalPosition;
    private bool isBumping = false; 

    void Start()
    {
        originalPosition = transform.position; 
        StartCoroutine(VibrateAndMove()); 
    }

    private System.Collections.IEnumerator VibrateAndMove()
    {
        while (true) 
        {
            if (!isBumping) // Only vibrate and move up/down if not bumping
            {
                
                float offsetX = Mathf.Sin(Time.time * vibrationSpeed) * vibrationIntensity;
                float offsetY = Mathf.Cos(Time.time * vibrationSpeed) * vibrationIntensity;

                float upDownOffset = Mathf.Sin(Time.time * upDownSpeed) * upDownAmplitude;

                transform.position = originalPosition + new Vector3(offsetX, offsetY + upDownOffset, 0);
            }

            yield return null; // Wait for the next frame
        }
    }

    public void ApplyBumpEffectImg(float bumpForce)
    {
            isBumping = true;
            StartCoroutine(Bump(bumpForce));
    }
    private System.Collections.IEnumerator Bump(float bumpForce)
    {
        float duration = 0.5f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetY = Mathf.Sin(elapsed / duration * Mathf.PI) * bumpForce;
            transform.position = originalPosition + new Vector3(0, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isBumping = false;
    }
}