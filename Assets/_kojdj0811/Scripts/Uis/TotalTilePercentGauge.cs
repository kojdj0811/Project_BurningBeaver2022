using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalTilePercentGauge : MonoBehaviour
{

    public RectTransform totalTilePercentGauge_Rect;
    public Image totalTilePercentGauge_Left;
    public Image totalTilePercentGauge_Right;
    public Image totalTilePercentGauge_Pivot;
    public AnimationCurve totalTilePercentGaugeEasing;



    public float animDuration;
    private float animStartTime;
    private float animFrom;
    private float animTo;

    private float u;

    public void SetTotalTilePercentGauge(float u) {
        float to = u * 2.0f - 1.0f;
        to = totalTilePercentGaugeEasing.Evaluate(Mathf.Abs(to)) * (to < 0.0f ? -1.0f : 1.0f);
        to = to * 0.5f + 0.5f;

        animFrom = this.u;
        animTo = to;

        animStartTime = Time.timeSinceLevelLoad;
    }

    public void UpdateTotalTilePercentGauge(float u) {
        u = Mathf.Lerp(animFrom, animTo, u);

        totalTilePercentGauge_Left.fillAmount = u;
        totalTilePercentGauge_Right.fillAmount = 1.0f - u;

        Vector3 pivotPosition = totalTilePercentGauge_Pivot.rectTransform.anchoredPosition;
        pivotPosition.x = totalTilePercentGauge_Rect.rect.width * (u - 0.5f);
        totalTilePercentGauge_Pivot.rectTransform.anchoredPosition = pivotPosition;

        this.u = u;
    }

    float EaseOutElastic(float u) {
        float c4 = (2.0f * Mathf.PI) / 3.0f;

        return u == 0.0f
        ? 0.0f
        : u == 1.0f
        ? 1.0f
        : Mathf.Pow(2.0f, -2.0f * u) * Mathf.Sin((u * 10.0f - 0.75f) * c4) + 1.0f;
    }

    void Update()
    {
        float u = Mathf.Clamp01((Time.timeSinceLevelLoad - animStartTime) / animDuration);
        UpdateTotalTilePercentGauge(EaseOutElastic(u));
    }
}
