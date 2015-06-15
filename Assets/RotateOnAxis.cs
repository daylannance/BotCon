using UnityEngine;
using System.Collections;

public class RotateOnAxis : MonoBehaviour {
	public Vector3 axis;
	public float degreesPerUpdate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
		transform.Rotate(axis,degreesPerUpdate);
	}
}
