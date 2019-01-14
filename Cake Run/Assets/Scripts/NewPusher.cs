using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPusher : MonoBehaviour
{
    //Pusherin liikuteltava osa.
    public Rigidbody2D pusher;
    public GameObject pusherPos;
    
    Vector2 targetPosOut;
    Vector2 targetPosIn;

    //Katsotaan onko pusheri tuuppaamassa ulospäin.
    public bool pushing = false;

    //Pusherin ulos- ja sisäänmenonopeudet.
    private float outSpeed = 8f;
    private float inSpeed = 1f;

    public float waitTime = 2f;

    public float moveDistance = 1;
    static int playerLayer = 14;
    public static int playerMask = 1 << playerLayer;

    void Start ()
    {
        pusher = GetComponent<Rigidbody2D>();
        targetPosOut = new Vector2(transform.position.x, transform.position.y - 0.8f);
        targetPosIn = new Vector2(transform.position.x, transform.position.y);
    }

	void Update ()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            PusherOut();
            Invoke("PusherIn", 1f);
        }
        RaycastHit2D hitPusherDown = Physics2D.Raycast(transform.position, Vector2.down, moveDistance, playerMask);
        if (pushing == true && hitPusherDown.collider.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = pusherPos.transform.position + new Vector3(0.0f, -1.4f, 0.0f);
        }
    }
    public void PusherOut()
    {
        pushing = true;
        float step = outSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosOut, step);
    }
    public void PusherIn()
    {
        pushing = false;
        waitTime = 3f;
        float step = inSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosIn, step);
    }
}
