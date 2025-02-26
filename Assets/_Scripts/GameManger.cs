using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    public SunCycle sunCycle;


    public Image driverImage; // Reference to the driver's UI Image
    public Sprite neutralSprite; // Neutral expression
    public Sprite madSprite; // Mad expression
    public Sprite happySprite; // Mad expression

    public List<GameObject> Buildings; // List of objects that will change sprites
    public AudioSource AwadVoiceClipSource;

    public int Money = 0;
    public Text moneyText;

    public int Bumps=0,Tickets=0;

    public int TargetLevel1=50;
    public int TargetLevel2=100;
    public int TargetLevel3=200;

    void Awake()
    {
        UpdateMoneyUI();
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
        UpdateMoneyUI();
        Debug.Log("Lost $" + amount + ". Current money: $" + Money);
    }
    public void ReceivePayment(int amount)
    {
        Money += amount;
        UpdateMoneyUI();
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
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + Money;
        }
    }
}