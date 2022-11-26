using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileStateChangerBase : MonoBehaviour
{
    public string owner;

    public virtual void Init()
    {
        int indexX = Random.Range(0, MapGenerater.S.mapWidth);
        int indexY = Random.Range(0, MapGenerater.S.mapHeight);

        Tile targetTile = MapGenerater.S.tileContainer[indexY, indexX];
        if (targetTile)
            transform.parent = targetTile.transform;
    }

    public abstract void ChangeTiles();
}
