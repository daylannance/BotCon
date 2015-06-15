using UnityEngine;
using System.Collections;
[RequireComponent(typeof(GUIText))]
public class ShowID : MonoBehaviour {
	
	GUIText gText;
	// Use this for initialization
	void Start () {
		GameObject obj = Instantiate(Resources.Load<GameObject>("IDTag")) as GameObject;
		gText = obj.GetComponent<GUIText>();
		gText.text = gameObject.GetInstanceID().ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Camera cam = Game.currentCam;
		Vector3 screenPos = Game.currentCam.WorldToScreenPoint(transform.position);
		screenPos.x = screenPos.x / Screen.width;
		screenPos.y = screenPos.y / Screen.height;
		gText.transform.position = screenPos;
	}
}
