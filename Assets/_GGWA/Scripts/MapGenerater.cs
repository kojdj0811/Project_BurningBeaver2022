using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    public int mapHeight = 4;
    public int mapWidth = 4;

    public int frameSize;
    public Tile[,] tileContainer;
    public GameObject prefabTile;

    public Sprite biberColor;
    public Sprite humanColor;
    public Sprite neutralityColor;


    public Dictionary<KeyCode,char> keyCharPairs = new Dictionary<KeyCode,char>();
    
    List<Tile> setedTileList = new List<Tile>();
    List<Tile> unsetedTileList = new List<Tile>();

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

    public void GetBackTile(Tile tile)
    {
        unsetedTileList.Add(tile);
    }

    private void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(SetRandomTile());
    }

    IEnumerator SetRandomTile()
    {
        while (true)
        {
            Debug.Log("실행");
            int loopRandomInt = Random.Range(1, 5);
            int loopDestInt = 0;

            float tileSpawingTime = Random.Range(minTileSpawingTime, maxTileSpawingTime);
            char randomChar = (char)Random.Range(97, 123);
            while (loopDestInt < loopRandomInt)
            {
                Debug.Log($"루프 데스티네이션 값 : {loopDestInt}");

                var target = unsetedTileList[Random.Range(0, unsetedTileList.Count)]; // exception 처리
                setedTileList.Add(target);
                unsetedTileList.Remove(target);

                target.SetTile(randomChar, neutralityColor, tileSpawingTime, "neutrality");
                loopDestInt++;
                tileSpawingTime = Random.Range(minTileSpawingTime, maxTileSpawingTime);
                randomChar = (char)Random.Range(97, 123);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    // 스타트 시에 

    public void GenerateMap(int width, int height)
    {
        tileContainer =  new Tile[width,height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var a = Instantiate(prefabTile, transform);
                unsetedTileList.Add(a.GetComponent<Tile>());
                a.transform.position = new Vector3(frameSize/mapWidth * j, frameSize/mapHeight * i,0);
                a.transform.localScale = new Vector3(frameSize / mapWidth, frameSize / mapHeight, 0);
                // 포지션과 사이즈 설정
                tileContainer[i, j] = a.GetComponent<Tile>();
            }
        }
    }


    void SetKeyCharPair()
    {
        int alphabetCount = (int)('Z' - 'A');
        for (int i = 0; i < alphabetCount; i++)
        {
            keyCharPairs.Add(KeyCode.A+i, (char)((int)('A')+i));
        }
    }
}
