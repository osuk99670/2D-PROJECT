using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip audioClip;
    public float audioVolume;
    AudioSource audioPlayer;

    void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        GameObject audioObject = new GameObject("BgmPlayer");
        audioObject.transform.parent = transform;
        audioPlayer=audioObject.AddComponent<AudioSource>();
        audioPlayer.playOnAwake = false;
        audioPlayer.loop = true;
        audioPlayer.volume = audioVolume;
        audioPlayer.clip= audioClip;
    }

    public void PlayBgm(bool isplay)
    {
        if(isplay) 
        {
            audioPlayer.Play();
        }
        else
        {
            audioPlayer.Stop();
        }
    }
}
