using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public MaterialType type;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GrabItem();
        }
    }
    public void GrabItem()
    {
        switch (type)
        {
            case MaterialType.REINDEER_PILL:
                Supplies.instance.reindeerPills += 1;
                break;
            case MaterialType.COVID_PILL:
                Supplies.instance.covidPills += 1;
                break;
            case MaterialType.VACCINE:
                Supplies.instance.vaccines += 1;
                break;
            case MaterialType.ROOSTER_DECOY:
                Supplies.instance.roosterDecoys += 1;
                break;
             case MaterialType.SHOTGUN:
                Supplies.instance.hasShotgun = true;
                break;
        }

        Destroy(this.gameObject);
    }
}

public enum MaterialType
{
    REINDEER_PILL,
    COVID_PILL,
    VACCINE,
    ROOSTER_DECOY,
    SHOTGUN

}