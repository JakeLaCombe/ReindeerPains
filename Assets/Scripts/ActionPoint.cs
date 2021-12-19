using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> currentObjects;

    private void Start()
    {
        currentObjects = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentObjects.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentObjects.Contains(other.gameObject))
        {
            currentObjects.Remove(other.gameObject);
        }
    }

    public List<GameObject> getActiveObjects()
    {
        currentObjects = currentObjects.FindAll(item => item != null);
        return currentObjects;
    }
}
