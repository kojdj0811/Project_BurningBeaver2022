using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Image pressToStart;



    private void Update() {
        Color col = pressToStart.color;
        col.a = Mathf.Cos(Time.realtimeSinceStartup * 2.0f) * 0.5f + 0.5f;
        pressToStart.color = col;
    }
}
