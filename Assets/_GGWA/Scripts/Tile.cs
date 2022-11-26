using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class Tile : MonoBehaviour
{

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

    Sprite tileCurrentSprite;
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
        switch (tileType)
        {
            case "neutrality":
                tileCurrentSprite = MapGenerater.S.humanColor;
                break;
            case "bieber":
                tileCurrentSprite = MapGenerater.S.neutralityColor;
                break;
            case "human":
                break;
            default:
                // 실패 로직
                break;
        }
    }

    void BiberAction()
    {
        if (Input.GetKeyDown(currentKey) && tileType =="human")
        {
             //MapGenerater.S.setedTileList.Add(this); // 중립으로 변경
        }
        else if(Input.GetKeyDown(currentKey) && tileType == "neutralityColor")
        {

            // 비버 영역으로 변경
        }
        




        if (Input.anyKeyDown)
        {
            foreach (var key in MapGenerater.S.keyCharPairs)
            {
                if (Input.GetKeyDown(key.Key) && key.Value == tileHotKey)
                {
                    if (key.Value == TileHotKey)
                    {
                        tileCurrentSprite = MapGenerater.S.biberColor; // 점수 관련 추가
                    }
                    // 점수 증가, 콤보 감소
                    else
                    {
                    }
                }

            }
        }
    }

    private void Update()
    {
        u = (Time.timeSinceLevelLoad - spwanedTime) / spwaningTime;
        BiberAction();

        if (u <= 1.0f)
        {
            //spawn

        }
        else
        {
            u = 1.0f;

        }

        tileCurrentColor.color = Color.Lerp(tileDefaultColor, Color.white, u);
        tileHotkeyTextMesh.color = Color.Lerp(tileDefaultColor, Color.black, u);
    }


    private void OnMouseUpAsButton()
    {
        humanAction();
        //이곳에 타일변경 정보 넣기
    }
}
