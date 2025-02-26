using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    public SunCycle sunCycle;

    public Image ClientImage;

    public Image driverImage; // Reference to the driver's UI Image
    public Sprite neutralSprite; // Neutral expression
    public Sprite madSprite; // Mad expression
    public Sprite happySprite; // Mad expression

    public List<GameObject> Buildings; // List of objects that will change sprites
    public AudioSource AwadVoiceClipSource;

    public int Money = 0;

    public LevelManager LevelManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        sunCycle.OnCycleEnd += OnSunCycleComplete;
        if(ClientImage != null)
            ClientImage.enabled = false;
    }

    void OnSunCycleComplete()
    {
        Debug.Log("The sun cycle has ended!");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(Tags.Interactable);
        foreach (GameObject npc in npcs)
        {
            Destroy(npc);
        }
        LevelManager.ShowLevelEndPanel();
    }


    public void LoseMoney(int amount)
    {
        // Change the driver's expression to mad
        if(driverImage!=null) 
        driverImage.sprite = madSprite;

        // Reset to neutral after a short delay
        Invoke("ResetDriverExpression", 2f); // Reset after 2 seconds

        Money -= amount;
        if (Money < 0) Money = 0; // Ensure money doesn't go below 0
        Debug.Log("Lost $" + amount + ". Current money: $" + Money);
    }
    public void ReceivePayment(int amount)
    {
        Money += amount;
        Debug.Log("Received $" + amount + ". Current money: $" + Money);

        // Change the driver's expression to happy
        if(driverImage != null)
        driverImage.sprite = happySprite;

        // Reset to neutral after a short delay
        Invoke("ResetDriverExpression", 2f); // Reset after 2 seconds
    }




    private void ResetDriverExpression()
    {
        // Reset the driver's expression to neutral
        if (driverImage != null)
        driverImage.sprite = neutralSprite;
    }
   
}