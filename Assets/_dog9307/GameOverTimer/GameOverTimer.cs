using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverTimer : MonoBehaviour
{
    public static GameOverTimer S;
    private void Awake()
    {
        if (S != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private float _totalGameTime = 100.0f;
    public float totalGametime { get { return _totalGameTime; } }
    [SerializeField]
    private float _burningStartTime = 80.0f;
    public float burningStartTime { get { return _burningStartTime; } }

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
        UiManager.S.TotalTilePercentGauge = 0.5f;

        _isAlreadyBurningStart = false;
        _isGameEnd = false;

        _startTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (_isGameEnd) return;

        float duringTime = Time.timeSinceLevelLoad - _startTime;

        if (duringTime >= _burningStartTime &&
            !_isAlreadyBurningStart)
        {
            _isAlreadyBurningStart = true;
            if (OnBurningStart != null)
                OnBurningStart.Invoke();
        }

        if (duringTime >= _totalGameTime)
        {
            _isGameEnd = true;

            if (OnGameTimeOver != null)
                OnGameTimeOver.Invoke();
        }

        float ratio = duringTime / _totalGameTime;
        UiManager.S.RemainedTimeGauge = 1.0f - ratio;
    }

    public void TestBurningStart()
    {
        print("Burning");
    }

    public void TestGameEnd()
    {
        UiManager.S.ActivePopup("TimeOver", true);
    }

    public void PlayBattleBGM()
    {
        SoundPlayer.S.PlayBgm("Battle_BGM");
    }

    public void PlayBurningBGM()
    {
        SoundPlayer.S.PlayBgm("Burning_BGM");
    }

    [SerializeField]
    private float _endBGMTime = 1.5f;
    public void PlayEndBGM()
    {
        SoundPlayer.S.PlayBgm("End_BGM");
        Invoke("EndBGMOff", _endBGMTime);
    }

    public void EndBGMOff()
    {
        SoundPlayer.S.GetComponent<AudioSource>().Stop();
    }

    public void AppearScoreBoard()
    {
        int leftPlayerTileCount = MapGenerater.S.beaverTileCount;
        int rightPlayerTileCount = MapGenerater.S.humanTileCount;
        int leftPlayerComboScore = MapGenerater.S.beaverCombo;
        int rightPlayerComboScore = MapGenerater.S.humanCombo;
        int leftPlayerPenaltyScore = MapGenerater.S.BeaverPenalty;
        int rightPlayerPenaltyScore = MapGenerater.S.HumanPenalty;
        UiManager.S.SetScoreboardValues(leftPlayerTileCount, rightPlayerTileCount, leftPlayerComboScore, rightPlayerComboScore, leftPlayerPenaltyScore, rightPlayerPenaltyScore);
        UiManager.S.SetWinnerboardValues(true);

        UiManager.S.ActivePopup("TimeOver", true);
        UiManager.S.StartTimeOverAnimation();
    }
}
