using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	public float orbitSpeed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround(Vector3.zero, Vector3.right, orbitSpeed * Time.deltaTime);
		transform.LookAt(Vector3.zero);

    }
}
