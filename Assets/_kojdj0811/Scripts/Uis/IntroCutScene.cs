using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutScene : MonoBehaviour
{
    public GameObject readyToPlay;
    void Update()
    {
        if(Input.anyKeyDown) {
            gameObject.SetActive(false);
            readyToPlay.SetActive(true);
        }
    }
}
