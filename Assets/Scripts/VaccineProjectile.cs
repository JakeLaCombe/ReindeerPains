using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody2D;
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void LaunchDirection(Vector3 direction)
    {
        rigidBody2D.velocity = Vector3.Normalize(direction) * 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float xScale = 1.0f;

        if ( rigidBody2D.velocity.x > 0)
        {
            xScale = -1.0f;
        }

        this.transform.localScale = new Vector3(xScale, 1.0f, 1.0f);

        if (rigidBody2D.velocity.y > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 270);
        }

        if (rigidBody2D.velocity.y < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 90);
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null && !enemy.HasBeenVaccinated())
        {
            Destroy(this.gameObject);
            enemy.Vaccinate();
        }

        if (other.name == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
