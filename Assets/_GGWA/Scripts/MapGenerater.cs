using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    public int mapHeight = 4;
    public int mapWidth = 4;

    public Tile[,] tileContainer;
    public GameObject prefabTile;

    public Sprite biberColor;
    public Sprite humanColor;
    public Sprite neutralityColor;


    public float minTileSpawingTime;
    public float maxTileSpawingTime;

    public static MapGenerater S;

    void Awake()
    {
        if (S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
        DontDestroyOnLoad(gameObject);

        GenerateMap(mapWidth,mapHeight);
    }


    public void GenerateMap(int width, int height)
    {
        tileContainer =  new Tile[width,height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tileContainer[i, j] = Instantiate(prefabTile, transform).GetComponent<Tile>();
                
                float tileSpawingTime = Random.Range(minTileSpawingTime, maxTileSpawingTime);
                char randomChar = (char)Random.Range(97, 123);

                tileContainer[i, j].SetTile(randomChar, biberColor, tileSpawingTime, "neutrality");
            }
        }

    }

}
