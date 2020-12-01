using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip RegularMusic;
    public AudioClip BossMusic;
    public AudioClip IntroMusic;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayRegular()
    {
        if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
        }
        AudioSource.clip = RegularMusic;
        AudioSource.Play();
        AudioSource.loop = true;
    }

    public void PlayBoss()
    {
        if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
        }
        AudioSource.clip = BossMusic;
        AudioSource.Play();
        AudioSource.loop = true;
    }

    public void PlayIntro()
    {
        if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
        }
        AudioSource.clip = IntroMusic;
        AudioSource.Play();
        AudioSource.loop = true;
    }
}