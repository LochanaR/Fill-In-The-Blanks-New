using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scores : MonoBehaviour {
	Text score;
	private int _score;
	// Use this for initialization
	void Start () {
	
		_score = 0;
		score = gameObject.GetComponent<Text> ();
		score.text ="Score" + _score; 
	}
	
	// Update is called once per frame
	void Update () {
	
		Debug.Log(score.text);
	}
}
