using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSample : MonoBehaviour
{

    private float animStartTime;
    public float animDuration = 10.0f;

    private void Start() {
        UiManager.S.TotalTilePercentGauge = 0.5f;
        UiManager.S.RemainedTimeGauge = 1.0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            UiManager.S.ActivePopup("GameStart", true);
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            UiManager.S.ActivePopup("Pause", true);
        }

        if(Input.GetKeyDown(KeyCode.Return)) {
            UiManager.S.SetScoreboardValues(1, 2, 3, 4, 5, 6);
            UiManager.S.SetWinnerboardValues(true);

            UiManager.S.ActivePopup("TimeOver", true);
            UiManager.S.StartTimeOverAnimation();
        }





        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            UiManager.S.TotalTilePercentGauge -= 0.02f;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            UiManager.S.TotalTilePercentGauge += 0.02f;
        }


        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            ComboFxManager.S.SpawnComboFx(Random.value > 0.5f ? true : false, Random.Range(0, 20).ToString());
        }



        UiManager.S.RemainedTimeGauge = (animStartTime + animDuration - Time.timeSinceLevelLoad) / animDuration;
    }

}
