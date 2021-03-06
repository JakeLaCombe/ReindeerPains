using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPill : MonoBehaviour
{
    private bool isSwapped;
    public Sprite SwapSprite;
    public void Swap()
    {
        if (!isSwapped && Supplies.instance.covidPills > 0)
        {
            isSwapped = true;
            Supplies.instance.covidPills -= 1;
            Supplies.instance.reindeerPills += 1;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = SwapSprite;
            SoundManager.instance.Pickup.Play();
        }
    }
}
