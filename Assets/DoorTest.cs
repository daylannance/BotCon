using UnityEngine;
using System.Collections;
using BotCon;

public class DoorTest : MonoBehaviour {
	public TimedRotation rotator;
	// Use this for initialization
	void Start () {
		rotator = GetComponentInChildren<TimedRotation>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown()
	{
		rotator.Rotate(rotator.Axis,rotator.targetAngle, 1f);
	}
}
