using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class Tile : MonoBehaviour
{
    public static bool isbiberTried;
    public static bool isBiberSuccessed;
    public static bool isHumanTried;
    public static bool isHumanSuccessed;


    private char tileHotKey;
    public TextMeshPro tileHotkeyTextMesh;
    public char TileHotKey {
        get => tileHotKey;
        set {
            tileHotKey = value;
            tileHotkeyTextMesh.text = tileHotKey.ToString();
        }
    }

    public KeyCode currentKey;

    public Sprite tileCurrentSprite;
    public SpriteRenderer tileCurrentColor;

    public float spwaningTime;
    public float spwanedTime;
    public string tileType;

    private bool isSetTiled = false;

    public bool _isSetTiled
    {
        get => isSetTiled;
        set => isSetTiled = value;
    }

    Vector4 tileDefaultColor = new Vector4(0,0,0,1);
    
    [Range(0.0f, 1.0f)]
    public float u;
    
    public bool GetTileSeted()
    {
        return _isSetTiled;
    }

    private void Start()
    {
        
    }

    public void SetTile(char tileHotKey, KeyCode currentKey, Sprite tileCurrentSprite, float spwaningTime,string tileType)
    {
        _isSetTiled = true;
        TileHotKey = tileHotKey;
        this.tileCurrentSprite = tileCurrentSprite;
        this.spwaningTime = spwaningTime;
        this.tileType = tileType;
        this.currentKey = currentKey;
    }

    void humanAction() // 플레이어가 타일을 클릭하면 되돌릴 함수 생성.
    {
        isHumanTried = true;

        switch (tileType)
        {
            case "neutrality":
                tileType = "human";
                tileCurrentSprite = MapGenerater.S.humanColor;

                if(isHumanSuccessed)
                    MapGenerater.S.humanCombo++;
                isHumanSuccessed = true;

                tileCurrentColor.color = Color.blue;
Debug.Log("[Human] To Human");
                break;
            case "bieber":
                tileType = "neutrality";
                tileCurrentSprite = MapGenerater.S.neutralityColor;

                if(isHumanSuccessed)
                    MapGenerater.S.humanCombo++;
                isHumanSuccessed = true;

                tileCurrentColor.color = Color.yellow;
Debug.Log("[Human] To Neutrality");
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
            TileStateChangerBase changer = GetComponentInChildren<TileStateChangerBase>();
            if (changer)
            {
                changer.ChangeTiles("human");
                changer.DestroyChanger();
            }
        }
    }

    void BiberAction()
    {
        isbiberTried = true;

        if (Input.GetKeyDown(currentKey))
        {
            switch (tileType)
            {
                case "neutrality":
                    tileType = "biber";
                    tileCurrentSprite = MapGenerater.S.biberColor;

                    if(isBiberSuccessed)
                        MapGenerater.S.biberCombo++;
                    isBiberSuccessed = true;

                    tileCurrentColor.color = Color.gray;
Debug.Log("[Biber] To biber");
                    break;
                case "bieber":
                    isBiberSuccessed = false;
                    break;
                case "human":
                    tileType = "neutrality";
                    tileCurrentSprite = MapGenerater.S.neutralityColor;

                    if(isBiberSuccessed)
                        MapGenerater.S.biberCombo++;
                    isBiberSuccessed = true;

                    tileCurrentColor.color = Color.yellow;
Debug.Log("[Biber] To Neutrality");
                    break;
                default:
                    isBiberSuccessed = false;
                    // 실패 로직은 LateUpdate 에서!
                    break;
            }

            if (isBiberSuccessed)
            {
                TileStateChangerBase changer = GetComponentInChildren<TileStateChangerBase>();
                if (changer)
                {
                    changer.ChangeTiles("bieber");
                    changer.DestroyChanger();
                }
            }
        }

        // if (Input.GetKeyDown(currentKey) && tileType =="human")
        // {
        //      MapGenerater.S.setedTileList.Add(this); // 중립으로 변경
        // }
        // else if(Input.GetKeyDown(currentKey) && tileType == "neutralityColor")
        // {

        //     // 비버 영역으로 변경
        // }




        // if (Input.anyKeyDown)
        // {
        //     foreach (var key in MapGenerater.S.keyCharPairs)
        //     {
        //         if (Input.GetKeyDown(key.Key) && key.Value == tileHotKey)
        //         {
        //             if (key.Value == TileHotKey)
        //             {
        //                 tileCurrentSprite = MapGenerater.S.biberColor; // 점수 관련 추가
        //             }
        //             // 점수 증가, 콤보 감소
        //             else
        //             {
        //             }
        //         }

        //     }
        // }
    }





    private void Update()
    {
        u = (Time.timeSinceLevelLoad - spwanedTime) / spwaningTime;


        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
        {
            if(Input.GetKeyDown((KeyCode)i))
            {
                BiberAction();
                break;
            }
        }


        if (u <= 1.0f)
        {
            tileCurrentColor.color = Color.Lerp(tileDefaultColor, Color.white, u);
            tileHotkeyTextMesh.color = Color.Lerp(tileDefaultColor, Color.black, u);
        }
        else
        {
            u = 1.0f;
        }
    }


    private void LateUpdate() {
        if(isbiberTried && !isBiberSuccessed)
        {
            MapGenerater.S.BiberPenalty++;
            Debug.Log("[Biber] : Failed!!!");
        }

        if(isHumanTried &&!isHumanSuccessed)
        {
            MapGenerater.S.HumanPenalty++;
            Debug.Log("[Human] : Failed!!!");
        }


        if(isbiberTried || isHumanTried) {
            MapGenerater.S.biberTileCount = 0;
            MapGenerater.S.humanTileCount = 0;

            for (int i = 0; i < MapGenerater.S.setedTileList.Count; i++)
            {
                if(MapGenerater.S.setedTileList[i].tileType == "biber") {
                    MapGenerater.S.biberTileCount++;
                } else if(MapGenerater.S.setedTileList[i].tileType == "human") {
                    MapGenerater.S.humanTileCount++;
                }
            }
        }

        isbiberTried = false;
        isHumanTried = false;
    }


    private void OnMouseUpAsButton()
    {
        humanAction();
        //이곳에 타일변경 정보 넣기
    }
}
