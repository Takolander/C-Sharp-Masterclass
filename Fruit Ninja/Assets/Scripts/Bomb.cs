using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade b = collision.GetComponent<Blade>();

        if (!b) 
        { 
            return; 
        }

        FindObjectOfType<GameManger>().OnBombHit();
    }
}
