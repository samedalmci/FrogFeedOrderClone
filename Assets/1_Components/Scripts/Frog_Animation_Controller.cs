using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Animation_Controller : MonoBehaviour
{
    [Header("General Stuff")]
    public Animator frogAnimator;


    // Triggers the animation for the frog to grow.
    public void GrowAnimation()
    {
        frogAnimator.SetTrigger("GrowTrigger");
    }

    public void FinishAnimation()
    {
        frogAnimator.SetTrigger("FinishGrowTrigger");
    }
}
