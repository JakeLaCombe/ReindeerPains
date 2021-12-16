using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject currentObject;
    private void OnTriggerEnter2D(Collider2D other)
    {
        currentObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentObject = null;    
    }

    public GameObject getActiveObject()
    {
        return currentObject;
    }
}
