using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMovingPF : MonoBehaviour
{
    public GameObject platPos;

    public bool onPlat = false;

    private float moveDistance = 0.15f;
    static int playerLayer = 14;
    public static int playerMask = 1 << playerLayer;

    void Start ()
    {
		
	}
	void Update ()
    {
        RaycastHit2D pfUp = Physics2D.Raycast(transform.position, Vector2.up, moveDistance, playerMask);
        if (pfUp.collider.tag == "Player")
        {
            onPlat = true;
            if (onPlat == true && Input.GetKeyDown("up") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right"))
            {
                onPlat = false;
            }
        }
        if (onPlat)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = platPos.transform.position + new Vector3(0.0f, 0.4f, 0.0f);
        }
    }
}
