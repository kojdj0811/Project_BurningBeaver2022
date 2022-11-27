using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class MapGenerater : MonoBehaviour
{
    public int mapHeight = 4;
    public int mapWidth = 4;
    public float setFrameSizeFloat; // 0.67 defaultValue


    public float tileGererateTerm = 2.0f;
    public int minRandomTerm = 3;
    public int maxRandomTerm = 10;

    public float frameSize;
    public Tile[,] tileContainer;
    public GameObject prefabTile;

    public Sprite beaverSprite;
    public Sprite humanSprite;
    public Sprite activedSprite;
    public Sprite desertSprite;


    public Dictionary<KeyCode,char> keyCharPairs = new Dictionary<KeyCode,char>();
    
    public List<Tile> setedTileList = new List<Tile>();
    public List<Tile> unsetedTileList = new List<Tile>();

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

    public bool isGameEnd { get; set; }

    [Header("�߶� ȿ���� ����Ʈ")]
    public string[] _humanSuccessSfxes;
    public string[] _humanFailSfxes;

    public string humanSuccessSfxName { get { return _humanSuccessSfxes[Random.Range(0, _humanSuccessSfxes.Length)]; } }
    public string humanFailSfxName { get { return _humanFailSfxes[Random.Range(0, _humanFailSfxes.Length)]; } }

    [Header("��� ȿ���� ����Ʈ")]
    public string[] _beaverSuccessSfxes;
    public string[] _beaverFailSfxes;

    public string beaverSuccessSfxName { get { return _beaverSuccessSfxes[Random.Range(0, _beaverSuccessSfxes.Length)]; } }
    public string beaverFailSfxName { get { return _beaverFailSfxes[Random.Range(0, _beaverFailSfxes.Length)]; } }

    [Header("Ȱ��ȭ ȿ����")]
    public string _tileActivateSfxName;

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

    public float GetRandomSpawningTime()
    {
        return Random.Range(minTileSpawingTime, maxTileSpawingTime);
    }
    IEnumerator SetRandomTile()
    {
        float startTime = Time.timeSinceLevelLoad;
        isGameEnd = false;
        while (!isGameEnd) // true ��� ������ �����ϱ�
        {
            Debug.Log("����");
            int updateTileCount;
            if (Time.timeSinceLevelLoad - startTime >= limitTimeToRandom)
            {
                updateTileCount = Random.Range(minRandomTerm, maxRandomTerm);
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

                var target = unsetedTileList[Random.Range(0, unsetedTileList.Count)]; // exception ó��
                setedTileList.Add(target);
                unsetedTileList.Remove(target);

                target.SetTiletoNeutrality();

                loopCount++;
                tileSpawingTime = Random.Range(minTileSpawingTime, maxTileSpawingTime);
                randomChar = (char)Random.Range(97, 123);
            }

            SoundPlayer.S.PlaySfx("Tile_activate");

            yield return new WaitForSeconds(tileGererateTerm);
        }
    }

    // ��ŸƮ �ÿ� 

    public void GenerateMap(int width, int height)
    {
        tileContainer =  new Tile[width,height];

        float deafultGap = (frameSize/mapWidth)* ((width * 0.5f)- 0.5f);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var a = Instantiate(prefabTile, transform);
                unsetedTileList.Add(a.GetComponent<Tile>());

                a.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -i - 1;

                a.transform.position = new Vector3(frameSize/(float)mapWidth * j- deafultGap, frameSize/(float)mapHeight * i- deafultGap+0.5f, 5.0f);
                a.transform.localScale = new Vector3((frameSize / (float)mapWidth) * setFrameSizeFloat, (frameSize / (float)mapHeight) * setFrameSizeFloat, 0); 
                // �����ǰ� ������ ����
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
