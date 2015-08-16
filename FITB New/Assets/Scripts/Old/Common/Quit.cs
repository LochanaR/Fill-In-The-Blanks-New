using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Quit : MonoBehaviour, IPointerClickHandler
{	
	public void OnPointerClick(PointerEventData eventData) {
		Application.Quit ();
	}
}
