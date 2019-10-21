using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public GameObject gb;
    public Animator animator;
    public float waitTime;
    public ImageFader fader;

    public void FirstEvent()
    {
        gb.SetActive(true);
        StartCoroutine(ENDGAME());
        animator.StopPlayback();
    }

    private IEnumerator ENDGAME()
    {
        float faderWaitTime = fader ? waitTime - fader.fadeDuration : 0;

        yield return new WaitForSeconds(fader ? faderWaitTime : waitTime);

        fader?.FadeIn();

        yield return new WaitForSeconds(fader ? fader.fadeDuration : 0);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
