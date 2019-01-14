using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Spawner tekee tästä oliosta kopioita pelimaailmaan.
    public GameObject front;
    public GameObject right;
    public GameObject left;

    // Kuinka usein spawner luo uusia olioita
    public float SpawnTime = 1f;

    // Kauanko aikaa on kulunut siitä, kun olio luotiin viimeksi.
    private float timer = 0;

    private void Update()
    {
        if (timer > SpawnTime)
        {
            Spawn();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void Spawn()
    {
        Instantiate(front, transform.position+ new Vector3(0.05f, -0.05f, 0.0f), transform.rotation);
        Instantiate(right, transform.position + new Vector3(0.6f, 0.0f, 0.0f), transform.rotation);
        Instantiate(left, transform.position + new Vector3(-0.5f, 0.0f, 0.0f), transform.rotation);
    }
}

