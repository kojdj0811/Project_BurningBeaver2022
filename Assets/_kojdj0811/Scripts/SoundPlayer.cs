using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer S;



    [Range(0.0f, 1.0f)]
    public float masterVolume = 1.0f;

    [Range(0.0f, 1.0f)]
    public float bgmVolume = 1.0f;

    [Range(0.0f, 1.0f)]
    public float sfxVolume = 1.0f;


    [System.Serializable]
    public enum SoundType
    {
        SFX,
        BGM,        
    }


    [System.Serializable]
    public struct SoundData {
        public SoundType soundType;
        public AudioClip audioClip;

        public SoundData (SoundType soundType, AudioClip audioClip) {
            this.soundType = soundType;
            this.audioClip = audioClip;
        }
    }



 
    public List<AudioClip> bgms;
    public List<AudioClip> sfxs;

    private Dictionary<string, SoundData> _SoundStorage;
    public Dictionary<string, SoundData> SoundStorage {
        get {
            if(_SoundStorage == null) {
                _SoundStorage = new Dictionary<string, SoundData>();

                for (int i = 0; i < bgms.Count; i++)
                {
                    _SoundStorage[bgms[i].name] = new SoundData(SoundType.BGM, bgms[i]);
                }

                for (int i = 0; i < sfxs.Count; i++)
                {
                    _SoundStorage[sfxs[i].name] = new SoundData(SoundType.SFX, sfxs[i]);
                }
            }

            return _SoundStorage;
        }
    }

    private AudioSource gbm;





    private void Awake() {
        if(S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;

        gbm = gameObject.AddComponent<AudioSource>();
        gbm.loop = true;
    }


    public void PlaySfx (string soundName, float volume = -1.0f) {
        PlaySfx(soundName, Vector3.zero, volume);
    }

    public void PlaySfx (string soundName, Vector3 position, float volume = -1.0f) {
        if(!SoundStorage.ContainsKey(soundName) || SoundStorage[soundName].soundType != SoundType.SFX) {
            return;
        }

        AudioSource audioSource = new GameObject(soundName).AddComponent<AudioSource>();
        audioSource.spatialBlend = 0.75f;
        audioSource.transform.SetParent(transform);
        audioSource.transform.position = position;


        float _sfxVolume = 0.0f;
        if(volume <= -1.0f) {
            _sfxVolume = sfxVolume;
        } else {
            _sfxVolume = volume;
        }
        audioSource.volume = _sfxVolume * masterVolume;


        audioSource.clip = SoundStorage[soundName].audioClip;
        audioSource.Play();
        Destroy(audioSource.gameObject, SoundStorage[soundName].audioClip.length);
    }



    public void PlayBgm (string soundName, float volume = -1.0f) {
        if(!SoundStorage.ContainsKey(soundName) || SoundStorage[soundName].soundType != SoundType.BGM) {
            return;
        }


        gbm.Stop();

        if(volume != -1)
            bgmVolume = volume;

        gbm.clip = SoundStorage[soundName].audioClip;
        gbm.Play();
    }



    private void Update() {
        gbm.volume = bgmVolume;
    }
}
