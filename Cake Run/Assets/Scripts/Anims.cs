using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anims : Destroyer
{
    public Animator collAnimator;


    public SpriteRenderer spriteRenderer;
    void Start ()
    {
        collAnimator = GetComponent<Animator>();
        collAnimator.enabled = false;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
}
	void Update ()
    {

	}
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collAnimator.enabled = true;
            Invoke("SetActive", 2f);
        }
    }
    public void SetActive()
    {
        this.spriteRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
