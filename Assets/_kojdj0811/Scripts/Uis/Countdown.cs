using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    public Button startbutton;
    public TextMeshProUGUI countdownTextMesh;

    public float remainedCountdownTime = 3.0f;

    public float animStartTime;

    bool isAnimationRunning;

    private void Awake() {
        animStartTime = Time.timeSinceLevelLoad;
        isAnimationRunning = false;
    }


    private void Update() {
        if(remainedCountdownTime > 0.0f) {
            remainedCountdownTime -= Time.unscaledDeltaTime;

            countdownTextMesh.text = ((int)remainedCountdownTime + 1).ToString();

            if(!isAnimationRunning && remainedCountdownTime <= 0.0f) {
                isAnimationRunning = true;
                StartCoroutine(StartGameAnimation());
            }
        }
    }


    IEnumerator StartGameAnimation() {
        countdownTextMesh.text = "START!";
        yield return new WaitForSecondsRealtime(1.0f);
        StartGame();
    }




    public void StartGame () {
        startbutton.onClick.Invoke();
        gameObject.SetActive(false);
    }
}
