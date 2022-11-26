using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class Tile : MonoBehaviour
{
    public static bool isBibierTried;
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

    void humanAction() // �÷��̾ Ÿ���� Ŭ���ϸ� �ǵ��� �Լ� ����.
    {
        isHumanTried = true;

        switch (tileType)
        {
            case "neutrality":
                tileType = "human";
                tileCurrentSprite = MapGenerater.S.humanColor;
                isHumanSuccessed = true;

                tileCurrentColor.color = Color.blue;
Debug.Log("[Human] To Human");
                break;
            case "bieber":
                tileType = "neutrality";
                tileCurrentSprite = MapGenerater.S.neutralityColor;
                isHumanSuccessed = true;

                tileCurrentColor.color = Color.yellow;
Debug.Log("[Human] To Neutrality");
                break;
            case "human":
                isHumanSuccessed = true;
                break;
            default:
                // ���� ������ LateUpdate ����!
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
        isBibierTried = true;

        if (Input.GetKeyDown(currentKey))
        {
            switch (tileType)
            {
                case "neutrality":
                    tileType = "bieber";
                    tileCurrentSprite = MapGenerater.S.biberColor;
                    isBiberSuccessed = true;

                    tileCurrentColor.color = Color.gray;
Debug.Log("[Biber] To Biber");
                    break;
                case "bieber":
                    isBiberSuccessed = true;
                    break;
                case "human":
                    tileType = "neutrality";
                    tileCurrentSprite = MapGenerater.S.neutralityColor;
                    isBiberSuccessed = true;

                    tileCurrentColor.color = Color.yellow;
Debug.Log("[Biber] To Neutrality");
                    break;
                default:
                    // ���� ������ LateUpdate ����!
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
        //      MapGenerater.S.setedTileList.Add(this); // �߸����� ����
        // }
        // else if(Input.GetKeyDown(currentKey) && tileType == "neutralityColor")
        // {

        //     // ��� �������� ����
        // }




        // if (Input.anyKeyDown)
        // {
        //     foreach (var key in MapGenerater.S.keyCharPairs)
        //     {
        //         if (Input.GetKeyDown(key.Key) && key.Value == tileHotKey)
        //         {
        //             if (key.Value == TileHotKey)
        //             {
        //                 tileCurrentSprite = MapGenerater.S.biberColor; // ���� ���� �߰�
        //             }
        //             // ���� ����, �޺� ����
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

        if (Input.anyKeyDown)
        {
            BiberAction();
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
        if(!isBiberSuccessed)
        {

        }

        if(!isHumanSuccessed)
        {

        }
    }


    private void OnMouseUpAsButton()
    {
        humanAction();
        //�̰��� Ÿ�Ϻ��� ���� �ֱ�
    }
}
