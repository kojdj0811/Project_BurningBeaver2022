using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossOccupier : TileStateChangerBase
{
    private int _indexX;
    private int _indexY;

    [SerializeField]
    private GameObject _effectPrefab;

    public override void Init()
    {
        _indexX = Random.Range(0, MapGenerater.S.mapWidth);
        _indexY = Random.Range(0, MapGenerater.S.mapHeight);

        Tile targetTile = MapGenerater.S.tileContainer[_indexY, _indexX];
        if (targetTile)
            transform.parent = targetTile.transform;
    }

    public override void ChangeTiles()
    {
        int x = 0;
        int y = 0;

        x = _indexX - 1;
        y = _indexY;
        SetTile(x, y);

        x = _indexX + 1;
        y = _indexY;
        SetTile(x, y);

        x = _indexX;
        y = _indexY - 1;
        SetTile(x, y);

        x = _indexX;
        y = _indexY + 1;
        SetTile(x, y);
    }

    void SetTile(int targetIndexX, int targetIndexY)
    {
        if (!(0 < targetIndexX && targetIndexX < MapGenerater.S.mapWidth)) return;
        if (!(0 < targetIndexY && targetIndexY < MapGenerater.S.mapHeight)) return;

        Sprite targetSprite = null;

        if (owner == "biber")
            targetSprite = MapGenerater.S.biberColor;
        else if (owner == "human")
            targetSprite = MapGenerater.S.humanColor;

        if (!targetSprite) return;

        Tile targetTile = MapGenerater.S.tileContainer[targetIndexY, targetIndexX];

        float tileSpawingTime = Random.Range(MapGenerater.S.minTileSpawingTime, MapGenerater.S.maxTileSpawingTime);
        char randomChar = (char)Random.Range(97, 123);

        targetTile.SetTile(randomChar, targetSprite, tileSpawingTime, owner);

        if (_effectPrefab)
        {
            GameObject newEffect = Instantiate(_effectPrefab);
            newEffect.transform.position = targetTile.transform.position;
        }
    }
}
