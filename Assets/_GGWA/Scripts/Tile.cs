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
            case "beaver":
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

    void BeaverAction()
    {
        isBeaverTried = true;

        if (Input.GetKeyDown(currentKey))
        {
            switch (tileType)
            {
                case "neutrality":
                    tileType = "beaver";
                    tileCurrentSprite = MapGenerater.S.beaverColor;

                    if(isBeaverSuccessed)
                        MapGenerater.S.beaverCombo++;
                    isBeaverSuccessed = true;

                    tileCurrentColor.color = Color.gray;
Debug.Log("[Beaver] To beaver");
                    break;
                case "beaver":
                    isBeaverSuccessed = false;
                    break;
                case "human":
                    tileType = "neutrality";
                    tileCurrentSprite = MapGenerater.S.neutralityColor;

                    if(isBeaverSuccessed)
                        MapGenerater.S.beaverCombo++;
                    isBeaverSuccessed = true;

                    tileCurrentColor.color = Color.yellow;
Debug.Log("[Beaver] To Neutrality");
                    break;
                default:
                    isBeaverSuccessed = false;
                    // 실패 로직은 LateUpdate 에서!
                    break;
            }

            if (isBeaverSuccessed)
            {
                TileStateChangerBase changer = GetComponentInChildren<TileStateChangerBase>();
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
            if(Input.GetKeyDown((KeyCode)i))
            {
                BeaverAction();
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
        if(isBeaverTried && !isBeaverSuccessed)
        {
            MapGenerater.S.BeaverPenalty++;
            Debug.Log("[Beaver] : Failed!!!");
        }

        if(isHumanTried &&!isHumanSuccessed)
        {
            MapGenerater.S.HumanPenalty++;
            Debug.Log("[Human] : Failed!!!");
        }


        if(isBeaverTried || isHumanTried) {
            MapGenerater.S.beaverTileCount = 0;
            MapGenerater.S.humanTileCount = 0;

            for (int i = 0; i < MapGenerater.S.setedTileList.Count; i++)
            {
                if(MapGenerater.S.setedTileList[i].tileType == "beaver") {
                    MapGenerater.S.beaverTileCount++;
                } else if(MapGenerater.S.setedTileList[i].tileType == "human") {
                    MapGenerater.S.humanTileCount++;
                }
            }
        }

        isBeaverTried = false;
        isHumanTried = false;
    }


    private void OnMouseUpAsButton()
    {
        humanAction();
        //이곳에 타일변경 정보 넣기
    }
}
