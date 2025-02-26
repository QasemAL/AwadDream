using UnityEngine;

public class CarVibration : MonoBehaviour
{
    public float vibrationIntensity = 0.1f; // Intensity of the vibration
    public float vibrationSpeed = 10f; // Speed of the vibration
    public float upDownAmplitude = 0.2f; // Amplitude of up-and-down movement
    public float upDownSpeed = 1f; // Speed of up-and-down movement

    private Vector3 originalPosition; // Store the original position of the car
    private bool isBumping = false; // Track if the car is currently bumping

    void Start()
    {
        originalPosition = transform.position; // Save the original position
        StartCoroutine(VibrateAndMove()); // Start the movement coroutine
    }

    private System.Collections.IEnumerator VibrateAndMove()
    {
        while (true) // Infinite loop for continuous movement
        {
            if (!isBumping) // Only vibrate and move up/down if not bumping
            {
                // Calculate vibration offset
                float offsetX = Mathf.Sin(Time.time * vibrationSpeed) * vibrationIntensity;
                float offsetY = Mathf.Cos(Time.time * vibrationSpeed) * vibrationIntensity;

                // Calculate up-and-down movement
                float upDownOffset = Mathf.Sin(Time.time * upDownSpeed) * upDownAmplitude;

                // Apply the offsets to the car's position
                transform.position = originalPosition + new Vector3(offsetX, offsetY + upDownOffset, 0);
            }

            yield return null; // Wait for the next frame
        }
    }

    // Method to apply a bump effect
    public void ApplyBumpEffectImg(float bumpForce)
    {
            isBumping = true;
            StartCoroutine(Bump(bumpForce));
    }
    private System.Collections.IEnumerator Bump(float bumpForce)
    {
        float duration = 0.5f; // Duration of the bump effect
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Move the car up and down based on the bump force
            float offsetY = Mathf.Sin(elapsed / duration * Mathf.PI) * bumpForce;
            transform.position = originalPosition + new Vector3(0, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset to the original position after the bump
        transform.position = originalPosition;
        isBumping = false; // Set bumping state to false
    }
}