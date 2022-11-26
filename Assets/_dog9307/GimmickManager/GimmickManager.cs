using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{
    #region SINGLETON
    public static GimmickManager S;

    void Awake()
    {
        if (S != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField]
    private List<GameObject> _gimmickPrefabs;

    [SerializeField]
    private float _resetTime = 25.0f;
    [SerializeField]
    private int _maximumGimmickCount = 3;

    private List<TileStateChangerBase> _currentGimmickList = new List<TileStateChangerBase>();

    private bool _isStart = false;
    private float _checkTime = 0.0f;

    public void GameStart()
    {
        _isStart = true;
        _checkTime = Time.timeSinceLevelLoad;

        GimmickSetting();
    }

    void Update()
    {
        if (!_isStart) return;

        if (Time.timeSinceLevelLoad - _checkTime > _resetTime)
        {
            _checkTime = Time.timeSinceLevelLoad;

            RemoveAllGimmicks();
            GimmickSetting();
        }
    }

    public void GameEnd()
    {
        _isStart = false;
    }

    private GameObject ChooseRandomGimmick()
    {
        int rndIndex = Random.Range(0, _gimmickPrefabs.Count);

        return _gimmickPrefabs[rndIndex];
    }

    void GimmickSetting()
    {
        int currentGimmickCount = Random.Range(0, _maximumGimmickCount + 1);
        for (int i = 0; i < currentGimmickCount; ++i)
        {
            GameObject prefab = ChooseRandomGimmick();
            if (!prefab)
            {
                --i;
                continue;
            }

            GameObject newGimmick = Instantiate(prefab);
            TileStateChangerBase changer = newGimmick.GetComponent<TileStateChangerBase>();
            if (!changer)
            {
                Destroy(newGimmick);
                --i;
                continue;
            }

            changer.Init();
            _currentGimmickList.Add(changer);
        }
    }

    void RemoveAllGimmicks()
    {
        for (int i = 0; i < _currentGimmickList.Count;)
            _currentGimmickList[i].DestroyChanger();

        _currentGimmickList.Clear();
    }

    public void RemoveGimmick(TileStateChangerBase changer)
    {
        if (_currentGimmickList.Count <= 0) return;

        if (_currentGimmickList.Contains(changer))
            _currentGimmickList.Remove(changer);
    }
}
