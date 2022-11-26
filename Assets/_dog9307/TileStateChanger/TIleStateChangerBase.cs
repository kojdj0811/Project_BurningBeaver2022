using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileStateChangerBase : MonoBehaviour
{
    [SerializeField]
    private string _sfxName = "";
    [SerializeField]
    protected SpriteRenderer _renderer;

    public virtual void Init()
    {
        int indexX = Random.Range(0, MapGenerater.S.mapWidth);
        int indexY = Random.Range(0, MapGenerater.S.mapHeight);

        Tile targetTile = MapGenerater.S.tileContainer[indexY, indexX];
        if (targetTile)
        {
            transform.parent = targetTile.transform;
            transform.localPosition = Vector3.zero;
        }
    }

    public abstract void ChangeTiles(string owner);

    public virtual void DestroyChanger()
    {
        if (_renderer)
            _renderer.gameObject.SetActive(false);

        GimmickManager.S.RemoveGimmick(this);
        Destroy(gameObject);
    }

    public void PlaySFX()
    {
        if (SoundPlayer.S)
            SoundPlayer.S.PlaySfx(_sfxName);
    }
}
