using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FxSpawner : MonoBehaviour
{
    public static FxSpawner S;


    public List<GameObject> fxs;

    private Dictionary<string, GameObject> _FxStorage;
    public Dictionary<string, GameObject> FxStorage {
        get {
            if(_FxStorage == null) {
                _FxStorage = new Dictionary<string, GameObject>();

                for (int i = 0; i < fxs.Count; i++)
                {
                    _FxStorage[fxs[i].name] = fxs[i];
                }
            }

            return _FxStorage;
        }
    }

    





    private void Awake() {
        if(S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;
    }

    public void SpawnFx (string fxName, Vector3 position, Quaternion rotation) {
        if(!FxStorage.ContainsKey(fxName)) {
            return;
        }

        Transform fxTrans = Instantiate(FxStorage[fxName]).transform;
        fxTrans.SetPositionAndRotation(position, rotation);
        fxTrans.transform.SetParent(transform);

        string[] str = fxName.Split('_');
        float duration = float.Parse(str[str.Length - 1]);
        Destroy(fxTrans.gameObject, duration);
    }

    public void SpawnFx (string fxName, Vector3 position, Vector3 eulerAngles) {
        SpawnFx(fxName, position, Quaternion.Euler(eulerAngles));
    }


    public void SpawnFx (string fxName, Vector3 position) {
        if(!FxStorage.ContainsKey(fxName)) {
            return;
        }

        Transform fxTrans = Instantiate(FxStorage[fxName]).transform;
        fxTrans.position = position;
        fxTrans.transform.SetParent(transform);

        string[] str = fxName.Split('_');
        float duration = float.Parse(str[str.Length - 1]);
        Destroy(fxTrans.gameObject, duration);
    }
}
