using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossOccupier : TileStateChangerBase
{
    private int _indexX;
    private int _indexY;

    [SerializeField]
    private GameObject _effectPrefab;

    private bool _isAlreadyUsed = false;

    public override void Init()
    {
        _indexX = Random.Range(0, MapGenerater.S.mapWidth);
        _indexY = Random.Range(0, MapGenerater.S.mapHeight);

        Tile targetTile = MapGenerater.S.tileContainer[_indexY, _indexX];
        if (targetTile)
        {
            targetTile.changer = this;

            transform.parent = targetTile.transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            transform.parent = null;

            transform.position += new Vector3(0.0f, 0.0f, -zPosition);
        }

        _isAlreadyUsed = false;
    }

    public override void ChangeTiles(string owner)
    {
        if (_isAlreadyUsed) return;

        int x = 0;
        int y = 0;

        x = _indexX - 1;
        y = _indexY;
        SetTile(x, y, owner);

        x = _indexX;
        y = _indexY - 1;
        SetTile(x, y, owner);

        x = _indexX + 1;
        y = _indexY;
        SetTile(x, y, owner);

        x = _indexX;
        y = _indexY + 1;
        SetTile(x, y, owner);

        _isAlreadyUsed = true;
    }

    void SetTile(int targetIndexX, int targetIndexY, string owner)
    {
        if (!(0 <= targetIndexX && targetIndexX < MapGenerater.S.mapWidth)) return;
        if (!(0 <= targetIndexY && targetIndexY < MapGenerater.S.mapHeight)) return;

        Tile targetTile = MapGenerater.S.tileContainer[targetIndexY, targetIndexX];

        if (owner == "beaver")
        {
            targetTile.SetTiletoBeaver();
        }
        else if (owner == "human")
        {
            targetTile.SetTiletoHuman();
        }

        if (_effectPrefab)
        {
            GameObject newEffect = Instantiate(_effectPrefab);
            newEffect.transform.position = targetTile.transform.position;
        }

        //float tileSpawingTime = Random.Range(MapGenerater.S.minTileSpawingTime, MapGenerater.S.maxTileSpawingTime);
        //char randomChar = (char)Random.Range(97, 123);

        //int offset = randomChar - 97;
        //KeyCode tempCode = (KeyCode)((int)KeyCode.A + offset);
        //targetTile.SetTile(randomChar, tempCode, targetSprite, tileSpawingTime, owner);
    }

    public override void DestroyChanger()
    {
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
