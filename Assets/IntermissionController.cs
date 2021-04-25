using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class IntermissionController : MonoBehaviour
{
    public AudioClip BackgroundMusicClip;
    public AudioClip AmbienceClip;

    private void Start()
    {
        GameController.Instantiate();

        if (BackgroundMusicClip)
            GameMusicPlayerControler.Instance.PlayMusic(BackgroundMusicClip);

        if (AmbienceClip)
            GameAudioPlayerController.Instance.PlayAudioClip(AmbienceClip);
    }
}
