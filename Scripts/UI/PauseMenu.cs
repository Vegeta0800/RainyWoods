using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button firstButton;

    private bool selectNextFrame = false;

    private void OnEnable()
    {
        if (EventSystem.current && firstButton)
        {
            EventSystem.current.SetSelectedGameObject(null);
            selectNextFrame = true;
        }
    }

    private void Update()
    {
        if (selectNextFrame)
        {
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
            selectNextFrame = false;
        }
    }

    public void OnContinue()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void OnReturn()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

}
