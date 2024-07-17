using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Touch : MonoBehaviour
{
    [Header("References")]
    private Frog_Animation_Controller frog_Animation_Controller;
    private Tongue tongue;
    public GameObject tongueObject;
    public GameObject curvedTongue;
    public GameObject arrow;
    public BoxCollider boxCollider;

    [Header("Movement Settings")]
    public float scaleSpeed = 5.0f;

    [Header("Touch States")]
    [HideInInspector] public bool isTouch;
    [HideInInspector] public bool grapeWin;
    [HideInInspector] private bool hasDecremented;
    [HideInInspector] public bool oneTouch;

    [Header("Grape Stats")]
    [HideInInspector] public int grapeEated = 0;
    [HideInInspector] public int grapeTouched = 0;
    public int mustGrapeTouch = 0;
    public bool arrowfinish = true;


    //public void OnMouseDown()
    //{
    //    // When touched, perform action only once and decrement move count
    //    if (tongueObject != null && oneTouch == false)
    //    {
    //        isTouch = false;
    //        StartCoroutine(DelayedOnMouseDown());
    //        GameManager.instance.currentMoves -= 1;
    //        oneTouch = true;

    //    }

    //}

    public void OnMouseOver()
    {
        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1)) // 1 sað týklama düðmesi için indeks numarasýdýr
        {
            OnRightMouseDown();
        }
    }

    public void OnRightMouseDown()
    {
        // When right-clicked over the object, perform action only once and decrement move count
        if (tongueObject != null && oneTouch == false)
        {
            isTouch = false;
            StartCoroutine(DelayedOnMouseDown());
            GameManager.instance.currentMoves -= 1;
            oneTouch = true;
        }
    }

    void Update()
    {
        // Initialize references if not already set
        if (frog_Animation_Controller == null)
        {
            frog_Animation_Controller = GetComponentInChildren<Frog_Animation_Controller>();
        }
        if (tongue == null)
        {
            tongue = GetComponentInChildren<Tongue>();
        }
        if (tongueObject != null)
        {
            HandleTongueScale();
        }
        if (GameManager.instance == null)
        {
            Debug.Log("GameManager Null");
        }



        // Check if enough grapes have been touched and decrement move count
        if (grapeTouched == mustGrapeTouch  && !hasDecremented)
        {
            hasDecremented = true;
            GameManager.instance.minimumMoveLimit -= 1;
            StartCoroutine(DelayedGrapeWin());
        }

        // Perform actions if all grapes are touched and arrow touch is finished
        if (grapeTouched == mustGrapeTouch && arrowfinish == true && !hasDecremented)
        {
            hasDecremented = true;
            GameManager.instance.minimumMoveLimit -= 1;
            StartCoroutine(DelayedGrapeWin());
        }

        // Destroy the frog if all required grapes are eaten
        if (mustGrapeTouch == grapeEated)
        {
            Destroy(gameObject);
            GameManager.instance.frogCount -= 1;
        }

     
    }

    // Start grape win for back
    private IEnumerator DelayedGrapeWin()
    {
        yield return new WaitForSeconds(0.5f);
        grapeWin = true;
        isTouch = false;
        StartCoroutine(DelayedScaleDown());
    }

  
   // Control the scale of the tongue
    private void HandleTongueScale()
    {
        // Scale the tongue forward if touched, otherwise scale it back
        if (isTouch == true)
        {
            
            Vector3 currentScale = tongueObject.transform.localScale;

            currentScale.y += scaleSpeed * Time.deltaTime;

            tongueObject.transform.localScale = currentScale;


        }
        if (isTouch == false && arrowfinish == true)
        {
            StartCoroutine(DelayedScaleDown());
            grapeTouched = 0;
        }



        // Scale the curved tongue forward if touched in arrow direction, otherwise scale it back
        if (tongue != null && tongue.touchedArrow == true && grapeWin == false)
        {

            isTouch = false;
            Vector3 currentScale = curvedTongue.transform.localScale;
            currentScale.y += 65f * Time.deltaTime;
            curvedTongue.transform.localScale = currentScale;

        }
        if (grapeTouched == mustGrapeTouch && arrowfinish == false)
        {


            if (curvedTongue.transform.localScale.y > 1f)
            {
                StartCoroutine(DelayedArrowedScaelDown());
            }

            else
            {
                arrowfinish = true;
                tongue.touchedArrow = false;
                Destroy(arrow,0.6f);
            }

        }

    }


    // Start animation when frog is touched
    private IEnumerator DelayedOnMouseDown()
    {
        frog_Animation_Controller.GrowAnimation();
        //Handheld.Vibrate();
        yield return new WaitForSeconds(1f);
        isTouch = true;

    }

    // Start tongue scale down animation
    private IEnumerator DelayedScaleDown()
    {
        yield return new WaitForSeconds(0.5f);

        if (tongueObject.transform.localScale.y > 0.5f)
        {
            Vector3 currentScale = tongueObject.transform.localScale;
            currentScale.y -= 35f * Time.deltaTime;
            tongueObject.transform.localScale = currentScale;
        }
    }

    // Start curved tongue scale down animation
    private IEnumerator DelayedArrowedScaelDown()
    {
        yield return new WaitForSeconds(0.5f);
        grapeWin = true;
        if (curvedTongue.transform.localScale.y > 1f)
        {
            Vector3 currentScale = curvedTongue.transform.localScale;
            currentScale.y -= 75f * Time.deltaTime;
            curvedTongue.transform.localScale = currentScale;
        }
    }
}


