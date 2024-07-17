using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{

    [Header("General Stuff")]
    public Animator frogAnimator;
    private Frog_Touch frog_touch;
    private string frogColorTag;
    private List<GameObject> pulledGrapes = new List<GameObject>();

    
    [Header("Tongue Settings")]
    public Transform CurvedToungTransform;
    public GameObject CurvedToung;
    public bool touchedArrow;


    [Header("Collider and Transforms")]
    public Transform tongueBase;  
    public BoxCollider boxCollider;
    public Transform tongueEnd; 

    private void Start()
    {        
        frog_touch = GetComponentInParent<Frog_Touch>();
        if (frog_touch == null)
        {
            Debug.LogError("FrogTouch bileþeni bulunamadý!");
        }

        // Set the frogColorTag variable by taking the tag of the main object
        frogColorTag = gameObject.tag;
        
    }

    void Update()
    {
        // Ensure Box Collider always stays at the end of the tongue
        boxCollider.center = tongueEnd.localPosition;

    }

    void OnTriggerEnter(Collider other)
    {
        // If the object you touched has the same tag as you
        if (other.gameObject.CompareTag(frogColorTag))
        {
            // If the number of grapes to be touched hasn't reached the limit yet
            if (frog_touch.grapeTouched < frog_touch.mustGrapeTouch)
            {
                frog_touch.grapeTouched += 1;
                Sound_Grape.instance.GrapeEatSFX();
                boxCollider.size = new Vector3(0f, 0f, 0f);

            }

            // If all grapes to be touched are won
            if (frog_touch.grapeWin == true)
            {
                pulledGrapes.Add(other.gameObject);
                StartCoroutine(PullGrape(other.gameObject, frog_touch.grapeTouched));
                other.gameObject.transform.parent = null;
            }
        }
        else if (true)
        {
            // If tongue touches the arrow
            if (other.gameObject.CompareTag("ArrowLeft"))
            {

                frogAnimator.SetTrigger("TongueLeftTrigger");
                touchedArrow = true;

                boxCollider.center = tongueEnd.localPosition;
                frog_touch.arrowfinish = false;




            }
            else if (other.gameObject.CompareTag("ArrowRight"))
            {
                frogAnimator.SetTrigger("TongueRightTrigger");
                touchedArrow = true;

                boxCollider.center = tongueEnd.localPosition;
                frog_touch.arrowfinish = false;
            }
            else
            {
                // If none of the above, it's a wrong object touched
                if (frog_touch.isTouch == true)
                {
                    Sound_Grape.instance.GrapeWrongetSFX();
                }

                frog_touch.isTouch = false;
                frog_touch.oneTouch = false;

            }
        }



    } 


    private IEnumerator PullGrape(GameObject grape, int index)
    {
        // Control if tongue touches the arrow, target towards curved tongue if true, tongue base position otherwise
        Vector3 targetPos;

        if (touchedArrow)
        {
            targetPos = CurvedToungTransform.position; 
        }
        else
        {
            targetPos = tongueBase.position; 
        }

        // Pull grape towards targetPos if grape object exists and distance to target is greater than 0.1f
        while (grape != null && Vector3.Distance(grape.transform.position, targetPos) > 0.1f)
        {
            float step = 2f * Time.deltaTime;
            if (grape != null)  
            {
                grape.transform.position = Vector3.MoveTowards(grape.transform.position, targetPos, step);
            }
            yield return null;
        }

        // Set grape to targetPos if grape object exists
        if (grape != null)  
        {
            grape.transform.position = targetPos;
        }
    }


}
