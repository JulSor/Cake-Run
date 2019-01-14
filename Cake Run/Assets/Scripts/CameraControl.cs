using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //public Transform Player;
    public GameObject Playerr;

    [SerializeField] Vector3 cameraOffSet;

    public float speed = 3f;

	void Start ()
    {
        //cameraOffSet = new Vector3(0.0f, 10.0f, -70f);
	}
	
	void Update ()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;

        position.y = Mathf.Lerp(this.transform.position.y, Playerr.transform.position.y + 5, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, Playerr.transform.position.x, interpolation);

        this.transform.position = position;

        /*if (position.y > -1.0f)
        {
            cameraOffSet = new Vector3(0.0f, 10.0f, -70);
            //CameraFollowing();
        }*/
    }
    /*public void CameraFollowing()
    {
        transform.position = Player.position + cameraOffSet;
    }*/
}
