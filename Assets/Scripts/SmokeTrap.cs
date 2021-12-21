using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTrap : MonoBehaviour
{
    public bool isActive;
    public BoxCollider2D boxCollider2D;
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        boxCollider2D = this.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isActive", isActive);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Activate()
    {
        animator.SetBool("isActive", true);
        isActive = true;
        GetComponent<MaterialPickup>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isActive)
        {
            Destroy(this.gameObject);
            Supplies.instance.smokeTraps += 1;
        }

        if (other.tag == "Enemy" && isActive)
        {
            Destroy(this.gameObject);
            Smoke smoke = GameObject.Instantiate(Prefabs.instance.SMOKE, this.transform.position, Quaternion.identity);
            other.GetComponent<Enemy>().SleepTransition();
        }
    }
}
