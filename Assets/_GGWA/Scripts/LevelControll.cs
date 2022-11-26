using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControll : MonoBehaviour
{
    MapGenerater mapGenerater;
    [System.Serializable]
    public struct controll
    {
        [Header("해당 변수들은 타일의 랜덤 리미트 해제 시간, min~max 사이 랜덤 시간")]
        public float minTileSpawingTime;
        public float maxTileSpawingTime;
        public float limitTimeToRandom;
    }

    controll mycontroll;

    private void Update()
    {

    }
    // 값을 바꾸는 스크립트

}
