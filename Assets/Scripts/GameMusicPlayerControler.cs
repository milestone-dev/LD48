using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlayerControler : MonoBehaviour {

    public static GameMusicPlayerControler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMusic(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }

    public void PlayMusicNamed(string audioClipName)
    {
        PlayMusic(Resources.Load<AudioClip>("Audio/" + audioClipName));
    }
}

