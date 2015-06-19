using UnityEngine;
using System.Collections;
using BotCon;

public class ShowString : MonoBehaviour {
	GUIText gText;
	// Use this for initialization
	void Start () {
		gText = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		gText.text = ComData.PyToEngineCommands.ToString();
	}
}
