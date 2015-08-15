using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageThrob : MonoBehaviour {
    public Image image;
    public float throbsPerSecond;
    float currentRadians;
    float radiansPerUpdate;
	// Use this for initialization
	void Awake () {
        radiansPerUpdate = ((Mathf.PI * 2) / throbsPerSecond) * Time.fixedDeltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate()
    {
        currentRadians += radiansPerUpdate;
        Color color = image.color;
        color.a = Mathf.Sin(currentRadians);
        image.color = color;
    }
}
