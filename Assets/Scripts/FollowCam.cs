using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public static FollowCam cam;
	public GameObject poi;
	Vector3 camPos;
	float easing;

	void Awake(){
		cam = this;
		camPos = this.transform.position;
		easing = 0.05f;
	}

	void FixedUpdate(){
		if (poi == null)
			return;
		Vector3 pos = poi.transform.position;
		pos.z = camPos.z;
		pos.x = Mathf.Max (camPos.x, pos.x);
		pos.y = Mathf.Max (camPos.y, pos.y);

		pos = Vector3.Lerp (this.transform.position, pos, easing);
		this.transform.position = pos;
	}
}
