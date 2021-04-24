using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioPlayerController : MonoBehaviour {

    public static GameAudioPlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudioClip(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void PlayAudioClipNamed(string audioClipName)
    {
        PlayAudioClip(Resources.Load<AudioClip>("Audio/" + audioClipName));
    }

}


