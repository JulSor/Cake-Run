using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] Vector2 movementVector = new Vector2(0.0f, -0.8f);
    [SerializeField] float period = 3.0f;

    float movementFactor;
    public Rigidbody2D mpf;

    Vector2 startingPos;

    void Start()
    {
        mpf = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
    }
    void FixedUpdate()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector2 offset = movementVector * movementFactor;
        mpf.MovePosition(startingPos + offset);
    }
}
