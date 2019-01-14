using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRight : MonoBehaviour 
{
    public Rigidbody2D right;
    public float Speed;

    public void Start()
    {
        right = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        LaunchRight();
        Destroy(this.gameObject, 0.7f);
    }
    /*public void Launch(Vector2 direction)
    {
        Vector2 velocity = direction.normalized * Speed;
        front.velocity = velocity;

    }*/
    public void LaunchRight()
    {
        right.AddForce(Vector3.right * Speed);
    }
}
