using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class pauseButton : MonoBehaviour, IPointerClickHandler
{
	public GameObject pauseMenu;
	private float timeScale;

	void Start(){
		Time.timeScale = 1f;
		timeScale = Time.timeScale;
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (Time.timeScale == timeScale) {
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
		} else {
			pauseMenu.SetActive (false);
			Time.timeScale = timeScale;
		}
	}
}

