using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Freya;
using MyBox;

public class ComboFx : MonoBehaviour
{
    public RectTransform rectTransform;
    public TextMeshPro comboText;
    public Rigidbody rigid;

    public float animInitScale;
    public float animDuration;

    public Vector2 jumpDirDeltaMinMax = new Vector2(-15.0f, 45.0f);
    public Vector2 jumpPowerMinMax = new Vector2(1.0f, 2.0f);
    public Vector2 jumpTorqueMinMax = new Vector2(1.0f, 2.0f);

    private float spawnedTime;

    private void Awake() {
        spawnedTime = Time.timeSinceLevelLoad;
        Vector2 jumpDir = Vector2.up;
        jumpDir = jumpDir.Rotate(Mathf.Deg2Rad * UnityEngine.Random.Range(jumpDirDeltaMinMax.x, jumpDirDeltaMinMax.y) * (transform.position.x < 0.0f ? -1.0f : 1.0f));

        rigid.AddForce(jumpDir * UnityEngine.Random.Range(jumpPowerMinMax.x, jumpPowerMinMax.y));
        rigid.AddTorque(Vector3.forward * UnityEngine.Random.Range(jumpTorqueMinMax.x, jumpTorqueMinMax.y));
    }


    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
