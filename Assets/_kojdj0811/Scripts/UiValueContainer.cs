using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;


public class UiValueContainer : MonoBehaviour
{
    public static UiValueContainer S;

    private void Awake() {
        if(S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
        DontDestroyOnLoad(gameObject);
    }






}
