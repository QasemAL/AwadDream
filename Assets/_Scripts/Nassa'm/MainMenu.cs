using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public AudioClip clip;
    public AudioSource audioSource;
    public void StartGame(string Scene)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(GoToScene(clip.length,"Scene"));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); 
    }

    IEnumerator GoToScene(float length,string Scene)
    {
        yield return length;
        SceneManager.LoadScene(Scene); 

    }

}