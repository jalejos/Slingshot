using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public static FollowCam cam;
	public GameObject poi;
	Vector3 initialPos;
	float easing;

	void Awake(){
		cam = this;
		initialPos = this.transform.position;
		easing = .05f;
	}

	void FixedUpdate(){
		if (poi == null)
			return;
		Vector3 pos = poi.transform.position;
		pos.z = initialPos.z;
		pos.x = Mathf.Max (pos.x, initialPos.x);
		pos.y = Mathf.Max (pos.y, initialPos.y);
		Vector3.Lerp(this.transform.position,pos,easing);
		this.transform.position = pos;

	}
}
