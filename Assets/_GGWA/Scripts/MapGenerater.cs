using System.Security;
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

    public Sprite beaverColor;
    public Sprite humanColor;
    public Sprite neutralityColor;


    public Dictionary<KeyCode,char> keyCharPairs = new Dictionary<KeyCode,char>();
    
    public List<Tile> setedTileList = new List<Tile>();
    List<Tile> unsetedTileList = new List<Tile>();

    public float minTileSpawingTime;
    public float maxTileSpawingTime;
    public float limitTimeToRandom;


    public static MapGenerater S;



    public int beaverTileCount;
    public int humanTileCount;
    public int beaverCombo;
    public int humanCombo;

    [SerializeField]
    private int beaverPenalty;
    public int BeaverPenalty {
        get => beaverPenalty;
        set {
            beaverCombo = 0;
            beaverPenalty = value;
        }
    }

    [SerializeField]
    private int humanPenalty;
    public int HumanPenalty {
        get => humanPenalty;
        set {
            humanCombo = 0;
            humanPenalty = value;
        }
    }





    void Awake()
    {
        if (S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
        DontDestroyOnLoad(gameObject);


        beaverTileCount = 0;
        humanTileCount = 0;
        beaverCombo = 0;
        humanCombo = 0;
        BeaverPenalty = 0;
        HumanPenalty = 0;

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
        float startTime = Time.timeSinceLevelLoad;

        while (true) // true 대신 조건을 설정하기
        {
            Debug.Log("실행");
            int updateTileCount;
            if (Time.timeSinceLevelLoad - startTime >= limitTimeToRandom)
            {
                updateTileCount = Random.Range(1, 5);
            }
            else
            {
                updateTileCount = 1;
            }
            Debug.Log(updateTileCount);
            Debug.Log(startTime + "");
            int loopCount = 0;

            float tileSpawingTime = Random.Range(minTileSpawingTime, maxTileSpawingTime);
            char randomChar = (char)Random.Range(97, 123);
            while (loopCount < updateTileCount)
            {
                // dongdong
                if (unsetedTileList.Count <= 0) break;

                var target = unsetedTileList[Random.Range(0, unsetedTileList.Count)]; // exception 처리
                setedTileList.Add(target);
                unsetedTileList.Remove(target);

                // dongdong
                int offset = randomChar - 97;
                KeyCode tempCode = (KeyCode)((int)KeyCode.A + offset);
                target.SetTile(randomChar, tempCode, neutralityColor, tileSpawingTime, "neutrality");

                loopCount++;
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
                a.transform.position = new Vector3(frameSize/(float)mapWidth * j, frameSize/(float)mapHeight * i,0);
                a.transform.localScale = new Vector3(frameSize / (float)mapWidth, frameSize / (float)mapHeight, 0);
                // 포지션과 사이즈 설정
                tileContainer[i, j] = a.GetComponent<Tile>();
            }
        }
    }


    //void SetKeyCharPair()
    //{
    //    int alphabetCount = (int)('Z' - 'A');
    //    for (int i = 0; i < alphabetCount; i++)
    //    {
    //        keyCharPairs.Add(KeyCode.A+i, (char)((int)('A')+i));
    //    }
    //}
}
