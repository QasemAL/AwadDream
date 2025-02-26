using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour, IInteractable
{
    public bool canInteractWith = false;
    public float MaxTargetTripRange = 30f;
    public float minTargetTripRange = 20f; // Ensures trips are not too close
    public GameObject PointPreFab;
    public LayerMask wallLayer;
    public List<AudioClip> audioClips;
    [Range(0f, 1f)] public float audioPlayChance = 0.5f; // 50% chance to play audio instantly
    public float minDelay = 1f;
    public float maxDelay = 3f;

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInteraction>().GotPassenger = true;

        Vector2 spawnPosition = Vector2.zero;
        int maxAttempts = 10;
        bool validPosition = false;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomOffset = Random.Range(minTargetTripRange, MaxTargetTripRange);
            float randomX = transform.position.x + (Random.value < 0.5f ? -randomOffset : randomOffset); // Random left or right
            spawnPosition = new Vector2(randomX, transform.position.y);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, spawnPosition - (Vector2)transform.position, Vector2.Distance(transform.position, spawnPosition), wallLayer);

            if (!hit.collider)
            {
                validPosition = true;
                break;
            }
        }

        if (!validPosition)
        {
            Debug.LogWarning("No valid trip point found!");
            return;
        }

        var point = Instantiate(PointPreFab, spawnPosition, Quaternion.identity);
        TripPoint trip = point.GetComponent<TripPoint>();
        trip.TripMoney = Random.Range(4, 21);
        trip.passenger = this.gameObject;

        if (Random.value <= 0.5f)
        {
            PlayRandomAudio();
        }
        else
        {
            float delay = Random.Range(minDelay, maxDelay);
            Invoke(nameof(PlayRandomAudio), delay);
        }

        this.gameObject.SetActive(false);
    }

    private void PlayRandomAudio()
    {
        if (audioClips.Count > 0)
        {
            int index = Random.Range(0, audioClips.Count);
            GameManger.Instance.AwadVoiceClipSource.PlayOneShot(audioClips[index]);
            audioClips.RemoveAt(index); // Prevent replaying
        }
    }
}
