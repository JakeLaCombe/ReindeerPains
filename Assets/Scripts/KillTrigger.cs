using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mocking Bird")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            this.GetComponentInParent<Enemy>().KillPlayer();
        }
    }
}
