using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	public float orbitSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround(Vector3.zero, Vector3.right, orbitSpeed * Time.deltaTime);
		transform.LookAt(Vector3.zero);

        /*Light light = GetComponent<Light>();
        if (transform.position.y < 0)
        {
            *//*if (light.intensity > 0.5)
            {
                light.intensity -= (float)(0.2 * Time.deltaTime);
            }*//*
        }
        else
        {
            if (gameObject.name == "Sun")
            {
                *//*if (light.intensity < 0.8)
                {
                    light.intensity += (float)(0.5 * Time.deltaTime);
                }*//*
            }
            if (gameObject.name == "Moon")
            {
                *//*if (light.intensity < 0.6)
                {
                    light.intensity += (float)(0.2 * Time.deltaTime);
                }*//*
            }
        }*/
    }
}
