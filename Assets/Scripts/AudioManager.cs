﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeBackgroundMusic(AudioClip music)
    {
        BackgroundMusic.Stop();
        BackgroundMusic.clip = music;
        BackgroundMusic.Play();

    }
}
