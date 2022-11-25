using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class Tile : MonoBehaviour
{

    public char tileHotKey;
    public TextMeshPro tileHotkeyTextMesh;
    public char TileHotKey {
        get => tileHotKey;
        set {
            tileHotKey = value;
            tileHotkeyTextMesh.text = tileHotKey.ToString();
        }
    }

    Sprite tileCurrentSprite;

    public float spwaningTime;
    public float spwanedTime;
    public string tileType;

    [Range(0.0f, 1.0f)]
    public float u;


    public void SetTile(char tileHotKey, Sprite tileCurrentSprite, float spwaningTime,string tileType)
    {
        this.tileHotKey = tileHotKey;
        this.tileCurrentSprite = tileCurrentSprite;
        this.spwaningTime = spwaningTime;
        this.tileType = tileType; 
    }


    private void Update()
    {
        u = (Time.timeSinceLevelLoad - spwanedTime) / spwaningTime;

        
        if (u <= 1.0f)
        {
            //spawn

        }
        else
        {
            u = 1.0f;

        }

        //currentColor = Color.Lerp(tranparentColor, ownColor, u);
    }


    private void OnMouseUpAsButton()
    {
        Debug.Log(tileHotKey);
    }
}
