using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOver : MonoBehaviour
{
    public Scoreboard scoreboard;
    public float animationDelay0 = 0.75f;
    private WaitForSeconds animDelay0;

    public void StartTimeOverAnimation() {
        if(animDelay0 == null)
            animDelay0 = new WaitForSeconds(animationDelay0);

        StartCoroutine(StartTimeOverAnimation_Coroutine());
    }

    private IEnumerator StartTimeOverAnimation_Coroutine () {
        yield return animDelay0;

        UiManager.S.ActivePopup("TimeOver", false);
        UiManager.S.ActivePopup("Scoreboard", true);
        scoreboard.StartScoreboardAnimation();
    }
}
