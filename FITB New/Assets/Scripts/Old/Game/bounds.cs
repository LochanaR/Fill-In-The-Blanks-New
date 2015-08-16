using UnityEngine;
using System.Collections;

public class bounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		collision.gameObject.SendMessage ("hit_boundary");
	}
}
