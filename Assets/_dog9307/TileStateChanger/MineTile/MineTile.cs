using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTile : TileStateChangerBase
{
    [SerializeField]
    private int _minNeutralityCount = 1;
    [SerializeField]
    private int _maxNeutralityCount = 3;

    [SerializeField]
    private GameObject _effectPrefab;

    private List<Tile> FindTiles(string owner)
    {
        List<Tile> findList = new List<Tile>();
        for (int i = 0; i < MapGenerater.S.mapHeight; ++i)
        {
            for (int j = 0; j < MapGenerater.S.mapWidth; ++j)
            {
                Tile currentTile = MapGenerater.S.tileContainer[j, i];
                if (currentTile)
                {
                    if (currentTile.tileType == owner)
                        findList.Add(currentTile);
                }
            }
        }

        return findList;
    }

    public override void ChangeTiles(string owner)
    {
        List<Tile> tileList = FindTiles(owner);
        if (tileList.Count <= 0) return;

        int rndCount = Random.Range(_minNeutralityCount, _maxNeutralityCount + 1);
        for (int i = 0; i < rndCount; ++i)
        {
            int targetIndex = Random.Range(0, tileList.Count);
            if (targetIndex <= 0) continue;

            Tile targetTile = tileList[targetIndex];
            if (targetTile)
            {
                targetTile.SetTiletoNeutrality();
                //targetTile.SetTile(randomChar, MapGenerater.S.neutralityColor, tileSpawingTime, "neutrality");

                if (_effectPrefab)
                {
                    GameObject newEffect = Instantiate(_effectPrefab);
                    newEffect.transform.position = targetTile.transform.position;
                }

                tileList.RemoveAt(targetIndex);
            }
        }

        PlaySFX();
    }
}
