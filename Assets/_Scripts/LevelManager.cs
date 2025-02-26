using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int fastBumps = 0; 
    public int tickets = 0;
   // public int currentMoney = 0;
    public int moneyTarget = 50;

    public GameObject levelEndPanel; 
    public Text bumpsText; 
    public Text ticketsText; 
    public Text moneyText; 
    public Text moneyTargetText;
    public GameObject MovementPanel;

    public Button Continue;

    void Start()
    {
        levelEndPanel.SetActive(false);
    }

    public void IncrementFastBumps()
    {
        fastBumps++;
    }

    public void IncrementTickets()
    {
        tickets++;
    }



    public void ShowLevelEndPanel()
    {
        bumpsText.text = "Fast Bumps: " + fastBumps;
        ticketsText.text = "Tickets: " + tickets;
        moneyText.text = "Money: $" + GameManger.Instance.Money;
        moneyTargetText.text = "Target: $" + moneyTarget;

        GameManger.Instance.AwadVoiceClipSource.Stop();

        if (GameManger.Instance.Money >= moneyTarget)
            Continue.enabled = true;
        else
            Continue.interactable = false;

        MovementPanel.SetActive(false);
        levelEndPanel.SetActive(true);


    }


    public void onButtonClicked(string sceneName)
    {
         SceneManager.LoadScene(sceneName);
    }

}