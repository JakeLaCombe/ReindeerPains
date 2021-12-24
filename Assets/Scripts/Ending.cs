using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Ending : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = GameObject.Find("Status").GetComponent<TextMeshProUGUI>();

        text.text = Supplies.instance.reindeerPills + " Pills Obtained";

        if (Supplies.instance.reindeerPills < 20)
        {
            text.text += "\n\nNot so well! No Christmas for you this year!";
            GameObject.Find("EndingFailed").GetComponent<AudioSource>().Play();
        }
        else if (Supplies.instance.reindeerPills < 100 || Supplies.instance.vaccinatedAdults < 10)
        {
            text.text += "\n\nNot bad, but the reindeers can use more!";
            GameObject.Find("EndingSuccess").GetComponent<AudioSource>().Play();
        }
        else
        {
            text.text += "\n\nNicely Done! Plenty of Pills for the evening!";
            GameObject.Find("EndingSuccess").GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
