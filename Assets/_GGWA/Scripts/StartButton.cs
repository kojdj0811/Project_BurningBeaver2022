using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public Transform pauseButton;
    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void GameStartButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.GetChild(0).gameObject.SetActive(true);

    }
}
