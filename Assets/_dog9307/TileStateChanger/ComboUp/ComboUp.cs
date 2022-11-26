using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUp : TileStateChangerBase
{
    [SerializeField]
    private int _comboCount = 3;

    [SerializeField]
    private ParticleSystem _effect;

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
        
        PlaySFX();

        _isAlreadyUsed = true;
    }

    public override void DestroyChanger()
    {
        if (_effect)
            _effect.Stop();

        if (_renderer)
            _renderer.gameObject.SetActive(false);

        _isAlreadyUsed = true;
        GimmickManager.S.RemoveGimmick(this);
        Invoke("Destroy", 1.0f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
