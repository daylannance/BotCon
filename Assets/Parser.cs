using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BotCon;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

[System.Serializable]
public class Parser : MonoBehaviour {
	
	public static Dictionary<string,GameObjectData> AllGameObjects = new Dictionary<string,GameObjectData>();
	public string testCommand = "";
	// Use this for initialization
	void Start () {
		foreach(GameObject obj in FindObjectsOfType<GameObject>())
		{
			if(obj.GetComponent<ShowID>())
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
	}
	
	// Update is called once per frame
	void Update () {
		//UpdateAllGameObjectData ();
		//MakeRequestedChanges();
		UpdateAllGameObjectData ();
		ProcessNextCommand();
		if(!string.IsNullOrEmpty(testCommand))
		{
			IGPI.interpreter.RunCommand (testCommand + "()");
		}
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
    void Log(string message)
    {
        IGPI.interpreter.Log(message);
    }
	public void ProcessNextCommand()
	{
		if(ComData.PyToEngineCommands.Count > 0)
		{
            var c = ComData.PyToEngineCommands.Dequeue();
            if (c.args != null)
            {
                if (c.cmd != null)
                {
                    c.cmd.Invoke(c.args);
                }
                else Log("No delegate was passed to Command object. This is likely do to invalid arguments.");
            }
            else Log(c.cmd.Target.ToString() + " has null or invalid arguments.");
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