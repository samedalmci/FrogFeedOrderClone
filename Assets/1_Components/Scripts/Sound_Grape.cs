using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Grape : MonoBehaviour
{
    public static Sound_Grape instance;
    public AudioSource soundGrapeSFX;

    [Header("Grape Sounds")]
    public AudioClip takeGrape;
    public AudioClip wrongeGrape;
    public AudioClip eatGrape;


    [Header("Level Sounds")]
    public AudioClip loseLevel;
    public AudioClip WonLevel;
    public AudioClip Button;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GrapeTakeSFX() 
    {
        soundGrapeSFX.PlayOneShot(takeGrape);

    }
    public void GrapeWrongetSFX()
    {
        soundGrapeSFX.PlayOneShot(wrongeGrape);

    }
    public void GrapeEatSFX()
    {
        soundGrapeSFX.PlayOneShot(eatGrape);

    }
    public void LevelLostSFX()
    {
        soundGrapeSFX.PlayOneShot(loseLevel);

    }
    public void LevelWonSFX()
    {
        soundGrapeSFX.PlayOneShot(WonLevel);

    }
    public void ButtonSFX()
    {
        
        soundGrapeSFX.PlayOneShot(Button);

    }

}
