using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class Tile : MonoBehaviour
{
    public static bool isBeaverTried;
    public static bool isBeaverSuccessed;
    public static bool isHumanTried;
    public static bool isHumanSuccessed;


    private char tileHotKey;
    public TextMeshPro tileHotkeyTextMesh;
    public SpriteRenderer tileFrame;
    public char TileHotKey {
        get => tileHotKey;
        set {
            tileHotKey = value;
            tileHotkeyTextMesh.text = tileHotKey.ToString().ToUpper();
        }
    }

    public KeyCode currentKey;

    public Sprite tileCurrentSprite;
    public SpriteRenderer tileTargetSprite;

    public float spwaningTime;
    public float spwanedTime;
    public string tileType;

    public bool isSetTiled = false;

    public bool _isSetTiled
    {
        get => isSetTiled;
        set => isSetTiled = value;
    }

    Vector4 tileDefaultColor = new Vector4(1,1,1,0);
    
    [Range(0.0f, 1.0f)]
    public float u;

    public TileStateChangerBase changer;

    private void Start()
    {
        tileHotkeyTextMesh = transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        tileFrame = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void drawTargetAlphabetOrFrame()
    {
        TileHotKey = (char)Random.Range(97, 123);
        int offset = TileHotKey - 97;
        currentKey = (KeyCode)((int)KeyCode.A + offset); // 대응 키, 핫키 설정
        spwanedTime = Time.timeSinceLevelLoad;
        spwaningTime = MapGenerater.S.GetRandomSpawningTime();
        switch (tileType)
        {
            case "beaver":
                // 격자의 알파값 설정
                tileHotkeyTextMesh.gameObject.SetActive(false);
                tileFrame.gameObject.SetActive(true);
                break;
            case "neutrality":
                //격자, 텍스트 알파값 설정
                tileHotkeyTextMesh.gameObject.SetActive(true);
                tileFrame.gameObject.SetActive(true);
                break;
            case "human":
                tileHotkeyTextMesh.gameObject.SetActive(true);
                tileFrame.gameObject.SetActive(false);
                tileHotkeyTextMesh.text = tileHotKey.ToString().ToUpper();
                // 텍스트 알파값 설정.
                break;
            case "desert":
                // 황무지로 설정
                tileHotkeyTextMesh.gameObject.SetActive(false);
                tileFrame.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        u = 0;



        //set 알파벳 random 함수 타입을 판단하여, 활성화시킬 개체, 타임을 받아서 설정하기
    }

    public void SetTile(char tileHotKey, KeyCode currentKey, Sprite tileCurrentSprite, float spwaningTime,string tileType)
    {
        u = 0;
        spwanedTime = Time.timeSinceLevelLoad;
        isSetTiled = false;
        TileHotKey = tileHotKey;
        this.tileCurrentSprite = tileCurrentSprite;
        this.tileTargetSprite.sprite = tileCurrentSprite;
        this.spwaningTime = spwaningTime;
        this.tileType = tileType;
        this.currentKey = currentKey;
    }

    public void SetTiletoHuman()
    {
        tileType = "human";
        tileTargetSprite.sprite = MapGenerater.S.humanSprite;

        drawTargetAlphabetOrFrame();

        UiManager.S.TotalTilePercentGauge -= 0.02f;
    }

    public void SetTiletoBeaver()
    {
        tileType = "beaver";
        tileTargetSprite.sprite = MapGenerater.S.beaverSprite;

        drawTargetAlphabetOrFrame();

        UiManager.S.TotalTilePercentGauge += 0.02f;
    }

    public void SetTiletoNeutrality() // 초원 생성
    {
        if (tileType == "beaver")
            UiManager.S.TotalTilePercentGauge -= 0.02f;
        else if (tileType == "human")
            UiManager.S.TotalTilePercentGauge += 0.02f;

        tileType = "neutrality";
        tileTargetSprite.sprite = MapGenerater.S.activedSprite;

        drawTargetAlphabetOrFrame();
    }
    public void SetTiletoDesert() // 사막 임시 생성
    {

        tileType = "desert";
        tileTargetSprite.sprite = MapGenerater.S.desertSprite;
        drawTargetAlphabetOrFrame();
        MapGenerater.S.setedTileList.Remove(this); // 배열 교환
        MapGenerater.S.unsetedTileList.Add(this);
    }


    void humanAction() // 플레이어가 타일을 클릭하면 되돌릴 함수 생성.
    {
        isHumanTried = true;

        //        switch (tileType)
        //        {
        //            case "neutrality":
        //                SetTiletoHuman();

        //                if (isHumanSuccessed)
        //                    MapGenerater.S.humanCombo++;
        //                isHumanSuccessed = true;

        //                //tileCurrentColor.color = Color.blue;
        //Debug.Log("[Human] To Human");
        //                break;
        //            case "beaver":
        //                SetTiletoNeutrality();

        //                if (isHumanSuccessed)
        //                    MapGenerater.S.humanCombo++;
        //                isHumanSuccessed = true;

        //                //tileCurrentColor.color = Color.yellow;
        //Debug.Log("[Human] To Neutrality");
        //                break;
        //            case "human":
        //                isHumanSuccessed = false;
        //                break;
        //            default:
        //                isHumanSuccessed = false;
        //                // 실패 로직은 LateUpdate 에서!
        //                break;
        //        }
        switch (tileType)
        {
            case "neutrality":
                SetTiletoHuman();

                if (isHumanSuccessed)
                    MapGenerater.S.humanCombo++;
                isHumanSuccessed = true;

                //tileCurrentColor.color = Color.blue;
                Debug.Log("[Human] To Human");
                break;
            case "beaver":
                SetTiletoDesert();

                if (isHumanSuccessed)
                    MapGenerater.S.humanCombo++;
                isHumanSuccessed = true;

                //tileCurrentColor.color = Color.yellow;
                Debug.Log("[Human] To Desert");
                break;
            case "human":
                isHumanSuccessed = false;
                break;
            default:
                isHumanSuccessed = false;
                // 실패 로직은 LateUpdate 에서!
                break;
        }

        if (isHumanSuccessed)
        {
            ComboFxManager.S.SpawnComboFx(false, $"{MapGenerater.S.humanCombo}");

            if (changer)
            {
                changer.ChangeTiles("human");
                changer.DestroyChanger();
            }
        }
    }

    void BeaverAction()
    {
        isBeaverTried = true;

        //        if (Input.GetKeyDown(currentKey))
        //        {
        //            switch (tileType)
        //            {
        //                case "neutrality":
        //                    SetTiletoBeaver();

        //                    // set 알파벳 random 함수 타입을 판단하여, 활성화시킬 개체, 타임을 받아서 설정하기
        //                    if(isBeaverSuccessed)
        //                        MapGenerater.S.beaverCombo++;
        //                    isBeaverSuccessed = true;

        //                    //tileCurrentColor.color = Color.gray;
        //Debug.Log("[Beaver] To beaver");
        //                    break;
        //                case "beaver":
        //                    isBeaverSuccessed = false;
        //                    break;
        //                case "human":
        //                    SetTiletoNeutrality();

        //                    if(isBeaverSuccessed)
        //                        MapGenerater.S.beaverCombo++;
        //                    isBeaverSuccessed = true;

        //                    //tileCurrentColor.color = Color.yellow;
        //Debug.Log("[Beaver] To Neutrality");
        //                    break;
        //                default:
        //                    isBeaverSuccessed = false;
        //                    // 실패 로직은 LateUpdate 에서!
        //                    break;
        //            }

        if (Input.GetKeyDown(currentKey))
        {
            switch (tileType)
            {
                case "neutrality":
                    SetTiletoBeaver();

                    // set 알파벳 random 함수 타입을 판단하여, 활성화시킬 개체, 타임을 받아서 설정하기
                    if (isBeaverSuccessed)
                        MapGenerater.S.beaverCombo++;
                    isBeaverSuccessed = true;

                    //tileCurrentColor.color = Color.gray;
                    Debug.Log("[Beaver] To beaver");
                    break;
                case "beaver":
                    isBeaverSuccessed = false;
                    break;
                case "human":
                    SetTiletoDesert();

                    if (isBeaverSuccessed)
                        MapGenerater.S.beaverCombo++;
                    isBeaverSuccessed = true;

                    Debug.Log("[Beaver] To Desert");
                    break;
                default:
                    isBeaverSuccessed = false;
                    // 실패 로직은 LateUpdate 에서!
                    break;
            }
            if (isBeaverSuccessed)
            {
                ComboFxManager.S.SpawnComboFx(true, $"{MapGenerater.S.beaverCombo}");

                if (changer)
                {
                    changer.ChangeTiles("beaver");
                    changer.DestroyChanger();
                }
            }
        }
    }





    private void Update()
    {
        u = (Time.timeSinceLevelLoad - spwanedTime) / spwaningTime;


        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
        {
            if(Input.GetKeyDown((KeyCode)i) && _isSetTiled)
            {
                BeaverAction();
                break;
            }
        }


        if (u <= 1.0f)
        {
            //tileTargetSprite.color = Color.Lerp(tileDefaultColor, Color.white, u);
            tileHotkeyTextMesh.color = Color.Lerp(tileDefaultColor, Color.white, u); // 이 부분 수정
            //tileTargetSprite.sprite = MapGenerater.S.activedSprite;

        }
        else
        {
            u = 1.0f;
            if(_isSetTiled == false)
            {

                _isSetTiled = true;

            }
        }
    }


    private void LateUpdate() {
        if (_isSetTiled) // 타일이 완전히 활성화 되기 전 선택 제한
        {
            if (isBeaverTried && !isBeaverSuccessed)
            {
                MapGenerater.S.BeaverPenalty++;
                Debug.Log("[Beaver] : Failed!!!");
            }

            if (isHumanTried && !isHumanSuccessed)
            {
                MapGenerater.S.HumanPenalty++;
                Debug.Log("[Human] : Failed!!!");
            }


            if (isBeaverTried || isHumanTried)
            {
                MapGenerater.S.beaverTileCount = 0;
                MapGenerater.S.humanTileCount = 0;

                for (int i = 0; i < MapGenerater.S.setedTileList.Count; i++)
                {
                    if (MapGenerater.S.setedTileList[i].tileType == "beaver")
                    {
                        MapGenerater.S.beaverTileCount++;
                    }
                    else if (MapGenerater.S.setedTileList[i].tileType == "human")
                    {
                        MapGenerater.S.humanTileCount++;
                    }
                }
            }

            isBeaverTried = false;
            isHumanTried = false;
        }
    }


    private void OnMouseUpAsButton()
    {
        // 활성화 배열 한번 탐색하는...

        for (int i = 0; i < MapGenerater.S.setedTileList.Count; i++)
        {
            if (MapGenerater.S.setedTileList[i].TileHotKey == this.tileHotKey)
            {
                MapGenerater.S.setedTileList[i].humanAction();
            }
        }
    }
}
