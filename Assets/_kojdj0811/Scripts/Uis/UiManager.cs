using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyBox;


public class UiManager : MonoBehaviour
{
    public static UiManager S;


    [SerializeField, ReadOnly, Range(0.0f, 1.0f)]
    private float totalTilePercentGauge;
    public float TotalTilePercentGauge {
        get => totalTilePercentGauge;
        set {
            totalTilePercentGauge = value;
            totalTilePercentGauge = Mathf.Clamp01(totalTilePercentGauge);
            totalTilePercentGaugeComponent.SetTotalTilePercentGauge(totalTilePercentGauge);
            // SetTotalTilePercentGauge(totalTilePercentGauge);
        }

    }


    [SerializeField, ReadOnly, Range(0.0f, 1.0f)]
    private float remainedTimeGauge;
    public float RemainedTimeGauge {
        get => remainedTimeGauge;
        set {
            remainedTimeGauge = value;
            remainedTimeGauge = Mathf.Clamp01(remainedTimeGauge);
            SetRemainedTimeGauge(remainedTimeGauge);
        }

    }



    [Separator("Setup")]
    public Transform popupsRoot;
    public TotalTilePercentGauge totalTilePercentGaugeComponent;

    public Image remainedTimeGauge_Left;
    public Image remainedTimeGauge_Right;

    public AnimationCurve remainedTimeEasing;


    public TimeOver timeOver;
    public Scoreboard scoreboard;
    public Winnerboard winnerboard;



    private Dictionary<string, GameObject> popups;






    private void Awake() {
        if(S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
        DontDestroyOnLoad(gameObject);
 
 
        popups = new Dictionary<string, GameObject>();
        for (int i = 0; i < popupsRoot.childCount; i++)
        {
            Transform target = popupsRoot.GetChild(i);
            popups[target.name] = target.gameObject;

            target.gameObject.SetActive(true);
            target.gameObject.SetActive(false);
        }
    }






    private void SetRemainedTimeGauge (float u) {
        u = 1.0f - remainedTimeEasing.Evaluate(1.0f - u);

        remainedTimeGauge_Left.fillAmount = u;
        remainedTimeGauge_Right.fillAmount = u;
    }



    public void SetScoreboardValues (int leftPlayerTileCount, int rightPlayerTileCount, int leftPlayerComboScore, int rightPlayerComboScore, int leftPlayerPenaltyScore, int rightPlayerPenaltyScore) {
        scoreboard.SetScoreboardValues(leftPlayerTileCount, rightPlayerTileCount, leftPlayerComboScore, rightPlayerComboScore, leftPlayerPenaltyScore, rightPlayerPenaltyScore);
    }

    public void StartTimeOverAnimation() {
        timeOver.StartTimeOverAnimation();
    }


    public void SetWinnerboardValues(bool isLeftPlayerWinnder) {
        winnerboard.SetWinnerboardValues(isLeftPlayerWinnder);
    }


    public void ActivePopup(string popupName, bool active) {
        popups[popupName].SetActive(active);
    }


    public void ShutDown () {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
