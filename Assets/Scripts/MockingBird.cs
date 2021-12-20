using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockingBird : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private bool active;
    void Awake()
    {
        animator = GetComponent<Animator>();
        active = false;
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        animator.SetBool("isActive", true);
        active = true;
    }

    public bool isActive()
    {
        return active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
