using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    //Erilaiset platformit. Kaikki yhtä pitkiä y-suunnassa.
    
    public Vector3 currentPlatPos;
    //Sen hetkisen platformin mesta
    public Vector3 newPlatPos;
    //Seuraavan platformin mesta

    public void Start()
    {
        for (int i = 0; i < platformPrefabs.Length; i++)
        {
            currentPlatPos = platformPrefabs[i].transform.position;
        }
        newPlatPos = currentPlatPos;
        Instantiate(platformPrefabs[0]);
    }
    public void NewRandomPlat()
    {
        newPlatPos = newPlatPos + new Vector3(0.0f, 10.0f, 0.0f); 
        //Uus platformi aina 10 yksikköä eteenpäin y-suunnassa
        Instantiate(platformPrefabs[Random.Range(1, platformPrefabs.Length)], newPlatPos, Quaternion.identity);
        //Spawnataan randomisti joku platformeista. Alotetaan 1:stä, koska 0, eli ensimmäinen on alotusplatform.
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlatTrigger")
        {
            NewRandomPlat();
            GameObject.FindGameObjectWithTag("PlatTrigger").SetActive(false); 
            //Deaktivoidaan näkymätön palikka platformin alusta, jottei yksi palikka instantoi montaa platformia
        }
    }
}
