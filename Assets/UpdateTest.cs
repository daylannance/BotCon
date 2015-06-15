using UnityEngine;
using System.Collections;

public class UpdateTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string cmd = 
@"gO = GameObject.FindSceneObjectsOfType(Test)
print gO[0]
obj = gO[0]";
		IGPI.interpreter.RunCommand(cmd);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
		string cmd = @"Move(obj,Vector3(0.01,0,0))";
		IGPI.interpreter.RunCommand (cmd);
	}
}
