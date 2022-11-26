using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileStateChangerBase : MonoBehaviour
{
    [SerializeField]
    private string _sfxName = "";
    [SerializeField]
    protected SpriteRenderer _renderer;

    [SerializeField]
    protected float _zPos = 1.0f;

    public virtual void Init()
    {
        int indexX = Random.Range(0, MapGenerater.S.mapWidth);
        int indexY = Random.Range(0, MapGenerater.S.mapHeight);

        Tile targetTile = MapGenerater.S.tileContainer[indexY, indexX];
        if (targetTile)
        {
            transform.parent = targetTile.transform;

            Vector3 newPos = targetTile.transform.position;
            newPos.z = _zPos;
            targetTile.transform.position = newPos;

            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
