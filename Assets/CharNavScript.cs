using UnityEngine;
using System.Collections;

public class CharNavScript : MonoBehaviour {
	NavMeshAgent agent;
	public GameObject markerPrefab;
	Animator anim;
	int animVelocityRef;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		animVelocityRef = Animator.StringToHash("velocity");
		//agent.updatePosition = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetFloat(animVelocityRef, agent.velocity.magnitude);
		if(agent.hasPath)
		{
			Instantiate (markerPrefab,agent.nextPosition,Quaternion.identity);
		}
	}
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast (ray, out hit,100))
			{
				agent.SetDestination(hit.point);
			}
		}
	}
}
