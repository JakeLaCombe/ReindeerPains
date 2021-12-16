using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TownChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {        
        Vector3Int position = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        position.z = 0;
        string tileName = this.GetComponent<Tilemap>().GetTile(position).name;

        if (tileName.Contains("store"))
        {
            SceneManager.LoadScene("Store1");
        }
        else if (tileName.Contains("townhouse"))
        {
            string number = tileName.Substring("townhouse".Length);
            SceneManager.LoadScene("House" + number);
        }
    }
}
