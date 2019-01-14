using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFront : MonoBehaviour
{
    // Spawner tekee tästä oliosta kopioita pelimaailmaan.
    public GameObject ObjectToSpawn;

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
        Instantiate(ObjectToSpawn, transform.position+ new Vector3(0.05f, -0.05f, 0.0f), transform.rotation);
    }
}

