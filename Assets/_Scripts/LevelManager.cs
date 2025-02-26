using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int fastBumps = 0; 
    public int tickets = 0;
    public int currentMoney = 0;
    public int moneyTarget = 50;

    public GameObject levelEndPanel; 
    public Text bumpsText; 
    public Text ticketsText; 
    public Text moneyText; 
    public Text moneyTargetText; 

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

    public void UpdateMoney(int amount)
    {
        currentMoney += amount;
    }

    public void ShowLevelEndPanel()
    {
        bumpsText.text = "Fast Bumps: " + fastBumps;
        ticketsText.text = "Tickets: " + tickets;
        moneyText.text = "Money: $" + currentMoney;
        moneyTargetText.text = "Target: $" + moneyTarget;

        levelEndPanel.SetActive(true);
    }
    /// <summary>
    /// /////////////////////call the showLevelEndPanel
    /// </summary>

    public void OnContinueButtonClicked()
    {
        // SceneManager.LoadScene("NextLevel");
    }
    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}