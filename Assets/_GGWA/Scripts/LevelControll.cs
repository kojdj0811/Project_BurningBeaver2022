using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControll : MonoBehaviour
{
    MapGenerater mapGenerater;
    [System.Serializable]
    public struct controll
    {
        [Header("�ش� �������� Ÿ���� ���� ����Ʈ ���� �ð�, min~max ���� ���� �ð�")]
        public float minTileSpawingTime;
        public float maxTileSpawingTime;
        public float limitTimeToRandom;
    }

    controll mycontroll;

    private void Update()
    {

    }
    // ���� �ٲٴ� ��ũ��Ʈ

}
