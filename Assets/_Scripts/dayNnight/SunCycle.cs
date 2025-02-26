using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class SunCycle : MonoBehaviour
{
 //   public RectTransform morningPos;
  //  public RectTransform noonPos;
 //   public RectTransform nightPos;
    public float cycleDuration = 10f; // Total cycle time in seconds

 

 //   private RectTransform sunRect;
    private float timer = 0f;
    private bool cycleEnded = false;

    public delegate void CycleEndEvent();
    public event CycleEndEvent OnCycleEnd; 

    void Start()
    {
       // sunRect = GetComponent<RectTransform>();
        StartCoroutine(StartSunCycle());
    }

    IEnumerator StartSunCycle()
    {
        while (timer < cycleDuration)
        {
            timer += Time.deltaTime;
            float t = timer / cycleDuration; 

            if (t < 0.5f) 
            {
             //   sunRect.anchoredPosition = Vector2.Lerp(morningPos.anchoredPosition, noonPos.anchoredPosition, t * 2);
                UpdatePrefabSprites("Morning"); 
            }
            else 
            {
               // sunRect.anchoredPosition = Vector2.Lerp(noonPos.anchoredPosition, nightPos.anchoredPosition, (t - 0.5f) * 2);
                UpdatePrefabSprites("Noon"); 
            }

            yield return null;
        }

       // sunRect.anchoredPosition = nightPos.anchoredPosition;
        UpdatePrefabSprites("Night"); 

        cycleEnded = true;

        OnCycleEnd?.Invoke();
    }

    void UpdatePrefabSprites(string timeOfDay)
    {
        foreach (GameObject obj in GameManger.Instance.Buildings)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            building buildingScript = obj.GetComponent<building>(); 

            if (spriteRenderer != null && buildingScript != null)
            {
                switch (timeOfDay)
                {
                    case "Morning":
                        Debug.Log("changged");
                        spriteRenderer.sprite = buildingScript.morningSprite;
                        break;

                    case "Noon":
                        Debug.Log("changged");
                        spriteRenderer.sprite = buildingScript.noonSprite;
                        break;

                    case "Night":
                        Debug.Log("changged");
                        spriteRenderer.sprite = buildingScript.nightSprite;
                        break;

                    default:
                        Debug.LogWarning("Invalid time of day: " + timeOfDay);
                        break;
                }
            }
        }
    }

}
