using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
    //ADD THIS SCRIPT TO ALL OBJECTS YOU WANT TO DESTROY WHEN COLLIDED WITH PLAYER
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyObject();
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void ProjectileDestroyer()
    {
        Destroy(gameObject, 1f);
    }
}
