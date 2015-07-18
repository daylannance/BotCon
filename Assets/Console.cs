using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BotCon;

public class Console : MonoBehaviour {
    InputField text;
	// Use this for initialization
	void Start () {
        text = GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnSubmit()
    {
        IGPI.interpreter.RunConsoleCommand();
        text.text = "";
        text.Select();
        text.ActivateInputField();
    }
}
