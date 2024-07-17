using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour
{
    [Header("General Stuff")]
    public Animator GrapeAnimator;
    private string frogColorTag;
    public Frog_Touch frog_touch;
   


    private void Start()
    {
        // We set the frogColorTag variable by taking the tag of the main object.
        frogColorTag = gameObject.tag;
       
    }    


    void OnTriggerEnter(Collider other)
    {
        // If the object collides with the player, increase grapeEated count and destroy the object.
        if (other.gameObject.CompareTag("Player"))
        {
            frog_touch.grapeEated += 1;
            Destroy(gameObject);
            Sound_Grape.instance.GrapeTakeSFX();
        }

        // If the collided surface has the same tag as this grape, trigger the grow animation.
        if (other.gameObject.CompareTag(frogColorTag))
        {
            //Handheld.Vibrate();
            GrapeAnimator.SetTrigger("GrowGrapeTrigger");
        }

        // If the collided surface does not have the same tag, change its color to red temporarily and trigger the animation.
        else
        {
            
            //Handheld.Vibrate();
            GrapeAnimator.SetTrigger("GrowGrapeTrigger");
            StartCoroutine(ResetColorAfterDelay());

        }
    }


    IEnumerator ResetColorAfterDelay()
    {
        // Get the object's material, change it, then revert it back after waiting
        Renderer renderer = GetComponent<Renderer>();        
        Material mat = renderer.material;
        
        mat.color = Color.red;        
        yield return new WaitForSeconds(0.5f);       
        mat.color = Color.white;
    }
}
