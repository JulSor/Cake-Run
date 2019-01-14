using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PfMoverOneWay : MonoBehaviour
{
    public Rigidbody2D OneWay;

    //Vector2 targetPos;

    [SerializeField] float platSpeed = 3f;
    public bool onMoving = false;

    [SerializeField] Vector2 movementVector = new Vector2(0.0f, 0.0f);

    void Start ()
    {
        OneWay = GetComponent<Rigidbody2D>();
        //targetPos = new Vector2(transform.position.x, transform.position.y + 2.0f);
    }

    void FixedUpdate()
    {
		if (onMoving)
        {
            OneWayMovement();
        }
	}
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("coll");
            onMoving = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onMoving = false;
        }
    }
    public void OneWayMovement()
    {
        float step = platSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movementVector, step);
    }
}
