using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTrap : MonoBehaviour
{
    public bool isActive;
    public BoxCollider2D boxCollider2D;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = this.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isActive)
        {
            Destroy(this.gameObject);
            Supplies.instance.smokeTraps += 1;
        }
    }
}
