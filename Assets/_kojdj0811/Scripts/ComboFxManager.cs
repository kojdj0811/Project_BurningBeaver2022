using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboFxManager : MonoBehaviour
{
    public static ComboFxManager S;
    public ComboFx comboFxOriginLeft;
    public ComboFx comboFxOriginRight;


    private void Awake() {
        if(S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
    }


    public void SpawnComboFx(bool isLeft, string comboText) {
        GameObject combo = null;
        if(isLeft) {
            combo = Instantiate(comboFxOriginLeft.gameObject);
        } else {
            combo = Instantiate(comboFxOriginRight.gameObject);
        }

        combo.transform.localScale = Vector3.one * (Random.Range(0.8f, 1.0f));
        combo.SetActive(true);
        combo.GetComponent<ComboFx>().comboText.text = comboText;
    }
}
