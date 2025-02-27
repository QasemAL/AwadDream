using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class entrylevel3script : MonoBehaviour
{

    public AudioClip firstaudio;
    public AudioClip secondaudio;
    public AudioClip thirdaudio;


    public AudioSource audioscrouce;
    public string sceneName;

    private void Start()
    {
        audioscrouce.PlayOneShot(firstaudio);
        StartCoroutine(playafter(firstaudio.length));
    }

    IEnumerator playafter(float len)
    {
        yield return new WaitForSeconds(len);
        audioscrouce.PlayOneShot(secondaudio);
        StartCoroutine(playAfterFirst(secondaudio.length));
    }

    IEnumerator playAfterFirst(float len)
    {
        yield return new WaitForSeconds(len);
        audioscrouce.PlayOneShot(thirdaudio);
        StartCoroutine(GoLevel1(thirdaudio.length));
    }

    IEnumerator GoLevel1(float len)
    {
        yield return new WaitForSeconds(len);
        SceneManager.LoadScene(sceneName);
    }







}
