using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class TitleReturn : MonoBehaviour, IPointerDownHandler
{
    public string scene;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(scene);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void OnPointerClick()
    {
        SwitchScene();
    }
}
