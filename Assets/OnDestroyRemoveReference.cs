using UnityEngine;
using System.Collections;
using BotCon;

public class OnDestroyRemoveReference : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDestroy()
	{
		Parser.AllGameObjects.Remove(gameObject.GetInstanceID().ToString());
	}
}
