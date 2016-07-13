using UnityEngine;
using System.Collections;

public class LineController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public GameObject lineObject;

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Instantiate(lineObject, transform.position, transform.rotation);
		}
	}
}
