using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	public float perspectiveZoomSpeed = 0.1f;        // The rate of change of the field of view in perspective mode.

	private float lerpSpeed = 1.0F;
	private Rotator r;
	
	private Vector3 theSpeed;
	private Vector3 avgSpeed;
	private bool isDragging = false;
	private bool isGonnaDrag = false;
	private Vector3 targetSpeedX;
	private float touchTime;
	private Renderer rend;
	private Vector3 screenPoint;
	private Vector3 initialPosition;
	private Vector3 offset;
	private Rigidbody2D rbody;
	private Color gold;

	void Start (){
		Input.simulateMouseWithTouches = true;
		rend = GetComponent<Renderer> ();
		rbody = GetComponent<Rigidbody2D> ();
		gold = new Color (1, 0.7f, 0.5f);
		r = GetComponent<Rotator> ();
	}

	void OnMouseDown() {
		//SendMessage ("set_rotate", false);
		Debug.Log (Time.time + " : Touched " + name + ", " + Input.mousePosition.x + ":" + Input.mousePosition.y);
		isDragging = true;
		isGonnaDrag = true;
		touchTime = Time.time;
		//screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); // I removed this line to prevent centring 
		initialPosition = transform.position;
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		if(rbody != null)
			rbody.velocity = Vector2.zero;
		if (r != null) {
			r.autorotate = false;
		}
	}

	void OnMouseDrag() { 
		if (isGonnaDrag && Time.time > touchTime + 1F) {
			Debug.Log (Time.time + " : Dragging " + name + ", " + Input.mousePosition.x + ":" + Input.mousePosition.y);
			rend.material.SetColor("_Color", gold);
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
			isDragging = false;
			if(rbody != null)
				rbody.velocity = curPosition - initialPosition + new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);
		}
	}	

	void OnMouseExit() {
		isGonnaDrag = false;
	}

	void OnMouseUp() {
		isDragging = false;
		isGonnaDrag = false;
		rend.material.SetColor("_Color",Color.white);
	}
	
	void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, -2);
		//pinch zoom start
		if (Input.touchCount == 2 && isDragging)
		{
			isGonnaDrag = false;
			rend.material.SetColor("_Color",Color.white);
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;
			transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale+new Vector3(deltaMagnitudeDiff * perspectiveZoomSpeed, deltaMagnitudeDiff * perspectiveZoomSpeed, 1), Time.deltaTime);

			Vector3 diff = touchOne.position - touchZero.position;
			float angle = (Mathf.Atan2(diff.y, diff.x));
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0f, 0f ,Mathf.Rad2Deg * angle), Time.deltaTime);

			// Clamp the field of view to make sure it's between 0 and 180.
			//transform.localScale.magnitude = Mathf.Clamp(transform.localScale.magnitude, 0.1f, 179.9f);
		}
	}
}