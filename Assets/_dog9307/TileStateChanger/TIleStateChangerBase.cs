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
    protected float zPosition = -5.0f;

    public Tile relativeTile { get; set; }

    public virtual void Init()
    {
        int indexX = Random.Range(0, MapGenerater.S.mapWidth);
        int indexY = Random.Range(0, MapGenerater.S.mapHeight);

        Tile targetTile = MapGenerater.S.tileContainer[indexY, indexX];
        if (targetTile)
        {
            targetTile.changer = this;

            transform.parent = targetTile.transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            transform.parent = null;

            transform.position += new Vector3(0.0f, 0.0f, -zPosition);
        }
    }

    public abstract void ChangeTiles(string owner);

    public virtual void DestroyChanger()
    {
        if (_renderer)
            _renderer.gameObject.SetActive(false);

        if (relativeTile)
            relativeTile.changer = null;

        GimmickManager.S.RemoveGimmick(this);
        Destroy(gameObject);
    }

    public void PlaySFX()
    {
        if (SoundPlayer.S)
            SoundPlayer.S.PlaySfx(_sfxName);
    }
}
