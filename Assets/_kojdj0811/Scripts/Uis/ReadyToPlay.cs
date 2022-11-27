using System;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ReadyToPlay : MonoBehaviour
{
    public Button mouseReadyButton;

    public Image keyboardReadyButtonImage;
    public Sprite keyboardReadySprite_wait;
    public Sprite keyboardReadySprite_ready;
    public TextMeshProUGUI keyboardReadyText;



    public GameObject countdown;


    private bool isAnimationRunning;

    private WaitForSecondsRealtime wait_1sec;


    private void Awake() {
        isAnimationRunning = false;
        keyboardReadyButtonImage.sprite = keyboardReadySprite_wait;
        wait_1sec = new WaitForSecondsRealtime(1.0f);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            keyboardReadyButtonImage.sprite = keyboardReadySprite_ready;
            keyboardReadyText.text = "READY!";
        }

        if(keyboardReadyButtonImage.sprite == keyboardReadySprite_ready && !mouseReadyButton.interactable) {
            if(!isAnimationRunning) {
                isAnimationRunning = true;
                StartCoroutine(StartReadyAnimation());
            }
        }
    }



    IEnumerator StartReadyAnimation () {
        yield return wait_1sec;
        countdown.SetActive(true);
        gameObject.SetActive(false);
    }
}
