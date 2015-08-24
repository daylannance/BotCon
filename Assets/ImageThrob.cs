using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageThrob : MonoBehaviour {
    public Image image;
    public float throbsPerSecond;
    public float minAmplitude;
    public float maxAmplitude;
    float currentRadians;
    float radiansPerUpdate;
    float range;
	// Use this for initialization
	void Awake () {
        range = maxAmplitude - minAmplitude;
        radiansPerUpdate = Mathf.PI * 2 * throbsPerSecond * Time.fixedDeltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate()
    {
        currentRadians += radiansPerUpdate;
        Color color = image.color;
        color.a = minAmplitude + (Mathf.Sin(currentRadians) * range);
        image.color = color;
    }
}
