using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public Winnerboard winnerboard;

    public float animationDelay0 = 0.75f;
    public float animationDelay1 = 0.2f;
    public float animationDelay2 = 0.2f;
    public float animationDelay3 = 2.0f;

    public TextMeshProUGUI tileCount_top;
    public TextMeshProUGUI tileCount_buttom;


    public TextMeshProUGUI combo_top;
    public TextMeshProUGUI combo_buttom;


    public TextMeshProUGUI penalty_top;
    public TextMeshProUGUI penalty_buttom;


    private WaitForSeconds animDelay0;
    private WaitForSeconds animDelay1;
    private WaitForSeconds animDelay2;
    private WaitForSeconds animDelay3;


    public void SetScoreboardValues (int leftPlayerTileCount, int rightPlayerTileCount, int leftPlayerComboScore, int rightPlayerComboScore, int leftPlayerPenaltyScore, int rightPlayerPenaltyScore) {
        tileCount_top.text = $"x {leftPlayerTileCount}";
        tileCount_buttom.text = $"x {rightPlayerTileCount}";

        combo_top.text = $"{leftPlayerComboScore} Combo";
        combo_buttom.text = $"{rightPlayerComboScore} Combo";

        penalty_top.text = $"- {leftPlayerPenaltyScore} Penalty";
        penalty_buttom.text = $"- {rightPlayerPenaltyScore} Penalty";




        tileCount_top.gameObject.SetActive(false);
        tileCount_buttom.gameObject.SetActive(false);

        combo_top.gameObject.SetActive(false);
        combo_buttom.gameObject.SetActive(false);

        penalty_top.gameObject.SetActive(false);
        penalty_buttom.gameObject.SetActive(false);
    }

    public void StartScoreboardAnimation() {
        if(animDelay0 == null)
            animDelay0 = new WaitForSeconds(animationDelay0);

        if(animDelay1 == null)
            animDelay1 = new WaitForSeconds(animationDelay1);

        if(animDelay2 == null)
            animDelay2 = new WaitForSeconds(animationDelay2);

        if(animDelay3 == null)
            animDelay3 = new WaitForSeconds(animationDelay3);


        StartCoroutine(StartScoreboardAnimation_Coroutine());
    }

    private IEnumerator StartScoreboardAnimation_Coroutine () {
        yield return animDelay0;

        tileCount_top.gameObject.SetActive(true);
        tileCount_buttom.gameObject.SetActive(true);

        yield return animDelay1;

        combo_top.gameObject.SetActive(true);
        combo_buttom.gameObject.SetActive(true);

        yield return animDelay2;
 
        penalty_top.gameObject.SetActive(true);
        penalty_buttom.gameObject.SetActive(true);

        yield return animDelay3;

        UiManager.S.ActivePopup("Scoreboard", false);
        UiManager.S.ActivePopup("Winnerboard", true);
        winnerboard.StartWinnerboardAnimation();
    }
}
