using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateUiViaFillAmount : MonoBehaviour
{
    public RectTransform target;
    public Image from;

    public AnimationCurve amplitudeViaFillAmount;
    public AnimationCurve wavelengthViaFillAmount;


    private Vector3 initAnchoredPosition;
    private float remainedTimeToVibrate;

    private void Awake() {
        initAnchoredPosition = target.anchoredPosition;
    }

    void Update()
    {
        remainedTimeToVibrate -= Time.deltaTime;

        if(remainedTimeToVibrate < 0.0f) {
            float wavelength = wavelengthViaFillAmount.Evaluate(1.0f - from.fillAmount);
            wavelength = wavelength - wavelength * 0.5f;

            target.anchoredPosition = initAnchoredPosition + new Vector3(Random.value * wavelength, Random.value * wavelength, 0.0f);


            float amplitude = amplitudeViaFillAmount.Evaluate(1.0f - from.fillAmount);
            remainedTimeToVibrate += amplitude;
        }
    }
}
