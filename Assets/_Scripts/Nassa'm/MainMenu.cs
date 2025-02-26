using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    public void StartGame(string sceneName)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(GoToScene(clip.length, sceneName));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    IEnumerator GoToScene(float length, string sceneName)
    {
        yield return new WaitForSeconds(length);
        SceneManager.LoadScene(sceneName);
    }
}
