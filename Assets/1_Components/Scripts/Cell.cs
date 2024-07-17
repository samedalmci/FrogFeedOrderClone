using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("General Stuff")]
    public Animator cellAnimator;  
    public GameObject objectToCreate;  
    private void Update()
    {
        // Start the destroy cell animation if there are no objects under the cell.
        if (gameObject.transform.childCount == 0)
        {
            cellAnimator.SetTrigger("DestroyCellTrigger");
        }

    }
    public void DestroyCell() 
    {
        Destroy(gameObject);
    }

    public void NewCellBorn()
    {
        // Activate the object to create if it exists.
        if (objectToCreate != null)
        {
            objectToCreate.SetActive(true);
        }
        else
        {
            Debug.Log("Üstümde Obje yok");
        }
      
    }
}
