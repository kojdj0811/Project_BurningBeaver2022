using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyBox;
using MagicArsenal;

public class EnvironmentFlood : TileStateChangerBase
{
    public enum FloodDir
    {
        NONE = -1,
        UpDown,
        LeftRight,
        END
    }

    private FloodDir _currentDir;
    private int _currentOriginIndex;

    [SerializeField]
    private SpriteMask _mask;

    [Header("발생 횟수")]
    [Tooltip("최소 발생 횟수")]
    [SerializeField]
    private int _minFloodCount = 1;
    [Tooltip("최대 발생 횟수")]
    [SerializeField]
    private int _maxFloodCount = 2;

    [Header("발생 가능 시간")]
    [Tooltip("홍수 시작 시간")]
    [SerializeField]
    private float _startTime = 30.0f;
    [Tooltip("홍수 끝 시간")]
    [SerializeField]
    private float _endTime = 70.0f;
    [Tooltip("홍수 사이 최소 시간 간격")]
    [SerializeField]
    private float _floodMinInterval = 20.0f;

    private List<float> _timeList = new List<float>();
    private int _currentTargetTimeIndex;

    private float _gameStartingTime;

    private bool _isTimerStart = false;

    [SerializeField]
    private Animator _effectAnim;
    [SerializeField]
    private float _animTime = 1.0f;
    [SerializeField]
    private int _animCount = 3;

    [SerializeField]
    private MagicBeamStatic _effect;

    void Start()
    {
        if (_mask)
            _mask.transform.parent = null;

        int floodCount = Random.Range(_minFloodCount, _maxFloodCount + 1);
        float floodTime = _startTime - _floodMinInterval;
        for (int i = 0; i < floodCount; ++i)
        {
            floodTime = Random.Range(floodTime + _floodMinInterval, _endTime);
            floodTime = Mathf.Clamp(floodTime, _startTime, _endTime);

            if (_timeList.Count >= 1)
            {
                if (floodTime - _timeList[i - 1] < _floodMinInterval)
                    break;
            }

            _timeList.Add(floodTime);
        }
        _currentTargetTimeIndex = 0;

        Vector3 scale = _effectAnim.transform.localScale;
        scale.x = MapGenerater.S.tileContainer[0, 0].transform.lossyScale.x;
        _effectAnim.transform.localScale = scale;

        _isTimerStart = false;
    }

    void Update()
    {
        if (!_isTimerStart) return;

        if (Time.timeSinceLevelLoad - _gameStartingTime > _timeList[_currentTargetTimeIndex])
        {
            FloodStart();

            _currentTargetTimeIndex++;
            if (_currentTargetTimeIndex >= _timeList.Count)
                _isTimerStart = false;
        }
    }

    [ButtonMethod]
    public void TimerStart()
    {
        _isTimerStart = true;
        _gameStartingTime = Time.timeSinceLevelLoad;
    }

    public void TimerEnd()
    {
        StopAllCoroutines();
        _isTimerStart = false;
    }

    public override void Init()
    {
        _currentDir = (FloodDir)Random.Range((int)FloodDir.NONE + 1, (int)FloodDir.END);
        Vector3 newPos = transform.position;
        switch (_currentDir)
        {
            case FloodDir.UpDown:
                transform.rotation = Quaternion.identity;
                _currentOriginIndex = Random.Range(0, MapGenerater.S.mapWidth);

                // 위치 변경
                newPos.x = MapGenerater.S.tileContainer[0, _currentOriginIndex].transform.position.x;
            break;

            case FloodDir.LeftRight:
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                _currentOriginIndex = Random.Range(0, MapGenerater.S.mapHeight);

                // 위치 변경
                newPos.y = MapGenerater.S.tileContainer[_currentOriginIndex, 0].transform.position.y;
                break;
        }
        transform.position = newPos;
    }

    void FloodStart()
    {
        Init();
        StartCoroutine(Flooding());
    }

    IEnumerator Flooding()
    {
        // 범위 표시 이펙트
        _effectAnim.SetBool("isBlink", true);
        yield return new WaitForSeconds(_animTime * _animCount);
        _effectAnim.SetBool("isBlink", false);

        ChangeTiles("");

        if (_effect)
        {
            PlaySFX();

            _effect.SpawnBeam();

            yield return new WaitForSeconds(1.0f);

            _effect.RemoveBeam();
        }
    }

    public override void ChangeTiles(string owner)
    {
        switch (_currentDir)
        {
            case FloodDir.UpDown:
                for (int i = 0; i < MapGenerater.S.mapHeight; i++)
                {
                    Tile currentTile = MapGenerater.S.tileContainer[i, _currentOriginIndex];

                    currentTile.SetTiletoNeutrality();
                }
            break;

            case FloodDir.LeftRight:
                for (int i = 0; i < MapGenerater.S.mapWidth; i++)
                {
                    Tile currentTile = MapGenerater.S.tileContainer[_currentOriginIndex, i];

                    currentTile.SetTiletoNeutrality();
                }
            break;
        }
    }
}
