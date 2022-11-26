using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUp : TileStateChangerBase
{
    [SerializeField]
    private int _comboCount = 3;

    [SerializeField]
    private GameObject _effectParticlePrefab;
    [SerializeField]
    private Animator _effectAnim;

    private bool _isAlreadyUsed = false;

    public override void Init()
    {
        base.Init();
        _isAlreadyUsed = false;
    }

    public override void ChangeTiles(string owner)
    {
        if (_isAlreadyUsed) return;

        if (owner == "beaver")
        {
            // ºñ¹ö ÄÞº¸ ¾÷
            MapGenerater.S.beaverCombo += _comboCount;
        }
        else if (owner == "human")
        {
            // ¶ß¶Ç ÄÞº¸ ¾÷
            MapGenerater.S.humanCombo += _comboCount;
        }

        if (_effectParticlePrefab)
        {
            GameObject newEffect = Instantiate(_effectParticlePrefab);
            newEffect.transform.position = transform.position;

            if (_effectAnim)
            {
                _effectAnim.SetTrigger("effectOn");

                PlaySFX();
            }
        }
    }

    public override void DestroyChanger()
    {
        _isAlreadyUsed = true;
        GimmickManager.S.RemoveGimmick(this);
        Invoke("Destroy", 1.0f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
