using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour, IInteractable
{
    public bool canInteractWith = false;
    public float MaxTargetTripRange = 30f;
    public float minTargetTripRange = 20f; // Ensures trips are not too close
    public GameObject PointPreFab;
    public LayerMask wallLayer;
    public List<AudioClip> audioEnterClips;
    public List<AudioClip> audioMiddleClips;
    public List<AudioClip> audioExitClips;

    public Sprite FaceSprite;

    [Range(0f, 1f)] public float audioPlayChance = 0.5f; // 50% chance to play enter clip instantly
    public float minDelay = 3f;
    public float maxDelay = 5f;

    public void Interact()
    {
        GameManger.Instance.AwadVoiceClipSource.Stop();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInteraction>().GotPassenger = true;

        Vector2 spawnPosition = Vector2.zero;
        int maxAttempts = 10;
        bool validPosition = false;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomOffset = Random.Range(minTargetTripRange, MaxTargetTripRange);
            float randomX = transform.position.x + (Random.value < 0.5f ? -randomOffset : randomOffset);
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
       
        if(FaceSprite != null)
        {
            GameManger.Instance.ClientImage.enabled = true;
            GameManger.Instance.ClientImage.sprite = FaceSprite;

        }



        // 50% chance to play an enter clip right away
        if (Random.value <= audioPlayChance && audioEnterClips.Count > 0)
        {
            int enterIndex = Random.Range(0, audioEnterClips.Count);
            GameManger.Instance.AwadVoiceClipSource.PlayOneShot(audioEnterClips[enterIndex]);
        }

        // Always play a middle clip between 3 to 5 seconds later
        float delay = Random.Range(minDelay, maxDelay);
        Invoke(nameof(PlayRandomAudio), delay);


        // 50% chance to play an exit clip 

        if (Random.value <= audioPlayChance && audioExitClips.Count > 0)
        {
            int enterIndex = Random.Range(0, audioExitClips.Count);
            trip.leavingClip = audioExitClips[enterIndex];

        }

        this.gameObject.SetActive(false);
    }

    private void PlayRandomAudio()
    {
        if (audioMiddleClips.Count > 0)
        {
            int index = Random.Range(0, audioMiddleClips.Count);
            GameManger.Instance.AwadVoiceClipSource.PlayOneShot(audioMiddleClips[index]);
            audioMiddleClips.RemoveAt(index); // Prevent replaying
        }
    }
}
