using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLeft : MonoBehaviour 
{
    public Rigidbody2D left;
    public float Speed;

    public void Start()
    {
        left = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        LaunchLeft();
        Destroy(this.gameObject, 0.7f);
    }
    /*public void Launch(Vector2 direction)
    {
        Vector2 velocity = direction.normalized * Speed;
        front.velocity = velocity;

    }*/
    public void LaunchLeft()
    {
        left.AddForce(Vector3.left * Speed);
    }
}
