using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxSample : MonoBehaviour
{
    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 20.0f;



        //Sfx sample
        if(Input.GetKeyDown(KeyCode.Space)) {
            position.z = Camera.main.transform.position.z;
            SoundPlayer.S.PlaySfx("sfx00", position);
        }

        if(Input.GetKeyDown(KeyCode.Q)) {
            SoundPlayer.S.PlayBgm("bgm00");
        }

        if(Input.GetKeyDown(KeyCode.W)) {
            SoundPlayer.S.PlayBgm("bgm01");
        }


        //Fx sample
        if(Input.GetKeyDown(KeyCode.A)) {
            FxSpawner.S.SpawnFx("fx00_1.9", position, Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.S)) {
            FxSpawner.S.SpawnFx("fx01_0.53", position, Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.D)) {
            FxSpawner.S.SpawnFx("fx01_0.53", position);
        }
    }
}
