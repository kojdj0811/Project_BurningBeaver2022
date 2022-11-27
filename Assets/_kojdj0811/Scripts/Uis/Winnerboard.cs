using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Winnerboard : MonoBehaviour
{
    public float animationDelay0 = 0.75f;
    public TextMeshProUGUI playerName;
    public Image medalImage;
    public Image leftPlayerImage;
    public Image rightPlayerImage;
    public Button retryButton;
    public Button exitButton;


    private WaitForSeconds animDelay0;
    private bool isLeftPlayerWinnder;

    public void SetWinnerboardValues (bool isLeftPlayerWinnder) {
        this.isLeftPlayerWinnder = isLeftPlayerWinnder;
        playerName.text = isLeftPlayerWinnder ? "Beaver" : "Justin";

        playerName.gameObject.SetActive(false);
        medalImage.gameObject.SetActive(false);
        leftPlayerImage.gameObject.SetActive(false);
        rightPlayerImage.gameObject.SetActive(false);

        retryButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void StartWinnerboardAnimation() {
        if(animDelay0 == null)
            animDelay0 = new WaitForSeconds(animationDelay0);

        StartCoroutine(StartWinnerboardAnimation_Coroutine());
    }


    public void ReloadScene () {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private IEnumerator StartWinnerboardAnimation_Coroutine () {
        SoundPlayer.S.PlaySfx("Winner_DrumRoll");

        yield return animDelay0;

        SoundPlayer.S.PlaySfx("Winner_Appear");

        medalImage.gameObject.SetActive(true);
        leftPlayerImage.gameObject.SetActive(isLeftPlayerWinnder);
        rightPlayerImage.gameObject.SetActive(!isLeftPlayerWinnder);

        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
}
