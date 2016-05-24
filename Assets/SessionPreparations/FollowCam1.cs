using UnityEngine;
using System.Collections;

public class FollowCam1 : MonoBehaviour {
	public static FollowCam1 cam;
	public GameObject poi;
	Vector3 initialPos;
	float easing;

	void Awake(){
		cam = this;
		initialPos = this.transform.position;
		easing = .05f;
	}

	void FixedUpdate(){
		Vector3 pos;
		if (poi == null) {
			pos = Vector3.zero;
		} else {
			pos = poi.transform.position;
			if (poi.tag == "Projectile") {
				if (poi.GetComponent<Rigidbody> ().IsSleeping () || Input.GetMouseButtonUp(1)) {
					Destroy (poi);
					poi = null;
					return;
				}
			}
		}
		pos.z = initialPos.z;
		pos.x = Mathf.Max (pos.x, initialPos.x);
		pos.y = Mathf.Max (pos.y, initialPos.y);
		Vector3.Lerp(this.transform.position,pos,easing);
		this.transform.position = pos;
		this.GetComponent<Camera> ().orthographicSize = pos.y + 10;
	}
}
