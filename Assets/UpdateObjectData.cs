using UnityEngine;
using System.Collections;
using BotCon;

public class UpdateObjectData : MonoBehaviour {
	Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObjectData data = ComData.AllGameObjects[GetInstanceID().ToString ()];
		data.position = transform.position;
		if(rigidbody)
		{
			data.velocity = rigidbody.velocity;
		}
	}
}
