using UnityEngine;
using System.Collections;
using BotCon;

public class StartAsCurrentCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Awake()
	{
		Game.currentCam = GetComponent<Camera>();
	}
}
