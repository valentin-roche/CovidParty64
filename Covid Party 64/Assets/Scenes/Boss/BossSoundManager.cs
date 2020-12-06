using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip HitSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayHitSound()
    {
        AudioSource.clip = HitSound;
        AudioSource.Play();
    }
}