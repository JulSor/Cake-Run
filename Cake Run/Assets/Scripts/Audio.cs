using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip coinPickup;
    
    public AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
	}

    public void Update()
    {
        if (Input.GetKey("k"))
        {
            audioSource.PlayOneShot(coinPickup, 1.0f);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(coinPickup, 1.0f);
        }
    }
    
}