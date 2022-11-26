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
        {
            transform.parent = targetTile.transform;
            transform.localPosition = Vector3.zero;
        }
    }

    public override void ChangeTiles(string owner)
    {
        int x = 0;
        int y = 0;

        x = _indexX - 1;
        y = _indexY;
        SetTile(x, y, owner);

        x = _indexX + 1;
        y = _indexY;
        SetTile(x, y, owner);

        x = _indexX;
        y = _indexY - 1;
        SetTile(x, y, owner);

        x = _indexX;
        y = _indexY + 1;
        SetTile(x, y, owner);
    }

    void SetTile(int targetIndexX, int targetIndexY, string owner)
    {
        if (!(0 <= targetIndexX && targetIndexX < MapGenerater.S.mapWidth)) return;
        if (!(0 <= targetIndexY && targetIndexY < MapGenerater.S.mapHeight)) return;

        Tile targetTile = MapGenerater.S.tileContainer[targetIndexY, targetIndexX];

        if (owner == "beaver")
        {
            targetTile.tileType = "beaver";
            targetTile.tileCurrentSprite = MapGenerater.S.beaverColor;

            targetTile.tileCurrentColor.color = Color.gray;
        }
        else if (owner == "human")
        {
            targetTile.tileType = "human";
            targetTile.tileCurrentSprite = MapGenerater.S.humanColor;

            targetTile.tileCurrentColor.color = Color.blue;
        }

        //float tileSpawingTime = Random.Range(MapGenerater.S.minTileSpawingTime, MapGenerater.S.maxTileSpawingTime);
        //char randomChar = (char)Random.Range(97, 123);

        //int offset = randomChar - 97;
        //KeyCode tempCode = (KeyCode)((int)KeyCode.A + offset);
        //targetTile.SetTile(randomChar, tempCode, targetSprite, tileSpawingTime, owner);

        if (_effectPrefab)
        {
            GameObject newEffect = Instantiate(_effectPrefab);
            newEffect.transform.position = targetTile.transform.position;

            PlaySFX();
        }
    }
}
