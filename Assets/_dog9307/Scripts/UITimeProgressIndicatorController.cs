using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeProgressIndicatorController : MonoBehaviour
{
    [SerializeField]
    private Image _targetImage;

    [SerializeField]
    private float _minY;
    [SerializeField]
    private float _maxY;

    [SerializeField]
    private RectTransform _rc;

    // Update is called once per frame
    void Update()
    {
        if (!_targetImage) return;

        float y = Mathf.Lerp(_maxY, _minY, _targetImage.fillAmount);

        Vector3 newPos = _rc.anchoredPosition;
        newPos.y = y;
        _rc.anchoredPosition = newPos;
    }
}
