using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryScript : MonoBehaviour
{

    public AudioClip firstaudio;
    public AudioClip secondaudio;

    public AudioSource audioscrouce;

    private void Start()
    {
        audioscrouce.PlayOneShot(firstaudio);
        StartCoroutine(playafter(firstaudio.length));
    }

    IEnumerator playafter(float len)
    {
        yield return new WaitForSeconds(len);
        audioscrouce.PlayOneShot(secondaudio);
        StartCoroutine(GoLevel1(secondaudio.length));
    }

    IEnumerator GoLevel1(float len)
    {
        yield return new WaitForSeconds(len);
        SceneManager.LoadScene("Level1");
    }



}
