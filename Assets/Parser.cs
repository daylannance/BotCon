using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BotCon;
using UnityEngine;

[System.Serializable]
public class Parser : MonoBehaviour {
	
	public Dictionary<string,GameObjectData> AllGameObjects = new Dictionary<string,GameObjectData>();
	// Use this for initialization
	void Start () {
		foreach(GameObject obj in FindObjectsOfType<GameObject>())
		{
			GameObjectData data = new GameObjectData();
			data.gameObject = obj;
			AllGameObjects.Add (obj.GetInstanceID().ToString(),data);
			if(obj.GetComponent<Rigidbody>())
			{
				data.rigidbody = obj.GetComponent<Rigidbody>();
			}
			data.id = obj.GetInstanceID();
			ComData.AllGameObjects = AllGameObjects;
			Debug.Log (AllGameObjects.Count);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//UpdateAllGameObjectData ();
		//MakeRequestedChanges();
		UpdateAllGameObjectData ();
		ProcessNextCommand();
	}
	public void UpdateAllGameObjectData()
	{
		foreach(var data in AllGameObjects)
		{
			data.Value.position = data.Value.gameObject.transform.position;
			if(data.Value.gameObject.GetComponent<Rigidbody>())
			{
				data.Value.velocity = data.Value.gameObject.GetComponent<Rigidbody>().velocity;
			}
		}
		BotCon.ComData.AllGameObjects = AllGameObjects;	
	}
	public void ProcessNextCommand()
	{
		if(ComData.PyToEngineCommands.Count > 0)
		{
			var c = ComData.PyToEngineCommands.Dequeue();
			c.cmd.Invoke(c.args);	
		}
	}
	public void ParseStringCommand(string command)
	{
		if(string.IsNullOrEmpty(command)) return;
		GameObject gObject = null;
		string[] words = command.Split(' ');
		
		switch(words[0])
		{
			case "Move":
				gObject = GetGameObjectByID(words[1]);
				if(gObject)
				{
					Vector3 vector = parseVector3(words[2], words[3], words[4]);
					gObject.transform.position += vector;
				}
				break;
		}
	}
	GameObject GetGameObjectByID(string id)
	{
		var gObject = AllGameObjects[id].gameObject;
		return gObject;
	}
	Vector3 parseVector3(string xStr, string yStr, string zStr)
	{
		Vector3 vect = Vector3.zero;
		float x,y,z;
		if(float.TryParse(xStr, out x)
			&&float.TryParse(yStr,out y)
			&&float.TryParse(zStr,out z))
		{
			vect = new Vector3(x,y,z);
		}
		else throw(new System.Exception("parseVector got some invalid arguments"));
		return vect;
	}
	public void MakeRequestedChanges()
	{
		while(ComData.Changes.Count >0)
		{
			GameObjectData data = ComData.Changes.Dequeue();
			GameObject obj = data.gameObject;
			obj.transform.position = data.position;
			if(obj.GetComponent<Rigidbody>())
			{
				obj.GetComponent<Rigidbody>().velocity = data.velocity;
			}
		}
	}
}