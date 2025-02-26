using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource[] completeSounds;
    public AudioSource[] backgroundSounds;

    private int lastBackgroundScene = 0;
    public void checkCompleteSound(int currentScene)
    {
        if (completeSounds[currentScene] != null) completeSounds[currentScene].Play();
    }


    public void checkBackgroundSound(int currentScene)
    {
        if (backgroundSounds[currentScene] != null)
        {
            backgroundSounds[lastBackgroundScene].Stop();
            backgroundSounds[currentScene].Play();
            lastBackgroundScene = currentScene;
        }  
    }
}
