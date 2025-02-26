using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace "GameScene" with the name of your game scene
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); 
    }
}