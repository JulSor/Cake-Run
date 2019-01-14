using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFront : MonoBehaviour
{
    public Rigidbody2D front;
    public float Speed;

    public void Start()
    {
        front = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        LaunchFront();
        Destroy(this.gameObject, 0.7f);
    }
    /*public void Launch(Vector2 direction)
    {
        Vector2 velocity = direction.normalized * Speed;
        front.velocity = velocity;
    }*/
    public void LaunchFront()
    {
        front.AddForce(Vector3.down * Speed);
    }
}
