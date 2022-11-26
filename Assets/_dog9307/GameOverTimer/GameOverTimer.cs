using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverTimer : MonoBehaviour
{
    [SerializeField]
    private float _totalGameTime = 100.0f;
    [SerializeField]
    private float _burningStartTime = 80.0f;

    private float _startTime;

    private bool _isAlreadyBurningStart;
    private bool _isGameEnd;

    public UnityEvent OnBurningStart;
    public UnityEvent OnGameTimeOver;

    void Start()
    {
        _isGameEnd = true;
    }

    public void GameTimerStart()
    {
        _isAlreadyBurningStart = false;
        _isGameEnd = false;

        _startTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (_isGameEnd) return;

        if (Time.timeSinceLevelLoad - _startTime >= _burningStartTime &&
            !_isAlreadyBurningStart)
        {
            _isAlreadyBurningStart = true;
            if (OnBurningStart != null)
                OnBurningStart.Invoke();
        }

        if (Time.timeSinceLevelLoad - _startTime >= _totalGameTime)
        {
            if (OnGameTimeOver != null)
                OnGameTimeOver.Invoke();
        }
    }
}
